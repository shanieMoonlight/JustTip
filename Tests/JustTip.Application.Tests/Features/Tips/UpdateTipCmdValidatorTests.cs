using JustTip.Application.Features.Tips;
using JustTip.Application.Features.Tips.Cmd.Update;

namespace JustTip.Application.Tests.Features.Tips;

public class UpdateTipCmdValidatorTests
{


    [Fact]
    public void Validate_ShouldReturnValidationFailure_WhenDtoIsNull()
    {
        // Arrange
        var validator = new UpdateTipCmdValidator();
        var command = new UpdateTipCmd(null);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.First().ErrorMessage.ShouldBe(JustTipMsgs.Error.IsRequired(nameof(UpdateTipCmd.Dto)));
    }

    //--------------------------// 

    [Fact]
    public void Validate_ShouldReturnValidationSuccess_WhenDtoIsNotNull()
    {
        // Arrange
        var validator = new UpdateTipCmdValidator();
        var command = new UpdateTipCmd(new UpdateTipDto());

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    //--------------------------// 

}//Cls