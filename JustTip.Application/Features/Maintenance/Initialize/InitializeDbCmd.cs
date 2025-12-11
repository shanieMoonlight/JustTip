using JustTip.Application.Features.Employees.Cmd;




namespace JustTip.Application.Features.Maintenance.Initialize;
public record InitializeDbCmd() : IJtCommand_NonUow<List<EmployeeDto>>;
