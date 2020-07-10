using DGPub.Domain.Core;

namespace DGPub.Domain.Tabs.Repositories
{
    public interface ITabRepository : IRepository<Tab>
    {
        IItemTabRepository ItemTabRepository { get; set; }
    }
}
