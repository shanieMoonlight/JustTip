using FluentValidation;

namespace JustTip.Application.Features.Roster.Cmd.UpdateShift;
public class UpdateShiftCmdValidator : AbstractValidator<UpdateShiftCmd>
{
    public UpdateShiftCmdValidator()
    {
        RuleFor(p => p.Dto)
        .NotEmpty()
              .WithMessage(JustTipMsgs.Error.IsRequired("{PropertyName}"));


        RuleFor(p => p.Id)
        .NotEmpty()
              .WithMessage(JustTipMsgs.Error.IsRequired("{PropertyName}"));

        When(p => p.Dto is not null, () =>
        {

            // Cross-property rule: start time must be earlier than end time (compare TimeOfDay)
            // Special case: EndTime at midnight (00:00) is allowed (represents end of day)
            RuleFor(p => p.Dto).Custom((dto, context) =>
            {
                if (dto is null)
                    return;

                var startTod = dto.StartTimeUtc.TimeOfDay;
                var endTod = dto.EndTimeUtc.TimeOfDay;

                // Allow end-of-day midnight as a valid end time regardless of start
                if (endTod == TimeSpan.Zero)
                    return;

                if (startTod >= endTod)
                    context.AddFailure("Dto", "Start time must be earlier than end time.");
            });
        });

    }
}//Cls
