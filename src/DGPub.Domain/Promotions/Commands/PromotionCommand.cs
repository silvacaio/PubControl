using DGPub.Domain.Core;
using System;

namespace DGPub.Domain.Promotions.Commands
{
    public class PromotionCommand : Command
    {
        public PromotionCommand(Guid tabId)
        {
            TabId = tabId;
        }

        public Guid TabId { get; private set; }

        public override bool IsValid()
        {
            return TabId != null;
        }
    }
}
