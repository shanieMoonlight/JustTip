using JustTip.Application.Features.Roster.Qry.GetShiftById;

namespace JustTip.Application.Tests.Features.Roster.GetShiftById;

public class GetShiftByIdQryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenShiftMissing()
    {
        // Arrange
        var mockRepo = new Mock<IShiftRepo>();
        var id = Guid.NewGuid();
        mockRepo.Setup(r => r.FirstOrDefaultByIdWithEmployeeAsync(id)).ReturnsAsync((Shift?)null);

        var handler = new GetShiftByIdQryHandler(mockRepo.Object);

        // Act
        var res = await handler.Handle(new GetShiftByIdQry(id), CancellationToken.None);

        // Assert
        res.NotFound.ShouldBeTrue();
    }

    //--------------------------//

    [Fact]
    public async Task Handle_ShouldReturnDto_WhenShiftFound()
    {
        // Arrange
        var mockRepo = new Mock<IShiftRepo>();

        var employee = Employee.Create("Sam", "desc");
        var date = DateTime.UtcNow.Date.AddDays(1);
        var start = date.AddHours(9);
        var end = date.AddHours(17);
        var shift = Shift.Create(employee, date, start, end);

        mockRepo.Setup(r => r.FirstOrDefaultByIdWithEmployeeAsync(shift.Id)).ReturnsAsync(shift);

        var handler = new GetShiftByIdQryHandler(mockRepo.Object);

        // Act
        var res = await handler.Handle(new GetShiftByIdQry(shift.Id), CancellationToken.None);

        // Assert
        res.Succeeded.ShouldBeTrue();
        res.Value.ShouldNotBeNull();
        res.Value!.Id.ShouldBe(shift.Id);
        res.Value.EmployeeId.ShouldBe(employee.Id);
        res.Value.Date.ShouldBe(shift.Date);
        res.Value.StartTimeUtc.ShouldBe(shift.StartTimeUtc);
        res.Value.EndTimeUtc.ShouldBe(shift.EndTimeUtc);
    }
}
