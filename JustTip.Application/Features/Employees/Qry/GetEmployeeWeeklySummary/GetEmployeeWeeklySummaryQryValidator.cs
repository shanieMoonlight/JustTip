using FluentValidation;

namespace JustTip.Application.Features.Employees.Qry.GetEmployeeWeeklySummary;
public class GetEmployeeWeeklySummaryQryValidator : AbstractValidator<GetEmployeeWeeklySummaryQry>
{
    public GetEmployeeWeeklySummaryQryValidator()
    {
        RuleFor(p => p.EmployeeId)
        .NotEmpty()
              .WithMessage(JustTipMsgs.Error.IsRequired("{PropertyName}"));

    }
}//Cls
