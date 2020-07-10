using DGPub.Domain.Tabs;
using DGPub.Domain.Tabs.Repositories;
using DGPub.Infra.Data.Context;

namespace DGPub.Infra.Data.Repositories.Tabs
{
    public class TabRepository : Repository<Tab>, ITabRepository
    {
        public TabRepository(DGPubContext context, IItemTabRepository itemTabRepository) : base(context)
        {
            ItemTabRepository = itemTabRepository;
        }

        public IItemTabRepository ItemTabRepository { get; set; }
    }
}
