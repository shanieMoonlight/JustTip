using FluentValidation;

namespace JustTip.Application.Features.Roster.Cmd.RemoveShift;
public class RemoveShiftFromEmployeeCmdValidator : AbstractValidator<RemoveShiftFromEmployeeCmd>
{
    public RemoveShiftFromEmployeeCmdValidator()
    {
        RuleFor(p => p.EmployeeId)
        .NotEmpty()
              .WithMessage(JustTipMsgs.Error.IsRequired("{PropertyName}"));

        RuleFor(p => p.ShiftId)
        .NotEmpty()
              .WithMessage(JustTipMsgs.Error.IsRequired("{PropertyName}"));



    }
}//Cls
