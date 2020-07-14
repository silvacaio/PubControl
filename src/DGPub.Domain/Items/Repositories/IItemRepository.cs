using DGPub.Domain.Core;

namespace DGPub.Domain.Items.Repositories
{
    public interface IItemRepository : IRepository<Item>
    {
        Item GetByName(string name);
    }
}
