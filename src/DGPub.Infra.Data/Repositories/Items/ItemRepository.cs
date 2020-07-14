using DGPub.Domain.Items;
using DGPub.Domain.Items.Repositories;
using DGPub.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DGPub.Infra.Data.Repositories.Items
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(DGPubContext context) : base(context)
        {
        }

        public Item GetByName(string name)
        {
            return Db.Item.AsNoTracking().FirstOrDefault(i => i.Name == name);
        }
    }
}
