namespace JustTip.Application.Features.Tips.Cmd.Create;
public record CreateTipCmd(CreateTipDto Dto) : IJtCommand<TipDto>;
