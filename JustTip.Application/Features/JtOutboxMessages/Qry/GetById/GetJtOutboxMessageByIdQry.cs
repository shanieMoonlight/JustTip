namespace JustTip.Application.Features.JtOutboxMessages.Qry.GetById;
public record GetJtOutboxMessageByIdQry(Guid? Id) : IJtQuery<JtOutboxMessageDto>;