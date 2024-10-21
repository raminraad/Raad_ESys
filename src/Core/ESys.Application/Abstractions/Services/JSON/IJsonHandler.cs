namespace ESys.Application.Abstractions.Services.JSON;

public interface IJsonHandler
{
    string ConvertKeyValuePairsToJson(Dictionary<string,string> keyValuePairs);
    T SimpleRead<T>(string fileName);
      void SimpleWrite(object obj, string fileName);

}