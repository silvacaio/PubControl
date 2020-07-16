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
    public class PromotionHandler : CommandHandler, IPromotionHandler
    {
        private readonly IEnumerable<IRunPromotionHandler> _promotions;
        private readonly ITabRepository _tabRepository;        

        public PromotionHandler(IEnumerable<IRunPromotionHandler> promotions, ITabRepository tabRepository, IUnitOfWork uow) : base(uow)
        {
            _promotions = promotions;
            _tabRepository = tabRepository;
        }

        public async Task<Event<PromotionEvent>> Handler(PromotionCommand command)
        {
            if (!command.IsValid())
                return Event<PromotionEvent>.CreateSuccess(new PromotionEvent());

            var tab = _tabRepository.FindByIdWithItems(command.TabId);
            if (_promotions == null)
                return Event<PromotionEvent>.CreateSuccess(new PromotionEvent());

            HashSet<string> promotionsAlert = new HashSet<string>();

            foreach (var promo in _promotions)
            {
                var result = await promo.Handler(new PromotionRunCommand(tab));
                if (!string.IsNullOrWhiteSpace(result.Value.Alert))
                    promotionsAlert.Add(result.Value.Alert);
            }

            if(!Commit())
                return Event<PromotionEvent>.CreateSuccess(new PromotionEvent(promotionsAlert));

            return Event<PromotionEvent>.CreateSuccess(new PromotionEvent(promotionsAlert));
        }
    }
}
