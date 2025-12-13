namespace JustTip.Application.Features.Employees.Cmd.Update;
public record UpdateEmployeeCmd(Guid Id, EmployeeDto Dto)  : IJtCommand<EmployeeDto>;
