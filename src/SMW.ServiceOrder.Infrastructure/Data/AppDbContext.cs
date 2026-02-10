using Microsoft.EntityFrameworkCore;
using SMW.ServiceOrder.Domain.Entities;
using SMW.ServiceOrder.Infrastructure.Configurations;
using System.Diagnostics.CodeAnalysis;

namespace SMW.ServiceOrder.Infrastructure.Data;

[ExcludeFromCodeCoverage]
public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<Domain.Entities.ServiceOrder> ServiceOrders { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<QuoteAvailableService> QuoteServices { get; set; }
    public DbSet<QuoteSupply> QuoteSupplies { get; set; }
    public DbSet<ServiceOrderEvent> EventLogs { get; set; }
    public DbSet<AvailableServiceSupply> AvailableServiceSupplies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ServiceOrderConfiguration());
        modelBuilder.ApplyConfiguration(new QuoteConfiguration());
        modelBuilder.ApplyConfiguration(new QuoteServiceConfiguration());
        modelBuilder.ApplyConfiguration(new QuoteSupplyConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceOrderEventConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.LogTo(Console.WriteLine).EnableSensitiveDataLogging();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var modifiedEntries = ChangeTracker.Entries().Where(e => e is { State: EntityState.Modified, Entity: Entity });
        foreach (var entry in modifiedEntries) ((Entity) entry.Entity).MarkAsUpdated();
        return base.SaveChangesAsync(cancellationToken);
    }
}
