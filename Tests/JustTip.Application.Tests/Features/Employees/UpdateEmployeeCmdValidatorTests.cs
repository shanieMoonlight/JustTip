using JustTip.Application.Features.Employees.Cmd.Update;

namespace JustTip.Application.Tests.Features.Employees;

public class UpdateEmployeeCmdValidatorTests
{


    [Fact]
    public void Validate_ShouldReturnValidationFailure_WhenDtoIsNull()
    {
        // Arrange
        var validator = new UpdateEmployeeCmdValidator();
        var command = new UpdateEmployeeCmd(Guid.NewGuid(), null);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.First().ErrorMessage.ShouldBe(JustTipMsgs.Error.IsRequired(nameof(UpdateEmployeeCmd.Dto)));
    }

    //--------------------------// 

    [Fact]
    public void Validate_ShouldReturnValidationFailure_WhenIdIsNull()
    {
        // Arrange
        var validator = new UpdateEmployeeCmdValidator();
        var command = new UpdateEmployeeCmd(default(Guid), new EmployeeDto());

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.First().ErrorMessage.ShouldContain(nameof(UpdateEmployeeCmd.Id));
    }

    //--------------------------// 

    [Fact]
    public void Validate_ShouldReturnValidationSuccess_WhenDtoIsNotNull()
    {
        // Arrange
        var validator = new UpdateEmployeeCmdValidator();
        var command = new UpdateEmployeeCmd(Guid.NewGuid(), new EmployeeDto());

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeTrue();
    }


}//Cls