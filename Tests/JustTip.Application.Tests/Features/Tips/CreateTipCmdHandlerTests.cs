

using JustTip.Testing.Utils.DataFactories.Dtos;
using Moq;

namespace JustTip.Application.Tests.Features.Tips;

public class CreateTipCmdHandlerTests
{
    private readonly Mock<ITipRepo> _mockRepo;
    private readonly CreateTipCmdHandler _handler;

    //- - - - - - - - - - - - - //

    public CreateTipCmdHandlerTests()
    {
        _mockRepo = new Mock<ITipRepo>();
        _handler = new CreateTipCmdHandler(_mockRepo.Object);
    }

    //--------------------------// 

    [Fact]
    public async Task Handle_ShouldCreateTip_WhenDtoIsValid()
    {
        // Arrange
        var requestDto = CreateTipDtoDataFactory.Create(
            dateTime: DateTime.UtcNow,
            amount:11);
        //var requestDto = TipDtoDataFactory.Create(
        //    null,
        //    "TipName",
        //    "TipDescription");
        var model = requestDto.ToModel();



        _mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Tip>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync((Tip mdl, CancellationToken cancellationToken) => model);

        // Act
        var result = await _handler.Handle(new CreateTipCmd(requestDto), CancellationToken.None);

        // Assert
        result.ShouldBeOfType<GenResult<TipDto>>();
        result.Value!.AmountEuros.ShouldBeEquivalentTo(requestDto.AmountEuros);
    }


}//Cls