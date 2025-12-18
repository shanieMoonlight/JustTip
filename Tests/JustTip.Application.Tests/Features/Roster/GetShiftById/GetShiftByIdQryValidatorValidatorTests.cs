using JustTip.Application.Features.Roster.Qry.GetShiftById;

namespace JustTip.Application.Tests.Features.Roster.GetShiftById;

public class GetShiftByIdQryValidatorValidatorTests
{


    [Fact]
    public void Validate_ShouldReturnValidationFailure_WhenIdIsNull()
    {
        // Arrange
        var validator = new GetShiftByIdQryValidator();
        var command = new GetShiftByIdQry(default);

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
        var validator = new GetShiftByIdQryValidator();
        var command = new GetShiftByIdQry(Guid.NewGuid());

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeTrue();
    }


}//Cls