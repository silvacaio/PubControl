using DGPub.Domain.Core;
using DGPub.Domain.Tabs.Events;
using System;
using System.Collections.Generic;

namespace DGPub.Domain.Tabs.Event
{
    public class InvoiceTabEvent : EventBase
    {
        public InvoiceTabEvent(Guid tabId,
            string customerName,
            decimal total,
            decimal totalDiscount,
            IEnumerable<ItemInvoiceEvent> items)
        {
            TabId = tabId;
            CustomerName = customerName;
            Total = total;
            TotalDiscount = totalDiscount;
            Items = items ?? new ItemInvoiceEvent[0];
        }

        public Guid TabId { get; private set; }
        public string CustomerName { get; private set; }
        public decimal Total { get; private set; }
        public decimal TotalDiscount { get; private set; }
        public IEnumerable<ItemInvoiceEvent> Items { get; private set; }
    }
}
