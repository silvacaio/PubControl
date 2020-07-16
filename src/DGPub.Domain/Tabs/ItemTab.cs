using DGPub.Domain.Core.Models;
using DGPub.Domain.Items;
using System;

namespace DGPub.Domain.Tabs
{
    public class ItemTab : Entity<ItemTab>
    {
        protected ItemTab() { }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public Guid ItemId { get; private set; }
        public virtual Item Item { get; private set; }
        public Guid TabId { get; private set; }
        public Tab Tab { get; private set; }

        public override bool IsValid()
        {
            return ItemId != null;
        }

        public decimal TotalItem()
        {
            return Math.Max(UnitPrice - Discount, 0);
        }

        public bool HasDiscount()
        {
            return Discount > 0;
        }             

        public static class ItemTabFactory
        {
            public static ItemTab Create(Guid tabId, Guid itemId, decimal unitPrice)
            {
                return new ItemTab
                {
                    Id = Guid.NewGuid(),
                    TabId = tabId,
                    ItemId = itemId,
                    UnitPrice = unitPrice
                };
            }

            public static ItemTab LoadWithDiscount(Guid id, Guid itemId, Guid tabId, decimal unitPrice, decimal discount)
            {
                return new ItemTab
                {
                    Id = id,
                    TabId = tabId,
                    ItemId = itemId,
                    UnitPrice = unitPrice,
                    Discount = discount
                };
            }
        }
    }
}
