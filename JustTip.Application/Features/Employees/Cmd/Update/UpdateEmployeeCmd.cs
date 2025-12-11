namespace JustTip.Application.Features.Employees.Cmd.Update;
public record UpdateEmployeeCmd(EmployeeDto Dto)  : IJtCommand<EmployeeDto>;
