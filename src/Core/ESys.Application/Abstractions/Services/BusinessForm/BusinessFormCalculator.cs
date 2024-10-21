using System.Numerics;
using ESys.Application.Abstractions.Persistence;
using ESys.Application.Abstractions.Services.BusinessFormCalculation;
using ESys.Application.Abstractions.Services.JSON;
using ESys.Application.Exceptions;
using ESys.Application.Statics;
using ESys.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Expression = org.matheval.Expression;

namespace ESys.Application.Abstractions.Services.BusinessForm;

public class BusinessFormCalculator
{
    IBusinessRepository _businessRepository;
    private readonly IJsonHandler _jsonHandler;
    private readonly IExpHandler _expHandler;
    private readonly IBusinessXmlRepository _businessXmlRepository;
    private readonly IServiceProvider _serviceProvider;

    //Q? what is Exp for?
    private string Exp = @"len,*,wid,*,qty,*,qty";

    private Dictionary<string, string> dataPool = new();
    private Dictionary<string, string> expPool = new();
    private Dictionary<string, string> funcPool = new();
    private Dictionary<string, string> lookupPool = new();
    private Business _business = new();


    public BusinessFormCalculator(IBusinessRepository businessRepository, IJsonHandler jsonHandler,
        IExpHandler expHandler, IBusinessXmlRepository businessXmlRepository,IServiceProvider serviceProvider)
    {
        _businessXmlRepository = businessXmlRepository;
        _serviceProvider = serviceProvider;
        _businessRepository = businessRepository;
        _jsonHandler = jsonHandler;
        _expHandler = expHandler;
    }

    /// <summary>
    /// Calculates data needed for Business Form reevaluation
    /// </summary>
    /// <param name="requestBody">A Json string containing Business Form data needed for calculation</param>
    /// <returns>A Json string containing calculated data for updating Business Form</returns>
    /// <exception cref="KeyNotFoundException">Occurs when received Business Id in requestBody does not exist in database</exception>
    public async Task<string> GetCalculatedBusinessForm(string requestBody)
    {
        // Q?
        // todo: make calculation from string exp and data then return the calculated values.

        FillJsonInDataPool(requestBody);

        var business = await _businessRepository.GetByIdAsync(BigInteger.Parse(dataPool["bizid"]));
        if (business?.BizId is not null)
            _business = business;
        else
            throw new KeyNotFoundException("Business does not exist.");

        AddExpsToDataPool(_business);
        AddFuncsToDataPool(_business);
        AddLookupsToDataPool(_business);

        foreach (var item in lookupPool)
            ApplyLookups(item.Value);
        foreach (var item in funcPool)
            ApplyFuncs(item.Value);

        var result = _jsonHandler.ConvertKeyValuePairsToJson(_expHandler.ApplyExpsOnData(dataPool, expPool));
        return result;
    }

    /// <summary>
    /// Iterates over lookup pool and data pool, then fetches BusinessXml from repository and applies lookups on data
    /// </summary>
    /// <param name="lookupStr">A string containing lookup values</param>
    private void ApplyLookups(string lookupStr)
    {
        var lookupDic = new Dictionary<string, string>();
        Expression expression = new Expression();

        foreach (var item in dataPool)
            if (double.TryParse(item.Value, out double d) && !Double.IsNaN(d) && !Double.IsInfinity(d))
                expression.Bind(item.Key, d);
            else
                expression.Bind(item.Key, item.Value);

        var lookups = JArray.Parse(lookupStr);
        foreach (JObject root in lookups)
        foreach (KeyValuePair<string, JToken> param in root)
        {
            if ((string)param.Value[BusinessFormStatics.ExpTag] != null)
            {
                string strFormula = param.Value[BusinessFormStatics.ExpTag]?.ToString().Replace("\\\"", "'");
                expression.SetFomular(strFormula);
                try
                {
                    lookupDic.Add(param.Key, expression.Eval().ToString());
                }
                catch
                {
                    // Ignore the occured error and get back to the rest of array
                }

                break; // This break is for preventing duplication in inner loop
            }

            lookupDic.Add(param.Key, (string)param.Value["val"]);
            break; // This break is for preventing duplication in outer loop
        }

        var xmlData = _businessXmlRepository.GetBusinessXmlAsDictionary(_business, lookupDic);
        foreach (var (key, value) in xmlData.Where(kv => !dataPool.ContainsKey(kv.Key)))
            dataPool.Add(key, value);
    }

