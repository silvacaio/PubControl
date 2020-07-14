using DGPub.Domain.Core;
using System;

namespace DGPub.Domain.Tabs.Commands
{
    public class CloseTabCommand : Command
    {
        public CloseTabCommand(Guid tabId)
        {
            TabId = tabId;
        }

        public Guid TabId { get; set; }
        public override bool IsValid()
        {
            return TabId != null;
        }
    }
}
