using FluentValidation;

namespace JustTip.Application.Features.JtOutboxMessages.Cmd.Delete;
public class DeleteJtOutboxMessageCmdValidator : AbstractValidator<DeleteJtOutboxMessageCmd>
{
    public DeleteJtOutboxMessageCmdValidator()
    {
        RuleFor(p => p.Id)
        .NotEmpty()
                .WithMessage(JustTipMsgs.Error.IsRequired("{PropertyName}"));

    }
}//Cls
