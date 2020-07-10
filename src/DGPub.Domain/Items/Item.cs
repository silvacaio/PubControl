using DGPub.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DGPub.Domain.Items
{
    public class Item : Entity<Item>
    {
        protected Item() { }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }
    }
}
