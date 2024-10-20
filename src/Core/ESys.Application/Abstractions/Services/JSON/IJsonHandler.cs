namespace ESys.Application.Abstractions.Services.JSON;

public interface IJsonHandler
{
    string ConvertKeyValuePairsToJson(Dictionary<string,string> keyValuePairs);
}