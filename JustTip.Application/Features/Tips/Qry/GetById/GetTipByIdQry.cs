namespace JustTip.Application.Features.Tips.Qry.GetById;
public record GetTipByIdQry(Guid? Id) : IJtQuery<TipDto>;