using JustTip.Application.Features.Employees.Cmd;

namespace JustTip.Application.Features.Employees.Qry.GetAll;
public record GetAllEmployeesQry : IJtQuery<IEnumerable<EmployeeDto>>;
