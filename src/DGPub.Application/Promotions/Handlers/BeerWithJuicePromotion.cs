using DGPub.Domain.Core;
using DGPub.Domain.Items.Repositories;
using DGPub.Domain.Promotions.Commands;
using DGPub.Domain.Promotions.Events;
using DGPub.Domain.Promotions.Handlers;
using DGPub.Domain.Tabs;
using DGPub.Domain.Tabs.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGPub.Application.Promotions.Handlers
{
    public class BeerWithJuicePromotion : IRunPromotionHandler
    {
        private readonly IItemRepository _itemRepository;
        private readonly IItemTabRepository _itemTabRepository;

        public readonly decimal _valuePromotionBeer = 3;
        public readonly string _description = "Na compra de 1 cerveja e 1 suco, a cerveja sai por R$3,00";
        public readonly bool _showAlert = false;

        public BeerWithJuicePromotion(IItemRepository itemRepository, IItemTabRepository itemTabRepository)
        {
            _itemRepository = itemRepository;
            _itemTabRepository = itemTabRepository;
        }

        public Task<Event<ResultPromotionEvent>> Handler(PromotionRunCommand command)
        {
            if (!command.IsValid())
                return Task.FromResult(Event<ResultPromotionEvent>.CreateSuccess(new ResultPromotionEvent(false)));

            var beerItem = _itemRepository.GetByName("cerveja");
            var juiceItem = _itemRepository.GetByName("suco");

            var beers = command.Tab.Items.Where(i => i.ItemId == beerItem.Id);

            if ((beers.Count() > 0 && beers.All(i => i.UnitPrice > _valuePromotionBeer)) &&
                command.Tab.Items.Any(i => i.ItemId == juiceItem.Id))
            {
                var beer = command.Tab.Items.FirstOrDefault(i => i.ItemId == beerItem.Id);
                beer = ItemTab.ItemTabFactory.LoadWithDiscount(beer.Id, beer.ItemId, beer.TabId, beer.UnitPrice, _valuePromotionBeer);
                _itemTabRepository.Update(beer);

                return Task.FromResult(Event<ResultPromotionEvent>.CreateSuccess(new ResultPromotionEvent(true)));
            }

            return Task.FromResult(Event<ResultPromotionEvent>.CreateSuccess(new ResultPromotionEvent(false)));
        }      
    }
}
