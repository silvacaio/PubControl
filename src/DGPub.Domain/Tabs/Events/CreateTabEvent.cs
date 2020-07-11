using DGPub.Domain.Core;
using System;


namespace DGPub.Domain.Tabs.Event
{
    public class CreateTabEvent : EventBase
    {
        public CreateTabEvent(Guid id, string customerName)
        {
            Id = id;
            CustomerName = customerName;          
        }

        public Guid Id { get; private set; }
        public string CustomerName { get; private set; }
    }
}
