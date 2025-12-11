using FluentValidation;

namespace JustTip.Application.Features.Tips.Cmd.Create;
public class CreateTipCmdValidator : AbstractValidator<CreateTipCmd>
{
    public CreateTipCmdValidator()
    {
        RuleFor(p => p.Dto)
        .NotEmpty()
              .WithMessage(JustTipMsgs.Error.IsRequired("{PropertyName}"));

    }
}//Cls
