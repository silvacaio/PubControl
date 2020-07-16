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

        public virtual Task<Event<UpdatedTabEvent>> Handler(AddItemTabCommand command)
        {
            if (!command.IsValid())
                return Task.FromResult(Event<UpdatedTabEvent>.CreateError("Informe todos os dados obrigatórios"));

            var result = AddItem(command);
            if (!result.Valid)
                return Task.FromResult(Event<UpdatedTabEvent>.CreateError("Falha ao adicionar item na comanda"));

            if (!Commit())
                return Task.FromResult(Event<UpdatedTabEvent>.CreateError("Falha ao adicionar item na comanda"));

            var tab = _tabRepository.FindByIdWithItems(command.TabId);
            return Task.FromResult(Event<UpdatedTabEvent>.CreateSuccess(new UpdatedTabEvent(tab.Id, tab.CustomerName, tab.Total)));
        }

        public Event<None> AddItem(AddItemTabCommand command)
        {
            var item = _itemRepository.FindById(command.ItemId);
            if (item == null)
                return Event<None>.CreateError("Item não encontrado");

            var itemTab = ItemTab.ItemTabFactory
                .Create(command.TabId, command.ItemId, item.Price);

            _itemTabRepository.Add(itemTab);

            return Event<None>.CreateSuccess(None.Create());
        }
    }
}
