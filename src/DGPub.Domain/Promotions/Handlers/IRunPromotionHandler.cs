using DGPub.Domain.Core;
using DGPub.Domain.Promotions.Commands;
using DGPub.Domain.Promotions.Events;
using DGPub.Domain.Tabs;

namespace DGPub.Domain.Promotions.Handlers
{
    public interface IRunPromotionHandler : IHandler<PromotionRunCommand, Event<ResultPromotionEvent>>
    {
       
    }
}
