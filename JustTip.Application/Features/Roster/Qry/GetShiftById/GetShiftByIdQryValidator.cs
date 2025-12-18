using FluentValidation;
using JustTip.Application.Features.Roster.Cmd.RemoveShift;

namespace JustTip.Application.Features.Roster.Qry.GetShiftById;
public class GetShiftByIdQryValidator : AbstractValidator<GetShiftByIdQry>
{
    public GetShiftByIdQryValidator()
    {
        RuleFor(p => p.Id)
        .NotEmpty()
              .WithMessage(JustTipMsgs.Error.IsRequired("{PropertyName}"));

    }
}//Cls
