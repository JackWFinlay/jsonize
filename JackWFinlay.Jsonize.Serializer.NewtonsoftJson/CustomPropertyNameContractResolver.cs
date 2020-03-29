using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JackWFinlay.Jsonize.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JackWFinlay.Jsonize.Serializer.NewtonsoftJson
{
    public class CustomPropertyNameContractResolver : DefaultContractResolver
    {
        public static readonly CustomPropertyNameContractResolver Instance = new CustomPropertyNameContractResolver();
        
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            Attribute attribute = property.AttributeProvider
                .GetAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(JsonizePropertyNameAttribute));

            // Set the PropertyName to whatever is in the JsonizePropertyNameAttribute or leave it as is if not present.
            property.PropertyName = ((JsonizePropertyNameAttribute) attribute)?.Name ?? property.PropertyName;

            return property;
        }
    }
}