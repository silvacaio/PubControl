using DGPub.Domain.Core;
using System;

namespace DGPub.Domain.Tabs.Commands
{
    public class AddItemTabCommand : Command
    {
        public AddItemTabCommand(Guid tabId, Guid itemId, int quantity)
        {
            TabId = tabId;
            ItemId = itemId;
            Quantity = quantity;
        }

        public Guid TabId { get; private set; }
        public Guid ItemId { get; private set; }
        public int Quantity { get; private set; }

        public override bool IsValid()
        {
            return TabId != null && ItemId != null && Quantity > 0;
        }
    }
}
