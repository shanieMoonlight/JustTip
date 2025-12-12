using JustTip.Application.Features.Tips.Qry.GetAll;
using JustTip.Application.Features.Tips.Qry.GetById;

namespace JustTip.Application.Tests.Features.Tips;

public class GetAllTipsQryHandlerTests
{
    private readonly Mock<ITipRepo > _mockRepo;
    private readonly GetAllTipsQryHandler _handler;
    
    //- - - - - - - - - - - - - //

    public GetAllTipsQryHandlerTests()
    {
        _mockRepo = new Mock<ITipRepo>();
        _handler = new GetAllTipsQryHandler(_mockRepo.Object);
    }

    //--------------------------// 

    [Fact]
    public async Task Handle_ShouldReturnAllTips_WhenSuccessful()
    {
        // Arrange
        var mdls = TipDataFactory.CreateMany();
        _mockRepo.Setup(repo => repo.ListAllAsync())
                .ReturnsAsync(mdls);

    var handler = new GetAllTipsQryHandler(_mockRepo.Object);
        var request = new GetAllTipsQry();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await handler.Handle(request, cancellationToken);

        // Assert
        result.ShouldBeOfType<GenResult<IEnumerable<TipDto>>>();
        result.Value?.Count().ShouldBe(mdls.Count);
    }

    //--------------------------// 

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoTipsExist()
    {
        // Arrange
        //_mockRepo.GetAllAsync().Returns([]);
        _mockRepo.Setup(repo => repo.ListAllAsync())
                .ReturnsAsync([]);

    var handler = new GetAllTipsQryHandler(_mockRepo.Object);
        var request = new GetAllTipsQry();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await handler.Handle(request, cancellationToken);

        // Assert
        result.ShouldBeOfType<GenResult<IEnumerable<TipDto>>>();
        result.Value.ShouldBeEmpty();
    }


}//Cls