using JustTip.Application.Domain.Entities.Common;
using JustTip.Application.Domain.Entities.Employees;
using JustTip.Application.Domain.Entities.OutboxMessages;
using JustTip.Application.Domain.Entities.Shifts;
using JustTip.Infrastructure.Persistence.Utils;
using Microsoft.EntityFrameworkCore;


namespace JustTip.Infrastructure.Persistence;

public class JtDbContext(DbContextOptions<JtDbContext> options) : DbContext(options)
{

    public DbSet<Employee> Employees => Set<Employee>();

    //TODO: maybe hide these behind Grid?
    public DbSet<Shift> Shifts => Set<Shift>();

    public DbSet<JtOutboxMessage> OutboxMessages => Set<JtOutboxMessage>();


    //------------------------//


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.HasDefaultSchema(JtPersistenceConfigConstants.Db.SCHEMA);
        modelBuilder.ApplyConfigurationsFromAssembly(JtInfrastructureAssemblyReference.Assembly);
        modelBuilder.ApplyClientSideIdGeneration();
    }


    //------------------------//


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var changedEntries = ChangeTracker.Entries<JtBaseDomainEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in changedEntries)
        {
            //TODO: decide if we want to set Created date here
            //if (entry.State == EntityState.Added)
            //    entry.Entity.SetCreated();
            if (entry.State == EntityState.Modified)
                entry.Entity.SetModified();

        }

        return await base.SaveChangesAsync(cancellationToken);

    }


}//Cls