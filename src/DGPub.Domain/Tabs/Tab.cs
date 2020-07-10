using DGPub.Domain.Core.Models;
using DGPub.Domain.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace DGPub.Domain.Tabs
{
    public class Tab : Entity<Tab>
    {
        protected Tab() { }
        public string CustomerName { get; private set; }

        public virtual ICollection<ItemTab> Items { get; set; }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(CustomerName);
        }

        public static class TabFactory
        {
            public static Tab Create(string customerName)
            {
                return new Tab { Id = Guid.NewGuid(), CustomerName = customerName, Items = new HashSet<ItemTab>() };
            }
        }       
    }
}
