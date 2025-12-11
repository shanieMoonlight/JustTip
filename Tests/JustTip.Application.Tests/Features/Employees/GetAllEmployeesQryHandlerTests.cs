using JustTip.Application.Features.Employees.Qry.GetAll;
using JustTip.Application.Features.Employees.Qry.GetById;
using JustTip.Testing.Utils.DataFactories;

namespace JustTip.Application.Tests.Features.Employees;

public class GetAllEmployeesQryHandlerTests
{
    private readonly Mock<IEmployeeRepo > _mockRepo;
    private readonly GetAllEmployeesQryHandler _handler;
    
    //- - - - - - - - - - - - - //

    public GetAllEmployeesQryHandlerTests()
    {
        _mockRepo = new Mock<IEmployeeRepo>();
        _handler = new GetAllEmployeesQryHandler(_mockRepo.Object);
    }

    //--------------------------// 

    [Fact]
    public async Task Handle_ShouldReturnAllEmployees_WhenSuccessful()
    {
        // Arrange
        var mdls = EmployeeDataFactory.CreateMany();
        _mockRepo.Setup(repo => repo.ListAllAsync())
                .ReturnsAsync(mdls);

    var handler = new GetAllEmployeesQryHandler(_mockRepo.Object);
        var request = new GetAllEmployeesQry();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await handler.Handle(request, cancellationToken);

        // Assert
        result.ShouldBeOfType<GenResult<IEnumerable<EmployeeDto>>>();
        result.Value?.Count().ShouldBe(mdls.Count);
    }

    //--------------------------// 

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoEmployeesExist()
    {
        // Arrange
        //_mockRepo.GetAllAsync().Returns([]);
        _mockRepo.Setup(repo => repo.ListAllAsync())
                .ReturnsAsync([]);

    var handler = new GetAllEmployeesQryHandler(_mockRepo.Object);
        var request = new GetAllEmployeesQry();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await handler.Handle(request, cancellationToken);

        // Assert
        result.ShouldBeOfType<GenResult<IEnumerable<EmployeeDto>>>();
        result.Value.ShouldBeEmpty();
    }

    //--------------------------// 

}//Cls