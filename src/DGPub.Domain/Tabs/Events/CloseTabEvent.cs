using DGPub.Domain.Core;
using System;


namespace DGPub.Domain.Tabs.Event
{
    public class CloseTabEvent : EventBase
    {
        public CloseTabEvent(Guid tabId)
        {
            TabId = tabId;
        }

        public Guid TabId { get; private set; }
    }
}
