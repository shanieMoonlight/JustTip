using FluentValidation;

namespace JustTip.Application.Features.Employees.Qry.GetEmployeeWeeklySummaryByWeek;
public class GetEmployeeWeeklySummaryByWeekQryValidator : AbstractValidator<GetEmployeeWeeklySummaryByWeekQry>
{
    public GetEmployeeWeeklySummaryByWeekQryValidator()
    {
        RuleFor(p => p.EmployeeId)
        .NotEmpty()
              .WithMessage(JustTipMsgs.Error.IsRequired("{PropertyName}"));


    }
}//Cls
