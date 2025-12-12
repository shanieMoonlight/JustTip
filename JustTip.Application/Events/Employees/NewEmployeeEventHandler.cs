using JustTip.Application.Domain.Entities;
using JustTip.Application.Domain.Entities.Employees.Events;
using JustTip.Application.Logging;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace JustTip.Application.Events.Employees;
public class NewEmployeeEventHandler(IJtUnitOfWork uow, ILogger<NewEmployeeEventHandler> logger)
    : INotificationHandler<EmployeeCreatedDomainEvent>
{


    public async Task Handle(EmployeeCreatedDomainEvent notification, CancellationToken cancellationToken)
    {

        try
        {
            var repo = uow.EmployeeRepo;
            var employeeId = notification.EmployeeId;

            var employee = await repo.FirstOrDefaultByIdAsync(employeeId);
            if (employee == null)
            {
                logger.LogError(
                    JtLoggingEvents.EventHandling.EMPLOYEES,
                    "{event} Failure.{info}",
                    nameof(EmployeeCreatedDomainEvent),
                    JustTipMsgs.Error.NotFound<Employee>(employeeId));
                return;
            }

            Console.WriteLine($"Sending email for Employee, Name: {employee.Name}, ID: {employeeId}");
            Debug.WriteLine($"Sending email for Employee, Name: {employee.Name}, ID: {employeeId}");
        }
        catch (Exception e)
        {
            logger.LogError(
                JtLoggingEvents.EventHandling.EMPLOYEES,
                e,
                "{event} Failure. Original error: {OriginalError}",
                nameof(EmployeeCreatedDomainEvent),
                e.ToString());
        }

    }


}//Cls
