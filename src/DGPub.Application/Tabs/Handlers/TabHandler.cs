using DGPub.Domain.Core;
using DGPub.Domain.Items.Repositories;
using DGPub.Domain.Promotions.Handlers;
using DGPub.Domain.Tabs;
using DGPub.Domain.Tabs.Commands;
using DGPub.Domain.Tabs.Event;
using DGPub.Domain.Tabs.Events;
using DGPub.Domain.Tabs.Handlers;
using DGPub.Domain.Tabs.Repositories;
using System.Threading.Tasks;

namespace DGPub.Application.Tabs.Handlers
{
    public class TabHandler : AddItemTabHandler,
        ICreateTabHandler
    {        
        private readonly ITabRepository _tabRepository;
        private readonly IPromotionHandler _promotionHandler;

        public TabHandler(            
            ITabRepository tabRepository,
            IUnitOfWork uow,
            IItemRepository itemRepository,
            IItemTabRepository itemTabRepository,
            IPromotionHandler promotionHandler) : base(uow, itemRepository, itemTabRepository, tabRepository)
        {            
            _tabRepository = tabRepository;
            _promotionHandler = promotionHandler;
        }

        public Task<CreateTabEvent> Handler(CreateTabCommand command)
        {
            //TODO 
            if (!command.IsValid())
                return null;

            var tab = Tab.TabFactory.Create(command.CustomerName);

            //TODO 
            if (!tab.IsValid())
                return null;

            _tabRepository.Add(tab);

            if (Commit())
            { }

            return null;
        }

        public override async Task<UpdatedTabEvent> Handler(AddItemTabCommand command)
        {
            var result = await base.Handler(command);
            if (!result.Valid)
                return result;

            var resultPromotion = await _promotionHandler.Handler(new Domain.Promotions.Commands.PromotionCommand(result.Tab.Id));

            return new UpdatedTabEvent(resultPromotion.Tab);
        }
    }
}
