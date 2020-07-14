using DGPub.Domain.Items;
using DGPub.Domain.Tabs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGPub.Infra.Data.Mappings
{
    public class ItemTabMap : IEntityTypeConfiguration<ItemTab>
    {
        public void Configure(EntityTypeBuilder<ItemTab> builder)
        {
            builder.Property(i => i.TabId)
                 .IsRequired();

            builder.Property(i => i.ItemId)
                 .IsRequired();

            builder.Property(i => i.Quantity)
                 .IsRequired();

            builder.Property(i => i.UnitPrice)
                 .IsRequired();
        }
    }
}
