using Newtonsoft.Json;

namespace Synword.Domain.Extensions;

public static class JsonExtensions
{
    public static T FromJson<T>(this string json) =>
        JsonConvert.DeserializeObject<T>(json);

    public static string ToJson<T>(this T obj) =>
        JsonConvert.SerializeObject(obj);
}
