using DGPub.Domain.Tabs;
using DGPub.Domain.Tabs.Repositories;
using DGPub.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DGPub.Infra.Data.Repositories.Tabs
{
    public class ItemTabRepository : Repository<ItemTab>, IItemTabRepository
    {
        public ItemTabRepository(DGPubContext context) : base(context)
        {
        }

        public override void Update(ItemTab obj)
        {
            Delete(obj.Id);
            Add(obj);
        }

        public void DeleteByTabId(Guid tabId)
        {
            Db.ItemTab.RemoveRange(Db.ItemTab.Where(x => x.TabId == tabId));
        }

        public ItemTab FindByIdAndTab(Guid itemId, Guid tabId)
        {
            return Db.ItemTab.AsNoTracking().FirstOrDefault(item => item.TabId == tabId && item.ItemId == itemId);
        }

        public List<ItemTab> FindByTab(Guid tabId)
        {
            return Db.ItemTab.AsNoTracking().Where(item => item.TabId == tabId).ToList();
        }
    }
}
