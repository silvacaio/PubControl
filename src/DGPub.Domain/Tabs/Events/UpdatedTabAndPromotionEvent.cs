using DGPub.Domain.Core;
using System;
using System.Collections.Generic;

namespace DGPub.Domain.Tabs.Events
{
    public class UpdatedTabAndPromotionEvent : EventBase
    {
        public UpdatedTabAndPromotionEvent(Guid id, string customerName, decimal total,
            IEnumerable<UpdateTabItemEvent> items, HashSet<string> alerts)
        {
            Id = id;
            CustomerName = customerName;
            Total = total;
            Items = items ?? new UpdateTabItemEvent[0];
            Alerts = alerts ?? new HashSet<string>();
        }
        
        public UpdatedTabAndPromotionEvent(Guid id, string customerName, decimal total) : this(id, customerName, total, null, null)
        {
           
        }

        public Guid Id { get; private set; }
        public string CustomerName { get; private set; }
        public decimal Total { get; private set; }
        public IEnumerable<UpdateTabItemEvent> Items { get; private set; }
        public HashSet<string> Alerts { get; private set; }
    }
}

