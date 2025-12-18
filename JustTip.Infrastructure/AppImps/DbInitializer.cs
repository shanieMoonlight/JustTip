using JustTip.Application.Abstractions;
using JustTip.Application.Domain.Entities.Employees;
using JustTip.Application.Domain.Entities.Tips;
using JustTip.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JustTip.Infrastructure.AppImps;
internal class DbInitializer(JtDbContext _db) : IDbInitializer
{
    public async Task InitializeAsync(List<Employee> employees, List<Tip> tips)
    {
        await MigrateAsync();

        _db.OutboxMessages.RemoveRange(_db.OutboxMessages);
        _db.Employees.RemoveRange(_db.Employees);
        _db.Tips.RemoveRange(_db.Tips);
        await _db.SaveChangesAsync();
        await _db.Employees.AddRangeAsync(employees);
        await _db.Tips.AddRangeAsync(tips);
        await _db.SaveChangesAsync();
    }

    //-----------------------------//

    public async Task MigrateAsync()
    {
        if (_db.Database.IsSqlite())
            await _db.Database.EnsureCreatedAsync();
        else
            await _db.Database.MigrateAsync();
    }

}//Cls
