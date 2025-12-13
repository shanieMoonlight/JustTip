using FluentValidation;

namespace JustTip.Application.Features.Employees.Cmd.Update;
public class UpdateEmployeeCmdValidator : AbstractValidator<UpdateEmployeeCmd>
{
    public UpdateEmployeeCmdValidator()
    {
        RuleFor(p => p.Dto)
        .NotEmpty()
                .WithMessage(JustTipMsgs.Error.IsRequired("{PropertyName}"));

            RuleFor(p => p.Id)
            .NotEmpty()
                .WithMessage(JustTipMsgs.Error.IsRequired("{PropertyName}"));
    }
}//Cls
