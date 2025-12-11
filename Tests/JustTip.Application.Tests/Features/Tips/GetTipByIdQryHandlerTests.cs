using JustTip.Application.Features.Tips.Qry.GetById;

namespace JustTip.Application.Tests.Features.Tips;

public class GetTipByIdQryHandlerTests
{
    private readonly Mock<ITipRepo> _mockRepo;
    private readonly GetTipByIdQryHandler _handler;

    
    //- - - - - - - - - - - - - //

    public GetTipByIdQryHandlerTests()
    {
        _mockRepo = new Mock<ITipRepo>();
        _handler = new GetTipByIdQryHandler(_mockRepo.Object);
    }

    //--------------------------// 

 
    [Fact]
    public async Task Handle_ShouldReturnTipDto_WhenExists()
    {
        // Arrange
        var tipId = Guid.NewGuid();
        var tipName = "MyTip";
        var expectedTip = TipDataFactory.Create(
            tipId);

        //_mockRepo.GetByIdAsync(tipName).Returns(expectedTip);
        _mockRepo.Setup(repo => repo.FirstOrDefaultByIdAsync(tipId))
                .ReturnsAsync(expectedTip);

        // Act
        var result = await _handler.Handle(new GetTipByIdQry(tipId), CancellationToken.None);

        // Assert
        Assert.IsType<GenResult<TipDto>>(result);
        Assert.NotNull(result.Value);
        Assert.Equal(tipId, result.Value.Id); // Assuming Id is mapped to Dto

        result.ShouldBeOfType<GenResult<TipDto>>();
        result.Value.ShouldNotBeNull();
        result.Value!.Id.ShouldBe(tipId);
    }

    //--------------------------// 

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenTipDoesNotExist()
    {
        // Arrange
        var tipId = Guid.NewGuid();
        //_mockRepo.GetByIdAsync(tipId).Returns((Tip)null);
        _mockRepo.Setup(repo => repo.FirstOrDefaultByIdAsync(tipId))
                   .ReturnsAsync((Tip)null!);

    // Act
    var result = await _handler.Handle(new GetTipByIdQry(tipId), CancellationToken.None);

        // Assert
    
        result.ShouldBeOfType<GenResult<TipDto>>();
        result.Succeeded.ShouldBeFalse();
        result.NotFound.ShouldBeTrue();
        result.Info.ShouldBe(JustTipMsgs.Error.NotFound<Tip>(tipId));
    }

    //--------------------------// 

    [Fact]
    public async Task Handle_ShouldReturnBadRequest_WhenNameIsEmpty()
    {
        // Arrange
        var request = new GetTipByIdQry(Guid.Empty);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        //Assert.IsType<GenResult<TipDto>>(result);
        //Assert.False(result.Succeeded);
        //Assert.True(result.NotFound);
        result.ShouldBeOfType<GenResult<TipDto>>();
        result.Succeeded.ShouldBeFalse();
        result.NotFound.ShouldBeTrue();
    }

    //--------------------------// 


}
