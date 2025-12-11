using JustTip.Application.Features.Tips;

namespace JustTip.Application.Features.Tips.Qry.GetAll;
public record GetAllTipsQry : IJtQuery<IEnumerable<TipDto>>;
