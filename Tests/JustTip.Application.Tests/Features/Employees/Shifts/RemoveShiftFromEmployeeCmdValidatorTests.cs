using JustTip.Application.Features.Roster.Cmd.RemoveShift;

namespace JustTip.Application.Tests.Features.Employees.Shifts;

public class RemoveShiftFromEmployeeCmdValidatorTests
{

    [Fact]
    public void Validate_ShouldReturnValidationFailure_WhenEmployeeIdNull()
    {
        // Arrange
        var validator = new RemoveShiftFromEmployeeCmdValidator();
        var command = new RemoveShiftFromEmployeeCmd(default, Guid.NewGuid());

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
    }


    //--------------------------// 

    [Fact]
    public void Validate_ShouldReturnValidationFailure_WhenDtoIsNull()
    {
        // Arrange
        var validator = new RemoveShiftFromEmployeeCmdValidator();
        var command = new RemoveShiftFromEmployeeCmd(Guid.NewGuid(), default);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
    }

    //--------------------------// 

    [Fact]
    public void Validate_ShouldReturnValidationSuccess_WhenDtoIsNotNull()
    {
        // Arrange
        var validator = new RemoveShiftFromEmployeeCmdValidator();
        var command = new RemoveShiftFromEmployeeCmd(Guid.NewGuid(), Guid.NewGuid());

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeTrue();
    }


}//Cls