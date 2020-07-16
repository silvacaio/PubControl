using DGPub.Domain.Core;
using DGPub.Domain.Items.Repositories;
using DGPub.Domain.Promotions.Commands;
using DGPub.Domain.Promotions.Events;
using DGPub.Domain.Promotions.Handlers;
using DGPub.Domain.Tabs;
using DGPub.Domain.Tabs.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace DGPub.Application.Promotions.Handlers
{
    public class FreeWaterWhenBrandyAndBeerPromotion : IRunPromotionHandler
    {
        private readonly IItemRepository _itemRepository;
        private readonly IItemTabRepository _itemTabRepository;

        public readonly string _description = "Na compra 3 conhaques mais 2 cervejas, poderá pedir uma água de graça";
        public readonly bool _showAlert = false;

        public FreeWaterWhenBrandyAndBeerPromotion(IItemRepository itemRepository, IItemTabRepository itemTabRepository)
        {
            _itemRepository = itemRepository;
            _itemTabRepository = itemTabRepository;
        }

        public Task<Event<ResultPromotionEvent>> Handler(PromotionRunCommand command)
        {
            if (!command.IsValid())
                return Task.FromResult(Event<ResultPromotionEvent>.CreateSuccess(new ResultPromotionEvent(false)));

            var beerItem = _itemRepository.GetByName("cerveja");
            var brandyItem = _itemRepository.GetByName("conhaque");
            var water = _itemRepository.GetByName("água");

            //já ganhou a água de graça
            if (command.Tab.Items.Any(a => a.ItemId == water.Id && a.TotalItem() == 0))
                return Task.FromResult(Event<ResultPromotionEvent>.CreateSuccess(new ResultPromotionEvent(false)));

            var hasThreeBrandy = command.Tab.Items.Where(i => i.ItemId == brandyItem.Id).Count() >= 3;
            var hasTwoBeer = command.Tab.Items.Where(i => i.ItemId == beerItem.Id).Count() >= 2;

            if (hasThreeBrandy && hasTwoBeer)
            {
                var waterTab = command.Tab.Items.FirstOrDefault(i => i.ItemId == water.Id);
                if (waterTab != null)
                    return CreateFreeWater(waterTab);

                return Task.FromResult(Event<ResultPromotionEvent>.CreateSuccess(new ResultPromotionEvent(false, _description)));
            }

            return Task.FromResult(Event<ResultPromotionEvent>.CreateSuccess(new ResultPromotionEvent(false)));
        }

        private Task<Event<ResultPromotionEvent>> CreateFreeWater(ItemTab waterTab)
        {
            waterTab = ItemTab.ItemTabFactory.LoadWithDiscount(waterTab.Id, waterTab.ItemId, waterTab.TabId, waterTab.UnitPrice, waterTab.UnitPrice);
            _itemTabRepository.Update(waterTab);

            return Task.FromResult(Event<ResultPromotionEvent>.CreateSuccess(new ResultPromotionEvent(true)));
        }
    }
}
