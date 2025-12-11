using JustTip.Application.Features.Tips.Cmd.Delete;

namespace JustTip.Application.Tests.Features.Tips;

public class DeleteTipCmdHandlerTests
{
    private readonly Mock<ITipRepo> _mockRepo;
    private readonly DeleteTipCmdHandler _handler;

    //- - - - - - - - - - - - - //

    public DeleteTipCmdHandlerTests()
    {
        _mockRepo = new Mock<ITipRepo>();
        _handler = new DeleteTipCmdHandler(_mockRepo.Object);
    }

    //--------------------------// 

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenDeletesSuccessfully()
    {
        // Arrange
        var Id = Guid.NewGuid();
        _mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>()))
                 .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(new DeleteTipCmd(Id), CancellationToken.None);

        // Assert
        result.ShouldBeOfType<BasicResult>();
        result.Succeeded.ShouldBeTrue();
        result.Info.ShouldBe(JustTipMsgs.Info.Deleted<Tip>(Id));
}

    //--------------------------// 

    //[Fact]
    //public async Task Handle_ShouldReturnInternalServerError_WhenExceptionIsThrown()
    //{
    //    // Arrange
    //    var Id = Guid.NewGuid();
    //    _mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>()))
    //             .ThrowsAsync(new Exception("Error deleting feature flag"));

    //    // Act
    //    var result = await _handler.Handle(new DeleteTipCmd(Id), CancellationToken.None);

    //    // Assert
    //    result.ShouldBeOfType<BasicResult>();
    //    result.Succeeded.ShouldBeFalse();
    //    result.StatusCode.ShouldBe(StatusCodes.InternalServerError);
    //}

    //--------------------------// 

}//Cls