using System;
using Jsonize.Abstractions.Configuration;
using Jsonize.Abstractions.Interfaces;
using Jsonize.Parser;
using Jsonize.Serializer;
using Microsoft.Extensions.DependencyInjection;

namespace Jsonize.DependencyInjection
{
    public static class JsonizeExtensions
    {
        public static IServiceCollection AddJsonizer(this IServiceCollection serviceCollection,
            IJsonizer jsonizer)
        {
            serviceCollection.AddSingleton<IJsonizer>(sp => jsonizer);

            return serviceCollection;
        }
        public static IServiceCollection AddJsonizer(this IServiceCollection serviceCollection,
            IJsonizeParser parser,
            IJsonizeSerializer serializer)
        {
            var jsonizer = new Jsonizer(parser, serializer);
            serviceCollection.AddSingleton<IJsonizer, Jsonizer>(sp => jsonizer);

            return serviceCollection;
        }
        
        public static IServiceCollection AddJsonizer(this IServiceCollection serviceCollection)
        {
            var jsonizer = new Jsonizer(new JsonizeParser(), new JsonizeSerializer());
            serviceCollection.AddSingleton<IJsonizer, Jsonizer>(sp => jsonizer);

            return serviceCollection;
        }
        
        public static IServiceCollection AddJsonizer(this IServiceCollection serviceCollection,
            IJsonizeParser parser)
        {
            var jsonizer = new Jsonizer(parser, new JsonizeSerializer());
            serviceCollection.AddSingleton<IJsonizer, Jsonizer>(sp => jsonizer);

            return serviceCollection;
        }
        
        public static IServiceCollection AddJsonizer(this IServiceCollection serviceCollection,
            IJsonizeSerializer serializer)
        {
            var jsonizer = new Jsonizer(new JsonizeParser(), serializer);
            serviceCollection.AddSingleton<IJsonizer, Jsonizer>(sp => jsonizer);

            return serviceCollection;
        }
        
        public static IServiceCollection AddJsonizer(this IServiceCollection serviceCollection,
            Func<IJsonizeParser> parser,
            Func<IJsonizeSerializer> serializer)
        {
            var jsonizer = new Jsonizer(parser(), serializer());
            serviceCollection.AddSingleton<IJsonizer, Jsonizer>(sp => jsonizer);

            return serviceCollection;
        }
        
        public static IServiceCollection AddJsonizer(this IServiceCollection serviceCollection,
            Func<IJsonizer> jsonizer)
        {
            serviceCollection.AddSingleton<IJsonizer>(sp => jsonizer());

            return serviceCollection;
        }
        
        public static IServiceCollection AddJsonizer(this IServiceCollection serviceCollection,
            JsonizeParserConfiguration parserConfiguration,
            IJsonizeSerializer serializer)
        {
            var jsonizer = new Jsonizer(new JsonizeParser(parserConfiguration), serializer);
            serviceCollection.AddSingleton<IJsonizer, Jsonizer>(sp => jsonizer);

            return serviceCollection;
        }
    }
}