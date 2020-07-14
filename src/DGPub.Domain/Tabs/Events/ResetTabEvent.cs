using DGPub.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DGPub.Domain.Tabs.Events
{
    public class ResetTabEvent : Event<UpdatedTabEvent>
    {
        public ResetTabEvent(Tab tab)
        {
            Tab = tab;
        }

        public Tab Tab { get; private set; }
    }

}
