using JustTip.Application.Domain.Entities;
using JustTip.Application.Domain.Entities.Employees.Events;
using JustTip.Application.Logging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JustTip.Application.Events.Shifts;
public class ShiftRemovedEventHandler(IJtUnitOfWork uow, ILogger<ShiftRemovedEventHandler> logger)
    : INotificationHandler<ShiftRemovedDomainEvent>
{

    public async Task Handle(ShiftRemovedDomainEvent notification, CancellationToken cancellationToken)
    {

        try
        {
            var repo = uow.EmployeeRepo;
            var employeeId = notification.EmployeeId;
            var shiftId = notification.ShiftId;

            var employee = await repo.FirstOrDefaultByIdWithShiftsAsync(employeeId);
            if (employee == null)
            {
                logger.LogError(
                    JtLoggingEvents.EventHandling.SHIFTS,
                    "{event} Failure.{info}",
                    nameof(ShiftRemovedDomainEvent),
                    JustTipMsgs.Error.NotFound<Employee>(employeeId));
                return;
            }

            var shift = employee.Shifts.Find(s => s.Id == shiftId);
            if (shift == null)
            {
                logger.LogError(
                    JtLoggingEvents.EventHandling.SHIFTS,
                    "{event} Failure.{info}",
                    nameof(ShiftRemovedDomainEvent),
                    JustTipMsgs.Error.NotFound<Shift>(shiftId));
                return;
            }

            Console.WriteLine($"Sending Shift REMOVED email for Employee, Name: {employee.Name}, ID: {employeeId}");
        }
        catch (Exception ex)
        {
            logger.LogError(
                JtLoggingEvents.EventHandling.SHIFTS,
                 ex,
                "{event} Failure. Original error: {OriginalError}",
                nameof(ShiftRemovedDomainEvent),
                ex.ToString());
        }

    }


}//Cls
