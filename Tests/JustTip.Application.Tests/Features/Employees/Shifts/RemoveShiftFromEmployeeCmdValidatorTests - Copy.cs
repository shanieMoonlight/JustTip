using JustTip.Application.Features.Employees.Qry.GetUpcomingShifts;
using JustTip.Application.Features.Roster.Cmd.RemoveShift;

namespace JustTip.Application.Tests.Features.Employees.Shifts;

public class GetUpcomingShiftsQryValidatorTests
{

    [Fact]
    public void Validate_ShouldReturnValidationFailure_WhenDtoIsNull()
    {
        // Arrange
        var validator = new GetUpcomingShiftsQryValidator();
        var command = new GetUpcomingShiftsQry(default);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.First().ErrorMessage
            .Replace(" ", "")
            .ShouldBe(JustTipMsgs.Error.IsRequired(nameof(GetUpcomingShiftsQry.EmployeeId)).Replace(" ", ""));
    }

    //--------------------------// 

    [Fact]
    public void Validate_ShouldReturnValidationSuccess_WhenDtoIsNotNull()
    {
        // Arrange
        var validator = new GetUpcomingShiftsQryValidator();
        var command = new GetUpcomingShiftsQry(Guid.NewGuid());
        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeTrue();
    }


}//Cls