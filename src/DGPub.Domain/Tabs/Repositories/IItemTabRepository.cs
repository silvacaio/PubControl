using DGPub.Domain.Core;
using System;

namespace DGPub.Domain.Tabs.Repositories
{
    public interface IItemTabRepository : IRepository<ItemTab>
    {
        ItemTab FindByIdAndTab(Guid itemId, Guid tabId);
    }
}
