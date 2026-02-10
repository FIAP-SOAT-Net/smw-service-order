using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMW.ServiceOrder.Domain.Entities;

namespace SMW.ServiceOrder.Infrastructure.Configurations;

public sealed class QuoteServiceConfiguration : IEntityTypeConfiguration<QuoteAvailableService>
{
    public void Configure(EntityTypeBuilder<QuoteAvailableService> builder)
    {
        builder.ToTable("quote_services");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();

        builder.Property(x => x.QuoteId).HasColumnName("quote_id").IsRequired();
        builder.Property(x => x.ServiceId).HasColumnName("service_id").IsRequired();
        builder.Property(x => x.Price).HasColumnName("price").HasColumnType("decimal(18,2)").IsRequired();

        builder.HasOne(x => x.Quote)
            .WithMany(x => x.Services)
            .HasForeignKey(x => x.QuoteId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Service)
            .WithMany()
            .HasForeignKey(x => x.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
