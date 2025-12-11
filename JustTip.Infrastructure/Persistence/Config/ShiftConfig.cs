using JustTip.Application.Domain.Entities.Shifts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JustTip.Infrastructure.Persistence.Config;
internal class ShiftConfig : IEntityTypeConfiguration<Shift>
{
    public void Configure(EntityTypeBuilder<Shift> builder)
    {

        builder.HasKey(x => x.Id);



    }
}//Cls
