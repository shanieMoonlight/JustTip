using FluentValidation;

namespace JustTip.Application.Features.Roster.Cmd.RemoveShift;
public class RemoveShiftFromEmployeeCmdValidator : AbstractValidator<RemoveShiftFromEmployeeCmd>
{
    public RemoveShiftFromEmployeeCmdValidator()
    {
        RuleFor(p => p.Dto)
        .NotEmpty()
              .WithMessage(JustTipMsgs.Error.IsRequired("{PropertyName}"));



    }
}//Cls
