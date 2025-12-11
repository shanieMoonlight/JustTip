using JustTip.Application.Features.Employees.Cmd.Shifts.AddShift;

namespace JustTip.Application.Tests.Features.Employees.Shifts;

public class AddShiftToEmployeeCmdValidatorTests
{

    //--------------------------// 

    [Fact]
    public void Validate_ShouldReturnValidationFailure_WhenDtoIsNull()
    {
        // Arrange
        var validator = new AddShiftToEmployeeCmdValidator();
        var command = new AddShiftToEmployeeCmd(null!);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.First().ErrorMessage.ShouldBe(JustTipMsgs.Error.IsRequired(nameof(AddShiftToEmployeeCmd.Dto)));
    }

    //--------------------------// 

    [Fact]
    public void Validate_ShouldReturnValidationSuccess_WhenDtoIsNotNull()
    {
        // Arrange
        var validator = new AddShiftToEmployeeCmdValidator();
        var command = new AddShiftToEmployeeCmd(new ShiftDto());

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeTrue();
    }


}//Cls