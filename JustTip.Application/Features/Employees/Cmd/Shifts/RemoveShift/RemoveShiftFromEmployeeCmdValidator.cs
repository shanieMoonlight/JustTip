using FluentValidation;

namespace JustTip.Application.Features.Employees.Cmd.Shifts.RemoveShift;
public class RemoveShiftFromEmployeeCmdValidator : AbstractValidator<RemoveShiftFromEmployeeCmd>
{
    public RemoveShiftFromEmployeeCmdValidator()
    {
        RuleFor(p => p.Dto)
        .NotEmpty()
              .WithMessage(JtMsgs.Error.IsRequired("{PropertyName}"));



    }
}//Cls
