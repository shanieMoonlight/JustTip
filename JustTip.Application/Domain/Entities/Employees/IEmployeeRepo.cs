using JustTip.Application.Domain.Abstractions.Repos;

namespace JustTip.Application.Domain.Entities.Employees;

/// <summary>
/// Interface for Employee Repository
/// </summary>
public interface IEmployeeRepo : IGenCrudRepo<Employee>
{
    Task<IReadOnlyList<Employee>> GetAllByNameAsync(string? filter);


    /// <summary>
    /// Get the Employee with id, <paramref name="id"/> Including Shifts
    /// </summary>
    /// <param name="id">Entity identifier</param>
    /// <returns></returns>
    Task<Employee?> FirstOrDefaultByIdWithShiftsAsync(Guid? id);

}

