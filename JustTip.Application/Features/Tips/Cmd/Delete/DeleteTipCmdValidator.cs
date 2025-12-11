using FluentValidation;

namespace JustTip.Application.Features.Tips.Cmd.Delete;
public class DeleteTipCmdValidator : AbstractValidator<DeleteTipCmd>
{
    public DeleteTipCmdValidator()
    {
        RuleFor(p => p.Id)
        .NotEmpty()
                .WithMessage(JustTipMsgs.Error.IsRequired("{PropertyName}"));

    }
}//Cls
