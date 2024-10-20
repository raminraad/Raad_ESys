using ESys.Application.Abstractions.Services.JSON;

namespace ESys.Libraries.JSON;

/// <summary>
/// Service provider for Json contents
/// </summary>
public class JsonHandler : IJsonHandler
{
    /// <summary>
    /// Converts a given key-value collection to a Json string
    /// </summary>
    /// <param name="keyValPairs"></param>
    /// <returns>Json string containing kel-value collection data</returns>
    public string ConvertKeyValuePairsToJson(Dictionary<string, string> keyValPairs)
    {
        string json = "[{";

        var first = true;

        foreach (var item in keyValPairs)
        {
            if (first)
            {
                json = json + "\"" + item.Key + "\":{\"val\":\"" + item.Value + "\"}";
                first = false;
            }
            else
            {
                json = json + ",\"" + item.Key + "\":{\"val\":\"" + item.Value + "\"}";
            }

        }

        json += "}]";

        return json;
    }
}