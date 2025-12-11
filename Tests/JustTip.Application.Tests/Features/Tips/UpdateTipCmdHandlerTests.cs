using JustTip.Dtos.Tests.Data.Initializers;

namespace JustTip.Application.Tests.Features.Tips;

public class UpdateTipCmdHandlerTests
{
    private readonly Mock<ITipRepo> _mockRepo;
    private readonly UpdateTipCmdHandler _handler;

    //- - - - - - - - - - - - - //

    public UpdateTipCmdHandlerTests()
    {
        _mockRepo = new Mock<ITipRepo>();
        _handler = new UpdateTipCmdHandler(_mockRepo.Object);
    }

    //--------------------------// 

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenUpdateSucceeds()
    {
        // Arrange

        var Id = Guid.NewGuid();

        var original = TipDataFactory.Create(Id);


        var requestDto = UpdateTipDtoDataFactory.Create(
           id:Id,
           amount:11
       );

        //var model = requestDto.ToModel();
        //var Model = requestDto.ToModel();

        _mockRepo.Setup(repo => repo.FirstOrDefaultByIdAsync(Id))
                 .ReturnsAsync(original);

        _mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Tip>()))
                 .ReturnsAsync((Tip entity) => entity);

        // Act
        var result = await _handler.Handle(new UpdateTipCmd(requestDto), CancellationToken.None);

        // Assert
        result.ShouldBeOfType<GenResult<TipDto>>();
        result.Value!.AmountEuros.ShouldBeEquivalentTo(requestDto.AmountEuros);
    }

    //--------------------------// 

    //[Fact]
    //public async Task Handle_ShouldReturnBadRequest_WhenDtoIsNull()
    //{
    //    // Arrange
    //    var request = new UpdateTipCmd(null);

    //    // Act
    //    var result = await _handler.Handle(request, CancellationToken.None);

    //    // Assert
    //    result.ShouldBeOfType<GenResult<TipDto>>();
    //    result.Succeeded.ShouldBeFalse();
    //    result.Info.ShouldBe(JustTipMsgs.Error.NO_DATA_SUPPLIED); 
    //    result.BadRequest.ShouldBeTrue(); 
    //}

    //--------------------------// 

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenTipNotFound()
    {
        // Arrange
        var Dto = new UpdateTipDto { Id = Guid.NewGuid() };

        _mockRepo.Setup(repo => repo.FirstOrDefaultByIdAsync(Dto.Id))
                 .ReturnsAsync((Tip)null!);

        // Act
        var result = await _handler.Handle(new UpdateTipCmd(Dto), CancellationToken.None);

        // Assert
        result.ShouldBeOfType<GenResult<TipDto>>();
        result.Succeeded.ShouldBeFalse();
        result.Info.ShouldBe(JustTipMsgs.Error.NotFound<Tip>(Dto.Id));
    }

}