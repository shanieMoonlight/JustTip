using JustTip.Application.Mediatr.CQRS;




namespace JustTip.Application.Features.Employees.Cmd.Create;
public record CreateEmployeeCmd(EmployeeDto Dto) : IJtCommand<EmployeeDto>;
