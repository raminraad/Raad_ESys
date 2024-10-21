using System.Text.Json;
using System.Text.Json.Serialization;
using ESys.Application.Abstractions.Services.JSON;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ESys.MVC;

/// <summary>
/// Service provider for Json contents
/// </summary>
public class JsonHandler 
{
    private static readonly JsonSerializerOptions _options =
        new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

    public static void SimpleWrite(object obj, string fileName)
    {
        var jsonString = JsonSerializer.Serialize(obj, _options);
        File.WriteAllText(fileName, jsonString);
    }

    public static T SimpleRead<T>(string fileName)
    {
        string jsonData = File.ReadAllText(fileName);
        return JsonConvert.DeserializeObject<T>(jsonData);
    }

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