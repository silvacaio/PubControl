using DGPub.Domain.Core;
using System;
using System.Collections.Generic;

namespace DGPub.Domain.Tabs.Events
{
    public class UpdatedTabEvent : EventBase
    {
        public UpdatedTabEvent(Guid id, string customerName,
            IEnumerable<UpdateTabItemEvent> items, HashSet<string> alerts)
        {
            Id = id;
            CustomerName = customerName;
            Items = items ?? new UpdateTabItemEvent[0];
            Alerts = alerts ?? new HashSet<string>();
        }

        public UpdatedTabEvent(Guid id, string customerName) : this(id, customerName, null, null)
        {

        }

        public Guid Id { get; private set; }
        public string CustomerName { get; private set; }
        public IEnumerable<UpdateTabItemEvent> Items { get; private set; }
        public HashSet<string> Alerts { get; private set; }
    }
}

