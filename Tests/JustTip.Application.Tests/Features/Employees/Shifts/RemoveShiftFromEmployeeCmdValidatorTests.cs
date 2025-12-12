using JustTip.Application.Features.Roster.Cmd.RemoveShift;

namespace JustTip.Application.Tests.Features.Employees.Shifts;

public class RemoveShiftFromEmployeeCmdValidatorTests
{

    //--------------------------// 

    [Fact]
    public void Validate_ShouldReturnValidationFailure_WhenDtoIsNull()
    {
        // Arrange
        var validator = new RemoveShiftFromEmployeeCmdValidator();
        var command = new RemoveShiftFromEmployeeCmd(null!);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.First().ErrorMessage.ShouldBe(JustTipMsgs.Error.IsRequired(nameof(RemoveShiftFromEmployeeCmd.Dto)));
    }

    //--------------------------// 

    [Fact]
    public void Validate_ShouldReturnValidationSuccess_WhenDtoIsNotNull()
    {
        // Arrange
        var validator = new RemoveShiftFromEmployeeCmdValidator();
        var command = new RemoveShiftFromEmployeeCmd(new RemoveShiftDto(default, default));

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeTrue();
    }


}//Cls