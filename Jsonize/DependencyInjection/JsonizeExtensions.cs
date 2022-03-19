using Jsonize.Abstractions.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Jsonize.DependencyInjection
{
    public static class JsonizeExtensions
    {
        public static IServiceCollection AddDefaultJsonizer(this IServiceCollection serviceCollection,
            IJsonizeParser parser,
            IJsonizeSerializer serializer)
        {
            var jsonizer = new Jsonizer(parser, serializer);
            serviceCollection.AddSingleton<IJsonizer, Jsonizer>(sp => jsonizer);

            return serviceCollection;
        }
    }
}