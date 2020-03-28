using System.Threading.Tasks;
using JackWFinlay.Jsonize.Abstractions.Models;

namespace JackWFinlay.Jsonize.Abstractions.Interfaces
{
    public interface IJsonizeParser
    {
        Task<JsonizeNode> ParseAsync(string htmlString);
    }
}