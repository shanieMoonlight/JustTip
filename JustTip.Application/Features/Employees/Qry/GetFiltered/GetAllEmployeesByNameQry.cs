using JustTip.Application.Features.Employees.Cmd;

namespace JustTip.Application.Features.Employees.Qry.GetFiltered;
public record class  GetAllEmployeesByNameQry(string? Filter) : IJtQuery<IEnumerable<EmployeeDto>>;
