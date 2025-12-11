using FluentValidation;

namespace JustTip.Application.Features.Employees.Cmd.Shifts.AddShift;
public class AddShiftToEmployeeCmdValidator : AbstractValidator<AddShiftToEmployeeCmd>
{
    public AddShiftToEmployeeCmdValidator()
    {
        RuleFor(p => p.Dto)
        .NotEmpty()
              .WithMessage(JustTipMsgs.Error.IsRequired("{PropertyName}"));

    }
}//Cls
