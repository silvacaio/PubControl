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
    public class LimitJuicePromotion : IRunPromotionHandler
    {
        private readonly IItemRepository _itemRepository;
        private readonly IItemTabRepository _itemTabRepository;

        public readonly string _description = "Só é permitido 3 sucos por comanda";

        public LimitJuicePromotion(IItemRepository itemRepository, IItemTabRepository itemTabRepository)
        {
            _itemRepository = itemRepository;
            _itemTabRepository = itemTabRepository;
        }

        public Task<Event<ResultPromotionEvent>> Handler(PromotionRunCommand command)
        {
            if (!command.IsValid())
                return Task.FromResult(Event<ResultPromotionEvent>.CreateSuccess(new ResultPromotionEvent(false)));

            var juiceItem = _itemRepository.GetByName("suco");

            var juices = command.Tab.Items.Where(i => i.ItemId == juiceItem.Id).ToArray();
            var countJuices = juices.Count();
            if (countJuices <= 3)
                return Task.FromResult(Event<ResultPromotionEvent>.CreateSuccess(new ResultPromotionEvent(false)));

            for (int i = countJuices; i > 3; i--)
            {
                var juice = juices[i - 1];
                command.Tab.Items.Remove(juice);
                _itemTabRepository.Delete(juices[i - 1].Id);
            }

            return Task.FromResult(Event<ResultPromotionEvent>.CreateSuccess(new ResultPromotionEvent(true, _description)));
        }
    }
}
