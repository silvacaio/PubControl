using DGPub.Domain.Core;
using System;
using System.Collections.Generic;

namespace DGPub.Domain.Tabs.Repositories
{
    public interface ITabRepository : IRepository<Tab>
    {
        IItemTabRepository ItemTabRepository { get; set; }

        IEnumerable<Tab> GetAllOpen(bool withItems);
        void RemoveAllItem(Guid tabId);
        Tab FindByIdWithItems(Guid id);
    }
}
