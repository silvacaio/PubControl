using DGPub.Domain.Tabs;
using DGPub.Domain.Tabs.Repositories;
using DGPub.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DGPub.Infra.Data.Repositories.Tabs
{
    public class TabRepository : Repository<Tab>, ITabRepository
    {
        public TabRepository(DGPubContext context, IItemTabRepository itemTabRepository) : base(context)
        {
            ItemTabRepository = itemTabRepository;
        }

        public IItemTabRepository ItemTabRepository { get; set; }

        public Tab FindByIdWithItems(Guid id)
        {
            var tab = Db.Tab.AsNoTracking()
                .FirstOrDefault(a => a.Id == id);


            var items = Db.ItemTab.AsNoTracking()
                 .Where(a => a.TabId == id).ToArray();

            tab.Items = new List<ItemTab>();
            foreach (var item in items)
            {
                tab.Items.Add(item);
            }

            return tab;

        }

        public Tab FindOpenByIdWithItems(Guid id)
        {
            var tab = Db.Tab.AsNoTracking()
                 .FirstOrDefault(a => a.Id == id && a.Open);

            if (tab == null)
                return tab;

            var items = Db.ItemTab.AsNoTracking()
                 .Where(a => a.TabId == id).ToArray();

            tab.Items = new List<ItemTab>();
            foreach (var item in items)
            {
                tab.Items.Add(item);
            }

            return tab;
        }

        public override IEnumerable<Tab> GetAll()
        {
            return Db.Tab.Include(a => a.Items).AsNoTracking().ToList();
        }

        public IEnumerable<Tab> GetAllOpen(bool withItems)
        {
            if (withItems)
                return Db.Tab.Include(a => a.Items).AsNoTracking().ToList();

            return Db.Tab.Where(t => t.Open).AsNoTracking().ToList();
        }

        public void RemoveAllItem(Guid tabId)
        {
            ItemTabRepository.DeleteByTabId(tabId);
        }
    }
}
