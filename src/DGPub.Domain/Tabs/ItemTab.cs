using DGPub.Domain.Core.Models;
using DGPub.Domain.Items;
using System;

namespace DGPub.Domain.Tabs
{
    public class ItemTab : Entity<ItemTab>
    {
        protected ItemTab() { }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public Guid ItemId { get; private set; }
        public virtual Item Item { get; private set; }
        public Guid TabId { get; private set; }

        public override bool IsValid()
        {
            return ItemId != null && Quantity > 0;
        }

        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }

        public static class ItemTabFactory
        {
            public static ItemTab Create(Guid tabId, Guid itemId, int quantity, decimal unitPrice)
            {
                return new ItemTab
                {
                    Id = Guid.NewGuid(),
                    TabId = tabId,
                    ItemId = itemId,
                    Quantity = quantity,
                    UnitPrice = unitPrice
                };
            }
        }
    }
}
