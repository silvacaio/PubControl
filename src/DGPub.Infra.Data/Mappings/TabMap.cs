using DGPub.Domain.Tabs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGPub.Infra.Data.Mappings
{
    public class TabMap : IEntityTypeConfiguration<Tab>
    {
        public void Configure(EntityTypeBuilder<Tab> builder)
        {
            builder.Property(t => t.CustomerName)              
              .IsRequired();

            builder.HasMany(t => t.Items)
                .WithOne(i => i.Tab)
                .IsRequired();
        }
    }
}
