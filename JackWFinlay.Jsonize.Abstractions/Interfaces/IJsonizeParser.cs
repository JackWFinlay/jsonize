using System.Threading.Tasks;
using JackWFinlay.Jsonize.Abstractions.Models;

namespace JackWFinlay.Jsonize.Abstractions.Interfaces
{
    public interface IJsonizeParser
    {
        /// <summary>
        /// Parse the given HTML string to the Jsonize format.
        /// </summary>
        /// <param name="htmlString">The HTML to parse.</param>
        /// <returns>The Jsonize representation of the supplied HTML.</returns>
        Task<JsonizeNode> ParseAsync(string htmlString);
    }
}