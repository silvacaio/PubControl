using DGPub.Domain.Tabs;
using DGPub.Domain.Tabs.Repositories;
using DGPub.Infra.Data.Context;
using System;
using System.Linq;

namespace DGPub.Infra.Data.Repositories.Tabs
{
    public class ItemTabRepository : Repository<ItemTab>, IItemTabRepository
    {
        public ItemTabRepository(DGPubContext context) : base(context)
        {
        }

        public ItemTab FindByIdAndTab(Guid itemId, Guid tabId)
        {
            return Db.ItemTab.FirstOrDefault(item => item.TabId == tabId && item.ItemId == itemId);
        }
    }
}
