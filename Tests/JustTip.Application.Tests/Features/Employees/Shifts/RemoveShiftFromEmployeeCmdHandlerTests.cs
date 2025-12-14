using JustTip.Application.Features.Roster.Cmd.RemoveShift;
using static Jt.Application.Utility.Results.BasicResult;

namespace JustTip.Application.Tests.Features.Employees.Shifts;

public class RemoveShiftFromEmployeeCmdHandlerTests
{
    private readonly Mock<IEmployeeRepo> _mockRepo;
    private readonly RemoveShiftFromEmployeeCmdHandler _handler;

    //- - - - - - - - - - - - - -//

    public RemoveShiftFromEmployeeCmdHandlerTests()
    {
        _mockRepo = new Mock<IEmployeeRepo>();
        _handler = new RemoveShiftFromEmployeeCmdHandler(_mockRepo.Object);
    }

    //--------------------------// 

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenEmployeeDoesNotExist()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        var shiftId = Guid.NewGuid();
        _mockRepo.Setup(repo => repo.FirstOrDefaultByIdWithShiftsAsync(It.IsAny<Guid?>()))
                 .ReturnsAsync((Employee?)null);

        // Act
        var result = await _handler.Handle(new RemoveShiftFromEmployeeCmd(employeeId, shiftId), CancellationToken.None);

        // Assert
        result.ShouldBeOfType<GenResult<RemovedResult>>();
        result.Succeeded.ShouldBeFalse();
        result.Status.ShouldBe(ResultStatus.NotFound);
        result.Info.ShouldBe(JustTipMsgs.Error.NotFound<Employee>(employeeId));
    }

    //--------------------------// 

    [Fact]
    public async Task Handle_ShouldRemoveShift_WhenEmployeeHasShift()
    {
        // Arrange
        var employee = Employee.Create("Test Employee", "desc");

        // create a valid future shift
        var date = DateTime.UtcNow.AddDays(1).Date;
        var start = date.AddHours(9);
        var end = date.AddHours(17);
        var shift = employee.AddShift(date, start, end);

        _mockRepo.Setup(repo => repo.FirstOrDefaultByIdWithShiftsAsync(It.Is<Guid?>(g => g == employee.Id)))
                 .ReturnsAsync(employee);

        //var dto = new RemoveShiftDto(employee.Id, shift.Id);

        // Act
        var result = await _handler.Handle(new RemoveShiftFromEmployeeCmd(employee.Id, shift.Id), CancellationToken.None);

        // Assert
        result.ShouldBeOfType<GenResult<RemovedResult>>();
        result.Succeeded.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        // Ensure the returned DTO no longer contains the removed shift
        result.Value!.Id.ShouldBe(shift.Id);
    }

    //--------------------------// 

}
