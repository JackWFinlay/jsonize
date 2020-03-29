﻿using System;
using System.Threading.Tasks;
using JackWFinlay.Jsonize.Abstractions.Interfaces;
using JackWFinlay.Jsonize.Abstractions.Models;
using Newtonsoft.Json;

namespace JackWFinlay.Jsonize.Serializer.NewtonsoftJson
{
    public class NewtonsoftJsonJsonizeSerializer : IJsonizeSerializer
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public NewtonsoftJsonJsonizeSerializer()
        {
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = CustomPropertyNameContractResolver.Instance
            };

        }

        public NewtonsoftJsonJsonizeSerializer(JsonSerializerSettings jsonSerializerSettings) : this()
        {
            _jsonSerializerSettings = jsonSerializerSettings;

            if (_jsonSerializerSettings.ContractResolver != null)
            {
                _jsonSerializerSettings.ContractResolver = CustomPropertyNameContractResolver.Instance;
            }
        }

        public Task<string> Serialize(JsonizeNode jsonizeNode)
        {
            string result = JsonConvert.SerializeObject(jsonizeNode, _jsonSerializerSettings);

            return Task.FromResult(result);
        }
    }
}