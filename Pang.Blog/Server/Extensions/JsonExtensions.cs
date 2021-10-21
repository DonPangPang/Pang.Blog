using Newtonsoft.Json;

namespace Pang.Blog.Server.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson<T>(this T obj) where T : class
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T? ToObject<T>(this string jsonString) where T : class
        {
            if (string.IsNullOrEmpty(jsonString))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}