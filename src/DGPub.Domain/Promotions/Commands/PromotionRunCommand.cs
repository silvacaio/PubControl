using DGPub.Domain.Core;
using DGPub.Domain.Tabs;
using System;

namespace DGPub.Domain.Promotions.Commands
{
    public class PromotionRunCommand : Command
    {
        public PromotionRunCommand(Tab tab)
        {
            Tab = tab;
        }

        public Tab Tab { get; private set; }

        public override bool IsValid()
        {
            return Tab?.Items?.Count > 0;
        }
    }
}
