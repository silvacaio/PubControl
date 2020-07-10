using DGPub.Domain.Items;
using DGPub.Domain.Items.Repositories;
using DGPub.Infra.Data.Context;

namespace DGPub.Infra.Data.Repositories.Items
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(DGPubContext context) : base(context)
        {
        }
    }
}
