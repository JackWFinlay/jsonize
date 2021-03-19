using System.Reflection;
using System.Text.Json;
using JackWFinlay.Jsonize.Abstractions.Attributes;
using JackWFinlay.Jsonize.Abstractions.Models;

namespace JackWFinlay.Jsonize.Serializer
{
    public class JsonizeNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            var customName = typeof(JsonizeNode)
                .GetProperty(name)?
                .GetCustomAttribute<JsonizePropertyNameAttribute>()?
                .Name 
                   ?? name;

            return customName;
        }
    }
}