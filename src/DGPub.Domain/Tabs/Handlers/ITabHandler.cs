using DGPub.Domain.Core;
using DGPub.Domain.Items.Repositories;
using DGPub.Domain.Tabs.Commands;
using DGPub.Domain.Tabs.Event;
using DGPub.Domain.Tabs.Events;
using DGPub.Domain.Tabs.Repositories;
using System.Threading.Tasks;

namespace DGPub.Domain.Tabs.Handlers
{
    public abstract class ITabHandler : 
        AddItemTabHandler,
        ICreateTabHandler,
        IResetTabHandler,
        ICloseTabHandler
    {
        public ITabHandler(IUnitOfWork uow, IItemRepository itemRepository, IItemTabRepository itemTabRepository, ITabRepository tabRepository)
            : base(uow, itemRepository, itemTabRepository, tabRepository)
        {
        }

        public abstract Task<Event<CreateTabEvent>> Handler(CreateTabCommand command);

        public abstract Task<Event<UpdatedTabEvent>> Handler(ResetTabCommand command);

        public abstract Task<Event<InvoiceTabEvent>> Handler(CloseTabCommand command);
    }
}
