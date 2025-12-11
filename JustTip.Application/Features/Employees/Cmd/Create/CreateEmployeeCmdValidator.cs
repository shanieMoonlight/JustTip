using FluentValidation;

namespace JustTip.Application.Features.Employees.Cmd.Create;
public class CreateEmployeeCmdValidator : AbstractValidator<CreateEmployeeCmd>
{
    public CreateEmployeeCmdValidator()
    {
        RuleFor(p => p.Dto)
        .NotEmpty()
              .WithMessage(JustTipMsgs.Error.IsRequired("{PropertyName}"));

    }
}//Cls
