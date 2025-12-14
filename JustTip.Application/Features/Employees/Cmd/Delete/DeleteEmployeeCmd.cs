namespace JustTip.Application.Features.Employees.Cmd.Delete;
public record DeletedResult(Guid DeletedId);
public record DeleteEmployeeCmd(Guid Id) :IJtCommand<DeletedResult>;
