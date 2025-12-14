using JustTip.Application.Features.Roster;
using JustTip.Application.Features.Roster.Cmd.AddShift;
using JustTip.Application.Features.Roster.Cmd.UpdateShift;
using JustTip.Testing.Utils.DataFactories.Dtos;
using static Jt.Application.Utility.Results.BasicResult;

namespace JustTip.Application.Tests.Features.Employees.Shifts;

public class AddShiftToEmployeeCmdHandlerTests
{
    private readonly Mock<IEmployeeRepo> _mockRepo;
    private readonly AddShiftToEmployeeCmdHandler _handler;

    //- - - - - - - - - - - - - - //

    public AddShiftToEmployeeCmdHandlerTests()
    {
        _mockRepo = new Mock<IEmployeeRepo>();
        _handler = new AddShiftToEmployeeCmdHandler(_mockRepo.Object);
    }

    //--------------------------// 

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenEmployeeDoesNotExist()
    {
        // Arrange
        var requestDto = new AddShiftDto();
        _mockRepo.Setup(repo => repo.FirstOrDefaultByIdAsync(It.IsAny<Guid?>()))
                 .ReturnsAsync((Employee?)null);

        // Act
        var result = await _handler.Handle(new AddShiftToEmployeeCmd(requestDto), CancellationToken.None);

        // Assert
        result.ShouldBeOfType<GenResult<ShiftDto>>();
        result.Succeeded.ShouldBeFalse();
        result.Status.ShouldBe(ResultStatus.NotFound);
        result.Info.ShouldBe(JustTipMsgs.Error.NotFound<Employee>(requestDto.EmployeeId));
    }

    //--------------------------// 

    [Fact]
    public async Task Handle_ShouldAddShift_WhenEmployeeExists_AndDtoIsValid()
    {
        // Arrange
        var employee = Employee.Create("Test Employee", "desc");
        // Provide valid future date and valid start/end times
        var date = DateTime.UtcNow.AddDays(1).Date;
        var start = date.AddHours(9);   // 09:00 on that date
        var end = date.AddHours(17);    // 17:00 on that date

        var requestDto = new AddShiftDto() { EmployeeId = employee.Id ,
            Date = date, StartTimeUtc = start, EndTimeUtc = end
            };

        _mockRepo.Setup(repo => repo.FirstOrDefaultByIdAsync(It.Is<Guid?>(g => g == employee.Id)))
                 .ReturnsAsync(employee);

        // Act
        var result = await _handler.Handle(new AddShiftToEmployeeCmd(requestDto), CancellationToken.None);

        // Assert
        result.ShouldBeOfType<GenResult<ShiftDto>>();
        result.Succeeded.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value!.EmployeeId.ShouldBe(employee.Id);
        result.Value.Date.Date.ShouldBe(date.Date);
        result.Value.StartTimeUtc.ShouldBe(start);
        result.Value.EndTimeUtc.ShouldBe(end);
    }

    //--------------------------// 


}//Cls