    /// <summary>
    /// input type: {{'BizId':{'val':'xxx'}},{'len':{'val':'xxx'}},{'wid':{'val':'xxx'}}}{{'BizId':{'val':'yyy'}},{'len':{'val':'xxx'}},{'wid':{'val':'xxx'}}}
    /// The method will call a Business by BizId and fill the values to get the return/s of the biz.
    /// the format of calling a function is same as a client call like: "{{'BizId':{'val':'xxx'}},{'len':{'val':'xxx'}},{'wid':{'val':'xxx'}}}"
    /// return type of the func will be pair of (key,value) which will add to a dictionary and return by the method.
    /// </summary>
    /// <param name="funcStr"></param>
    /// <returns>Dictionary<key:string, value:string></returns>
    public void ApplyFuncs(string funcStr)
    {
        var keyValuePairs = new Dictionary<string, string>();

        Expression expression = new Expression();
        foreach (var item in dataPool)
        {
            try
            {
                expression.Bind(item.Key, Convert.ToDouble(item.Value));
            }
            catch (Exception)
            {
                expression.Bind(item.Key, item.Value);
                //throw;
            }
        }

        var objects = JArray.Parse(funcStr); // parse as array
        foreach (JObject root in objects)
        {
            foreach (KeyValuePair<string, JToken> param in root)
            {
                if ((string)param.Value[BusinessFormStatics.ExpTag] != null)
                {
                    expression.SetFomular(
                        new string((string)param.Value[BusinessFormStatics.ExpTag]).Replace("\\\"", "\""));

                    keyValuePairs.Add(param.Key, expression.Eval().ToString());
                    break;
                }
                else
                {
                    keyValuePairs.Add(param.Key, (string)param.Value["val"]);
                }
            }
        }

        var innerCalculator = _serviceProvider.GetRequiredService<BusinessFormCalculator>();
        FillJsonInDataPool(innerCalculator.GetCalculatedBusinessForm(_jsonHandler.ConvertKeyValuePairsToJson(keyValuePairs)).Result);
    }

    /// <summary>
    /// Iterates over Exp property of businesses and fills the items in exp pool
    /// </summary>
    /// <param name="business">Corresponding Business</param>
    private void AddExpsToDataPool(Business business)
    {
        foreach (JObject root in JArray.Parse(business.Exp))
        foreach (KeyValuePair<string, JToken> param in root)
            expPool.Add(param.Key, (string)param.Value[BusinessFormStatics.ExpTag]);
    }

    /// <summary>
    /// Iterates over Func property of Business and fills the items in func pool
    /// </summary>
    /// <param name="business">Corresponding Business</param>
    private void AddFuncsToDataPool(Business business)
    {
        try
        {
            if (string.IsNullOrEmpty(business.Func)) return;

            foreach (JObject root in JArray.Parse(business.Func))
            foreach (KeyValuePair<string, JToken> param in root)
                funcPool.Add(param.Key, param.Value[BusinessFormStatics.FuncTag].ToString());
        }
        catch (Exception e)
        {
            throw new BadRequestException(e.Message);
        }
    }

    /// <summary>
    /// Iterates over Lookup property of Business and fills the items in lookup pool
    /// </summary>
    /// <param name="business">Corresponding Business</param>
    private void AddLookupsToDataPool(Business business)
    {
        if (string.IsNullOrEmpty(_business.Lookup)) return;

        foreach (JObject root in JArray.Parse(business.Lookup))
        foreach (KeyValuePair<string, JToken> param in root)
            lookupPool.Add(param.Key, (string)param.Value[BusinessFormStatics.LookupTag]);
    }

    /// <summary>
    /// Iterates over Json file and fills in data pool
    /// </summary>
    /// <param name="jsonData">A Json string containing key-value pairs</param>
    private void FillJsonInDataPool(string jsonData)
    {
        foreach (JObject root in JArray.Parse(jsonData))
        foreach (KeyValuePair<string, JToken> param in root)
            dataPool.Add(param.Key, (string)param.Value["val"]);
    }
}