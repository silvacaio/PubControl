namespace DGPub.Domain.Tabs.Events
{
    public class ItemInvoiceEvent
    {
        public ItemInvoiceEvent(string name, decimal unitPrice, decimal discount)
        {
            Name = name;
            UnitPrice = unitPrice;
            Discount = discount;
        }

        public string Name { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
    }
}
