using FluentValidation;

namespace JustTip.Application.Features.Tips.Cmd.Update;
public class UpdateTipCmdValidator : AbstractValidator<UpdateTipCmd>
{
    public UpdateTipCmdValidator()
    {
        RuleFor(p => p.Dto)
        .NotEmpty()
                .WithMessage(JustTipMsgs.Error.IsRequired("{PropertyName}"));

    }
}//Cls
