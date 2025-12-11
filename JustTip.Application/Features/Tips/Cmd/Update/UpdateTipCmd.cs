using JustTip.Application.Features.Tips;

namespace JustTip.Application.Features.Tips.Cmd.Update;
public record UpdateTipCmd(UpdateTipDto Dto)  : IJtCommand<TipDto>;
