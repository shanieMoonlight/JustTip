using JustTip.Application.Features.Employees.Cmd.Delete;

namespace JustTip.Application.Tests.Features.Employees;

public class DeleteEmployeeCmdValidatorTests
{

    //--------------------------// 

    [Fact]
    public void Validate_ShouldReturnValidationFailure_WhenDtoIsNull()
    {
        // Arrange
        var validator = new DeleteEmployeeCmdValidator();
        var command = new DeleteEmployeeCmd(default);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.First().ErrorMessage.ShouldBe(JustTipMsgs.Error.IsRequired(nameof(Employee.Id)));
    }

    //--------------------------// 

    [Fact]
    public void Validate_ShouldReturnValidationSuccess_WhenDtoIsNotNull()
    {
        // Arrange
        var validator = new DeleteEmployeeCmdValidator();
        var command = new DeleteEmployeeCmd(Guid.NewGuid());

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeTrue();
    }


}//Cls