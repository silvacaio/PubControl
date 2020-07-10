using DGPub.Domain.Core;
using DGPub.Domain.Promotions.Commands;
using DGPub.Domain.Promotions.Events;
using DGPub.Domain.Promotions.Handlers;
using DGPub.Domain.Tabs.Event;
using DGPub.Domain.Tabs.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DGPub.Application.Promotions.Handlers
{
    public class PromotionHandler : CommandHandler,
        IPromotionHandler
    {
      private readonly IEnumerable<IPromotion> _promotions;
        private readonly ITabRepository _tabRepository;
        //, IEnumerable<IPromotion> promotions
        public PromotionHandler(IUnitOfWork uow, ITabRepository tabRepository) : base(uow)
        {
         ///   _promotions = promotions;
            _tabRepository = tabRepository;
        }

        public Task<PromotionEvent> Handler(PromotionCommand command)
        {
            if (!command.IsValid())
                return null;

            var tab = _tabRepository.FindById(command.TabId);
            if (_promotions == null)
                return Task.FromResult(new PromotionEvent(tab));

            //foreach (var promo in _promotions)
            //{
            //    promo.Calcule(tab);
            //}

            return Task.FromResult(new PromotionEvent(tab));
        }
    }
}
