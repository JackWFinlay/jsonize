using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Jsonize.Abstractions.Models;

namespace Jsonize.Abstractions.Interfaces
{
    public interface IJsonizeParser
    {
        /// <summary>
        /// Parse the given HTML string to the Jsonize format.
        /// </summary>
        /// <param name="htmlString">The HTML to parse.</param>
        /// <param name="cancellationToken">Cancellation token to request cancellation.</param>
        /// <returns>The Jsonize representation of the supplied HTML.</returns>
        Task<JsonizeNode> ParseAsync(string htmlString, CancellationToken cancellationToken = default);

        /// <summary>
        /// Parse the given HTML stream to the Jsonize format.
        /// </summary>
        /// <param name="htmlStream">The HTML stream to parse.</param>
        /// <param name="cancellationToken">Cancellation token to request cancellation.</param>
        /// <returns>The Jsonize representation of the supplied HTML.</returns>
        Task<JsonizeNode> ParseAsync(Stream htmlStream, CancellationToken cancellationToken = default);
    }
}