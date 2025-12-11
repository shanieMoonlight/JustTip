using JustTip.Application.Domain.Entities.Employees;
using JustTip.Infrastructure.Persistence.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JustTip.Infrastructure.Persistence.Config;
internal class EmployeeConfig : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(JtPersistenceConfigConstants.Config.NAME_MAX_LENGTH);

        builder.Property(b => b.Description)
            .IsRequired(false)
            .HasMaxLength(JtPersistenceConfigConstants.Config.DESCRIPTION_MAX_LENGTH);

        builder.HasMany(b => b.Shifts)
            .WithOne(e => e.Employee)
            .OnDelete(DeleteBehavior.Cascade);

    }

}//Cls
