using JustTip.Application.Features.Employees.Cmd.Delete;

namespace JustTip.Application.Tests.Features.Employees;

public class DeleteEmployeeCmdHandlerTests
{
    private readonly Mock<IEmployeeRepo> _mockRepo;
    private readonly DeleteEmployeeCmdHandler _handler;

    //- - - - - - - - - - - - - //

    public DeleteEmployeeCmdHandlerTests()
    {
        _mockRepo = new Mock<IEmployeeRepo>();
        _handler = new DeleteEmployeeCmdHandler(_mockRepo.Object);
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
        var result = await _handler.Handle(new DeleteEmployeeCmd(Id), CancellationToken.None);

        // Assert
        result.ShouldBeOfType<BasicResult>();
        result.Succeeded.ShouldBeTrue();
        result.Info.ShouldBe(JtMsgs.Info.Deleted<Employee>(Id));
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
    //    var result = await _handler.Handle(new DeleteEmployeeCmd(Id), CancellationToken.None);

    //    // Assert
    //    result.ShouldBeOfType<BasicResult>();
    //    result.Succeeded.ShouldBeFalse();
    //    result.StatusCode.ShouldBe(StatusCodes.InternalServerError);
    //}

    //--------------------------// 

}//Cls