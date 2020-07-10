using DGPub.Domain.Core;
using DGPub.Domain.Tabs;

namespace DGPub.Domain.Promotions.Events
{
    public class PromotionEvent : Event<PromotionEvent>
    {
        public PromotionEvent(Tab tab)
        {
            Tab = tab;
        }

        public Tab Tab { get; private set; }        
    }
}
