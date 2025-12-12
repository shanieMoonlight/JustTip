using FluentValidation;

namespace JustTip.Application.Features.Roster.Cmd.AddShift;
public class AddShiftToEmployeeCmdValidator : AbstractValidator<AddShiftToEmployeeCmd>
{
    public AddShiftToEmployeeCmdValidator()
    {
        RuleFor(p => p.Dto)
        .NotEmpty()
              .WithMessage(JustTipMsgs.Error.IsRequired("{PropertyName}"));

    }
}//Cls
