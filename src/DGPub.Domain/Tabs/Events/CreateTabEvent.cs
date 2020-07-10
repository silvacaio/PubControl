using DGPub.Domain.Core;
using System;


namespace DGPub.Domain.Tabs.Event
{
    public class CreateTabEvent : Event<CreateTabEvent>
    {
        public Guid Id { get; private set; }
        public string CustomerName { get; private set; }
    }
}
