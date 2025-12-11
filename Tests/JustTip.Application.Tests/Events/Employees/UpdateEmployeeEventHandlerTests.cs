namespace JustTip.Application.Tests.Events.Employees;

public class UpdateEmployeeEventHandlerTests
{
    private readonly Mock<IJtUnitOfWork> _uow = new();
    private readonly Mock<IEmployeeRepo> _employeeRepo = new();
    private readonly Mock<ILogger<UpdateEmployeeEventHandler>> _loggerMock = new();

    public UpdateEmployeeEventHandlerTests()
    {
        _uow.Setup(u => u.EmployeeRepo).Returns(_employeeRepo.Object);
    }

    //--------------------------// 

    [Fact]
    public async Task Handle_ShouldLogNotFound_WhenEmployeeMissing()
    {
        // Arrange
        var id = Guid.NewGuid();
        _employeeRepo.Setup(r => r.FirstOrDefaultByIdAsync(id)).ReturnsAsync((Employee?)null);

        var handler = new UpdateEmployeeEventHandler(_uow.Object, _loggerMock.Object);

        // Act
        await handler.Handle(new EmployeeUpdatedDomainEvent(id), CancellationToken.None);

        // Assert
        _loggerMock.VerifyErrorLogging(JtLoggingEvents.EventHandling.EMPLOYEES, times: () => Times.Once());
    }

    //--------------------------// 

    [Fact]
    public async Task Handle_ShouldNotLogError_WhenEmployeeExists()
    {
        // Arrange
        var employee = EmployeeDataFactory.Create(name: "Name", description: "Desc");
        _employeeRepo.Setup(r => r.FirstOrDefaultByIdAsync(employee.Id)).ReturnsAsync(employee);

        var handler = new UpdateEmployeeEventHandler(_uow.Object, _loggerMock.Object);

        // Act
        await handler.Handle(new EmployeeUpdatedDomainEvent(employee.Id), CancellationToken.None);

        // Assert
        _loggerMock.VerifyErrorLogging(times: () => Times.Never());
    }

    //--------------------------// 

    [Fact]
    public async Task Handle_ShouldLogException_WhenRepoThrows()
    {
        // Arrange
        var id = Guid.NewGuid();
        var ex = new Exception("boom");
        _employeeRepo.Setup(r => r.FirstOrDefaultByIdAsync(id)).ThrowsAsync(ex);

        var handler = new UpdateEmployeeEventHandler(_uow.Object, _loggerMock.Object);

        // Act
        await handler.Handle(new EmployeeUpdatedDomainEvent(id), CancellationToken.None);

        // Assert
        _loggerMock.VerifyExceptionLogging(JtLoggingEvents.EventHandling.EMPLOYEES, ex, times: () => Times.Once());
    }
}
