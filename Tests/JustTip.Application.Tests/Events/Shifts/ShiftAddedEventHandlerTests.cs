using JustTip.Application.Events.Shifts;
using JustTip.Application.Domain.Entities.Employees.Events;
using JustTip.Application.Logging;
using JustTip.Testing.Utils.Logging;
using Microsoft.Extensions.Logging;

namespace JustTip.Application.Tests.Events.Shifts;

public class ShiftAddedEventHandlerTests
{
    private readonly Mock<IJtUnitOfWork> _uow = new();
    private readonly Mock<IEmployeeRepo> _employeeRepo = new();
    private readonly Mock<ILogger<ShiftAddedEventHandler>> _loggerMock = new();

    public ShiftAddedEventHandlerTests()
    {
        _uow.Setup(u => u.EmployeeRepo).Returns(_employeeRepo.Object);
    }

    [Fact]
    public async Task Handle_ShouldLogNotFound_WhenEmployeeMissing()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        _employeeRepo.Setup(r => r.FirstOrDefaultByIdWithShiftsAsync(employeeId)).ReturnsAsync((Employee?)null);

        var handler = new ShiftAddedEventHandler(_uow.Object, _loggerMock.Object);

        // Act
        await handler.Handle(new ShiftAddedDomainEvent(employeeId, Guid.NewGuid()), CancellationToken.None);

        // Assert
        _loggerMock.VerifyErrorLogging(JtLoggingEvents.EventHandling.SHIFTS, times: () => Times.Once());
    }

    [Fact]
    public async Task Handle_ShouldNotLogError_WhenEmployeeAndShiftExist()
    {
        // Arrange
        var employee = Employee.Create("Name", "Desc");
        var date = DateTime.UtcNow.AddDays(1).Date;
        var start = date.AddHours(9);
        var end = date.AddHours(17);
        var shift = employee.AddShift(date, start, end);

        _employeeRepo.Setup(r => r.FirstOrDefaultByIdWithShiftsAsync(employee.Id)).ReturnsAsync(employee);

        var handler = new ShiftAddedEventHandler(_uow.Object, _loggerMock.Object);

        // Act
        await handler.Handle(new ShiftAddedDomainEvent(employee.Id, shift.Id), CancellationToken.None);

        // Assert
        _loggerMock.VerifyErrorLogging(times: () => Times.Never());
    }

    [Fact]
    public async Task Handle_ShouldLogException_WhenRepoThrows()
    {
        // Arrange
        var id = Guid.NewGuid();
        var ex = new Exception("boom");
        _employeeRepo.Setup(r => r.FirstOrDefaultByIdWithShiftsAsync(id)).ThrowsAsync(ex);

        var handler = new ShiftAddedEventHandler(_uow.Object, _loggerMock.Object);

        // Act
        await handler.Handle(new ShiftAddedDomainEvent(id, Guid.NewGuid()), CancellationToken.None);

        // Assert
        _loggerMock.VerifyExceptionLogging(JtLoggingEvents.EventHandling.SHIFTS, ex, times: () => Times.Once());
    }
}
