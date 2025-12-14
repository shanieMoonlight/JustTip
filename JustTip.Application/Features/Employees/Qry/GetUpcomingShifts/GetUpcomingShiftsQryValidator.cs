using FluentValidation;

namespace JustTip.Application.Features.Employees.Qry.GetUpcomingShifts;
public class GetUpcomingShiftsQryValidator : AbstractValidator<GetUpcomingShiftsQry>
{
    public GetUpcomingShiftsQryValidator()
    {
        RuleFor(p => p.EmployeeId)
        .NotEmpty()
              .WithMessage(JustTipMsgs.Error.IsRequired("{PropertyName}"));


    }
}//Cls
