namespace JustTip.Application.Features.Employees.Qry.GetById;
public record GetEmployeeByIdQry(Guid? Id) : IJtQuery<EmployeeDto>;