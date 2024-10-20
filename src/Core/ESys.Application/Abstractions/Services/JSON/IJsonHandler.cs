namespace ESys.Application.Abstractions.Services.JsonHandler;

public interface IJsonHandler
{
    string ConvertKeyValuePairsToJson(Dictionary<string,string> keyValuePairs);
}