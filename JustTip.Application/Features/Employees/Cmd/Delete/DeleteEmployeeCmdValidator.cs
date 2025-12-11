using FluentValidation;
using JustTip.Application.Utility;

namespace JustTip.Application.Features.Employees.Cmd.Delete;
public class DeleteEmployeeCmdValidator : AbstractValidator<DeleteEmployeeCmd>
{
    public DeleteEmployeeCmdValidator()
    {
        RuleFor(p => p.Id)
        .NotEmpty()
                .WithMessage(JtMsgs.Error.IsRequired("{PropertyName}"));

    }
}//Cls
