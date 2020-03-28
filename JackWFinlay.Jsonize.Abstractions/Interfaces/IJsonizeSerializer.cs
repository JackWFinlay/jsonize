using System.Threading.Tasks;
using JackWFinlay.Jsonize.Abstractions.Models;

namespace JackWFinlay.Jsonize.Abstractions.Interfaces
{
    public interface IJsonizeSerializer
    {
        Task<string> Serialize(JsonizeNode jsonizeNode);
    }
}