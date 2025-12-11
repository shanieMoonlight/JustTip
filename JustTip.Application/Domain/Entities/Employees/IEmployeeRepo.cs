using JustTip.Application.Domain.Abstractions.Repos;

namespace JustTip.Application.Domain.Entities.Employees;

/// <summary>
/// Interface for Grid Repository
/// </summary>
public interface IEmployeeRepo : IGenCrudRepo<Employee>
{
    Task<IReadOnlyList<Employee>> GetAllByNameAsync(string? filter);
}

