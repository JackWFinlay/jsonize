using System.Threading.Tasks;
using JackWFinlay.Jsonize.Abstractions.Models;

namespace JackWFinlay.Jsonize.Abstractions.Interfaces
{
    public interface IJsonizeSerializer
    {
        /// <summary>
        /// Serialize the <see cref="JsonizeNode"/> representation into a <see cref="string"/>.
        /// </summary>
        /// <param name="jsonizeNode">The <see cref="JsonizeNode"/> parent to serialize.</param>
        /// <returns>The JSON <see cref="string"/> of the Jsonize data-structure.</returns>
        Task<string> Serialize(JsonizeNode jsonizeNode);
    }
}