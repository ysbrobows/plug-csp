using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlugApi.Entities;

namespace PlugApi.Data.Mappings
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Created);

            builder.Property(e => e.Updated);

            builder.Property(e => e.CustomerName)
                .HasMaxLength(255);

            builder.Property(e => e.ApiKey)
                .HasMaxLength(36)
                .IsUnicode(false);

            builder.Property(e => e.IsActive)
                .IsRequired();

            builder.Property(e => e.InstanceDatabaseId)
                .IsRequired(false);

            builder.Property(e => e.SchemaName)
                .HasMaxLength(255);
        }
    }
}
