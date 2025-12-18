using JustTip.Testing.Utils.DataFactories.Dtos;

namespace JustTip.Application.Tests.Features.Employees;

public class CreateEmployeeCmdHandlerTests
{
    private readonly Mock<IEmployeeRepo> _mockRepo;
    private readonly CreateEmployeeCmdHandler _handler;

    //- - - - - - - - - - - - - //

    public CreateEmployeeCmdHandlerTests()
    {
        _mockRepo = new Mock<IEmployeeRepo>();
        _handler = new CreateEmployeeCmdHandler(_mockRepo.Object);
    }

    //--------------------------// 

    [Fact]
    public async Task Handle_ShouldCreateEmployee_WhenDtoIsValid()
    {
        // Arrange
        var requestDto = EmployeeDtoDataFactory.Create(
            null,
            "EmployeeName",
            "EmployeeDescription");
        var model = requestDto.ToModel();



        _mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Employee>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync((Employee mdl, CancellationToken cancellationToken) => model);

        // Act
        var result = await _handler.Handle(new CreateEmployeeCmd(requestDto), CancellationToken.None);

        // Assert
        result.ShouldBeOfType<GenResult<EmployeeDto>>();
        result.Value!.Name.ShouldBeEquivalentTo(requestDto.Name);
        result.Value!.Description.ShouldBeEquivalentTo(requestDto.Description);
    }


}//Cls