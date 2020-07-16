using DGPub.Domain.Core;
using System;
using System.Collections.Generic;

namespace DGPub.Domain.Tabs.Repositories
{
    public interface IItemTabRepository : IRepository<ItemTab>
    {
        ItemTab FindByIdAndTab(Guid itemId, Guid tabId);
        void DeleteByTabId(Guid tabId);
        List<ItemTab> FindByTab(Guid tabId);
    }
}
