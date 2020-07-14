using DGPub.Domain.Items.Repositories;
using DGPub.Domain.Promotions.Handlers;
using DGPub.Domain.Tabs;
using DGPub.Domain.Tabs.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DGPub.Application.Promotions.Handlers
{
    public class BeerWithJuicePromotion : IPromotion
    {
        private readonly IItemRepository _itemRepository;
        private readonly IItemTabRepository _itemTabRepository;

        public BeerWithJuicePromotion(IItemRepository itemRepository, IItemTabRepository itemTabRepository)
        {
            _itemRepository = itemRepository;
            _itemTabRepository = itemTabRepository;
        }

        public readonly decimal _valuePromotionBeer = 3;

        protected override void CalculeInternal(Tab tab)
        {
            var beerItem = _itemRepository.GetByName("cerveja");
            var juiceItem = _itemRepository.GetByName("suco");

            var beers = tab.Items.Where(i => i.ItemId == beerItem.Id);

            if ((beers.Count() > 0 && beers.All(i => i.UnitPrice > _valuePromotionBeer)) &&
                tab.Items.Any(i => i.ItemId == juiceItem.Id))
            {
                var beer = tab.Items.FirstOrDefault(i => i.ItemId == beerItem.Id);
                beer = ItemTab.ItemTabFactory.Load(beer.Id, beer.ItemId, beer.TabId, _valuePromotionBeer);
                _itemTabRepository.Update(beer);
            }
        }

        protected override bool IsValid(Tab tab)
        {
            return tab?.Items?.Count > 0;
        }
    }
}
