using DGPub.Domain.Core;
using DGPub.Domain.Helpers.Items;
using DGPub.Domain.Items.Repositories;
using DGPub.Domain.Promotions.Handlers;
using DGPub.Domain.Tabs;
using DGPub.Domain.Tabs.Commands;
using DGPub.Domain.Tabs.Event;
using DGPub.Domain.Tabs.Events;
using DGPub.Domain.Tabs.Handlers;
using DGPub.Domain.Tabs.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DGPub.Application.Tabs.Handlers
{
    public class TabHandler : ITabHandler
    {
        private readonly ITabRepository _tabRepository;
        private readonly IPromotionHandler _promotionHandler;
        private readonly IItemCache _itemCache;

        public TabHandler(
            ITabRepository tabRepository,
            IUnitOfWork uow,
            IItemRepository itemRepository,
            IItemTabRepository itemTabRepository,
            IPromotionHandler promotionHandler,
            IItemCache itemCache) : base(uow, itemRepository, itemTabRepository, tabRepository)
        {
            _tabRepository = tabRepository;
            _promotionHandler = promotionHandler;
            _itemCache = itemCache;
        }

        public override async Task<Event<CreateTabEvent>> Handler(CreateTabCommand command)
        {
            if (!command.IsValid())
                return Event<CreateTabEvent>.CreateError("Informe todos os dados obrigatórios");

            var tab = Tab.TabFactory.Create(command.CustomerName);      

            _tabRepository.Add(tab);

            if (!Commit())
                return Event<CreateTabEvent>.CreateError("Falha ao adicionar comanda");

            return Event<CreateTabEvent>.CreateSuccess(new CreateTabEvent(tab.Id, tab.CustomerName));
        }

        public override async Task<Event<UpdatedTabEvent>> Handler(AddItemTabCommand command)
        {
            var result = await base.Handler(command);

            if (!result.Valid)
                return result;

            var resultPromotion = await _promotionHandler
                .Handler(new Domain.Promotions.Commands.PromotionCommand(result.Value.Id));

            return Event<UpdatedTabEvent>.CreateSuccess(CreateTabUpdateEvent(command.TabId, resultPromotion.Value?.Alerts));
        }

        public UpdatedTabEvent CreateTabUpdateEvent(Guid tabId, HashSet<string> alerts)
        {
            var tab = _tabRepository.FindByIdWithItems(tabId);
            var items = new HashSet<UpdateTabItemEvent>();

            foreach (var item in tab.Items)
            {
                items.Add(new UpdateTabItemEvent(_itemCache.GetName(item.Id), item.TotalItem(), item.HasDiscount()));
            }

            return new UpdatedTabEvent(tab.Id, tab.CustomerName, tab.Total, items, alerts);
        }

        public override Task<Event<UpdatedTabEvent>> Handler(ResetTabCommand command)
        {
            if (!command.IsValid())
                return Task.FromResult(Event<UpdatedTabEvent>.CreateError("Não foi possível resetar a comanda"));

            //remover todos os itens
            _tabRepository.RemoveAllItem(command.TabId);

            if (!Commit())
                return Task.FromResult(Event<UpdatedTabEvent>.CreateError("Não foi possível resetar a comanda"));

            var tabUpdated = _tabRepository.FindByIdWithItems(command.TabId);

            return Task.FromResult(Event<UpdatedTabEvent>.CreateSuccess(
                new UpdatedTabEvent(tabUpdated.Id, tabUpdated.CustomerName, tabUpdated.Total)));
        }

        public override Task<Event<InvoiceTabEvent>> Handler(CloseTabCommand command)
        {
            if (!command.IsValid())
                return Task.FromResult(Event<InvoiceTabEvent>.CreateError("Não foi possível fechar a comanda"));

            var tab = _tabRepository.FindByIdWithItems(command.TabId);
            tab.Close();
            _tabRepository.Update(tab);

            if (!Commit())
                return Task.FromResult(Event<InvoiceTabEvent>.CreateError("Falha ao fechar a comanda"));

            return Task.FromResult(Event<InvoiceTabEvent>.CreateSuccess(CreateInvoice(tab)));
        }

        public InvoiceTabEvent CreateInvoice(Tab tab)
        {
            var items = new HashSet<ItemInvoiceEvent>();
            foreach (var item in tab?.Items)
            {
                items.Add(new ItemInvoiceEvent(_itemCache.GetName(item.ItemId), item.UnitPrice, item.Discount));
            }

            return new InvoiceTabEvent(tab.Id, tab.CustomerName, tab.Total, tab.TotalDiscount, items);
        }
    }
}
