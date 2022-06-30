using Domain.Aggregates.AirportAggregate;
using Domain.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : BaseEntityTypeConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property("FlightId")
                .IsRequired();

            builder.Property("NumberOfPassangers")
            .IsRequired();

        }
    }
}