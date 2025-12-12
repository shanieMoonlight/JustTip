using JustTip.Application.Domain.Entities;
using JustTip.Application.Domain.Entities.Employees.Events;
using JustTip.Application.Logging;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace JustTip.Application.Events.Employees;
public class UpdateEmployeeEventHandler(IJtUnitOfWork uow, ILogger<UpdateEmployeeEventHandler> logger)
    : INotificationHandler<EmployeeUpdatedDomainEvent>
{


    public async Task Handle(EmployeeUpdatedDomainEvent notification, CancellationToken cancellationToken)
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
                    nameof(EmployeeUpdatedDomainEvent),
                    JustTipMsgs.Error.NotFound<Employee>(employeeId));
                return;
            }

            Console.WriteLine($"Do some processing for Employee, Name: {employee.Name}, ID: {employeeId}");
            Debug.WriteLine($"Do some processing for Employee, Name: {employee.Name}, ID: {employeeId}");
        }
        catch (Exception e)
        {
            logger.LogError(
                JtLoggingEvents.EventHandling.EMPLOYEES,
                e,
                "{event} Failure. Original error: {OriginalError}",
                nameof(UpdateEmployeeEventHandler),
                e.ToString());
        }

    }


}//Cls
