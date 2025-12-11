using JustTip.Application.Features.Employees;

namespace JustTip.Application.Features.Maintenance.Initialize;
public record InitializeDbCmd() : IJtCommand_NonUow<List<EmployeeDto>>;
