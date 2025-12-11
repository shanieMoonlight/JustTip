using JustTip.Application.Domain.Entities.Employees;
using JustTip.Infrastructure.Persistence.Repos.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace JustTip.Infrastructure.Persistence.Repos;
internal class EmployeeRepo(JtDbContext db)
    : AGenCrudRepo<Employee>(db), IEmployeeRepo
{
    public async Task<Employee?> FirstOrDefaultByIdWithShiftsAsync(Guid? id)
    {
        if (id is null)
            return null;

        return await DbCtx.Employees
            .Include(e => e.Shifts)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    //----------------------//

    public async Task<IReadOnlyList<Employee>> GetAllByNameAsync(string? filter)
    {
        if (string.IsNullOrWhiteSpace(filter))
            return [];

        return await DbCtx.Employees
            .Where(e => e.Name.Contains(filter))
            .ToListAsync();
    }
}
