using JustTip.Application.Abstractions;
using JustTip.Application.Domain.Entities.Employees;
using JustTip.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JustTip.Infrastructure.AppImps;
internal class DbInitializer(JtDbContext _db) : IDbInitializer
{
    public async Task InitializeAsync(List<Employee> employees)
    {
        await MigrateAsync();
        await _db.Employees.AddRangeAsync(employees);
        await _db.SaveChangesAsync();
    }

    //-----------------------------//

    public async Task MigrateAsync() => 
        await _db.Database
            .MigrateAsync();

}//Cls
