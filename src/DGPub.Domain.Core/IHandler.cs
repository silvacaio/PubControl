using System.Threading.Tasks;

namespace DGPub.Domain.Core
{
    public interface IHandler<T, TValue>
        where T : Command
        where TValue : EventBase
    {
        Task<TValue> Handler(T command);
    }
}
