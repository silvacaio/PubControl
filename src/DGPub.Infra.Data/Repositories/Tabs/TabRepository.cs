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
            return Db.Tab.AsNoTracking().Include(a => a.Items)
                .FirstOrDefault(a => a.Id == id);
        }

        public Tab FindById(Guid id)
        {
            return Db.Tab.AsNoTracking()
                .Include(a => a.Items)
                .FirstOrDefault(i => i.Id == id);
        }

        public override IEnumerable<Tab> GetAll()
        {
            return Db.Tab.AsNoTracking().Include(a => a.Items).ToList();
        }

        public IEnumerable<Tab> GetAllOpen(bool withItems)
        {
            if (withItems)
                return Db.Tab.AsNoTracking().Include(a => a.Items).ToList();

            return Db.Tab.AsNoTracking().Where(t => t.Open).ToList();
        }

        public void RemoveAllItem(Guid tabId)
        {
            ItemTabRepository.DeleteByTabId(tabId);
        }
    }
}
