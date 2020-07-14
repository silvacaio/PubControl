namespace DGPub.Domain.Tabs.Events
{
    public class UpdateTabItemEvent
    {
        public UpdateTabItemEvent(string name, decimal value, bool hasDiscount)
        {
            this.Name = name;
            this.Value = value;
            this.HasDiscount = hasDiscount;
        }

        public string Name { get; private set; }
        public decimal Value { get; private set; }
        public bool HasDiscount { get; private set; }
    }
}
