using DGPub.Domain.Core;
using DGPub.Domain.Promotions.Commands;
using DGPub.Domain.Promotions.Events;

namespace DGPub.Domain.Promotions.Handlers
{
    public interface IPromotionHandler : IHandler<PromotionCommand, PromotionEvent>
    {

    }
}
