using DGPub.Domain.Core;
using DGPub.Domain.Tabs;
using System.Collections.Generic;

namespace DGPub.Domain.Promotions.Events
{
    public class PromotionEvent : EventBase// Event<PromotionEvent>
    {
        public PromotionEvent()
        {
        }

        public PromotionEvent(HashSet<string> alerts)
        {
            Alerts = alerts;
        }

        public HashSet<string> Alerts { get; private set; }
    }
}
