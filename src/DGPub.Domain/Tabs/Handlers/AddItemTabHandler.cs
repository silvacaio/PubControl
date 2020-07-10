using DGPub.Domain.Core;
using DGPub.Domain.Items.Repositories;
using DGPub.Domain.Tabs.Commands;
using DGPub.Domain.Tabs.Events;
using DGPub.Domain.Tabs.Repositories;
using System.Threading.Tasks;

namespace DGPub.Domain.Tabs.Handlers
{
    public class AddItemTabHandler : CommandHandler, IAddItemTabHandler
    {
        private readonly IItemRepository _itemRepository;
        private readonly IItemTabRepository _itemTabRepository;
        private readonly ITabRepository _tabRepository;

        public AddItemTabHandler(IUnitOfWork uow, IItemRepository itemRepository, IItemTabRepository itemTabRepository, ITabRepository tabRepository) : base(uow)
        {
            _itemRepository = itemRepository;
            _itemTabRepository = itemTabRepository;
            _tabRepository = tabRepository;
        }

        public virtual Task<UpdatedTabEvent> Handler(AddItemTabCommand command)
        {
            if (command.IsValid())
                return null;

            var itemTab = _itemTabRepository.FindByIdAndTab(command.ItemId, command.TabId);

            Event<None> result = itemTab != null
                ? UpdateItem(itemTab, command) : AddItem(command);

            if (!Commit())
                return null;

            var tab = _tabRepository.FindById(command.TabId);
            return Task.FromResult(new UpdatedTabEvent(tab));
        }

        protected Event<None> AddItem(AddItemTabCommand command)
        {
            var item = _itemRepository.FindById(command.ItemId);
            if (item == null)
                return Event<None>.CreateError("dasdasdas");

            var itemTab = ItemTab.ItemTabFactory.Create(command.TabId, command.ItemId, command.Quantity, item.Price);
            if (!itemTab.IsValid())
                return Event<None>.CreateError("dasdasdas");

            _itemTabRepository.Add(itemTab);

            return None.Create();
        }

        protected Event<None> UpdateItem(ItemTab itemTab, AddItemTabCommand command)
        {
            itemTab.AddQuantity(command.Quantity);
            if (!itemTab.IsValid())
                return Event<None>.CreateError("dasdasdas");

            _itemTabRepository.Update(itemTab);

            return None.Create();
        }


    }
}
