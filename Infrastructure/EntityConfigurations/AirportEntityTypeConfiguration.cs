using Domain.Aggregates.AirportAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    public class AirportEntityTypeConfiguration : BaseEntityTypeConfiguration<Airport>
    {
        public override void Configure(EntityTypeBuilder<Airport> builder)
        {
            base.Configure(builder);
            
            builder.Property("Code")
                .IsRequired();

            builder.HasIndex(o => o.Code).IsUnique();

            builder.Property("Name")
                .IsRequired();
        }
    }
}