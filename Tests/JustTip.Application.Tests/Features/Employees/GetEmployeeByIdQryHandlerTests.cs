using JustTip.Application.Features.Employees.Qry.GetById;
using JustTip.Testing.Utils.DataFactories;

namespace JustTip.Application.Tests.Features.Employees;

public class GetEmployeeByIdQryHandlerTests
{
    private readonly Mock<IEmployeeRepo> _mockRepo;
    private readonly GetEmployeeByIdQryHandler _handler;

    
    //- - - - - - - - - - - - - //

    public GetEmployeeByIdQryHandlerTests()
    {
        _mockRepo = new Mock<IEmployeeRepo>();
        _handler = new GetEmployeeByIdQryHandler(_mockRepo.Object);
    }

    //--------------------------// 

 
    [Fact]
    public async Task Handle_ShouldReturnEmployeeDto_WhenExists()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        var employeeName = "MyEmployee";
        var expectedEmployee = EmployeeDataFactory.Create(
            employeeId, 
            employeeName);

        //_mockRepo.GetByIdAsync(employeeName).Returns(expectedEmployee);
        _mockRepo.Setup(repo => repo.FirstOrDefaultByIdAsync(employeeId))
                .ReturnsAsync(expectedEmployee);

        // Act
        var result = await _handler.Handle(new GetEmployeeByIdQry(employeeId), CancellationToken.None);

        // Assert
        Assert.IsType<GenResult<EmployeeDto>>(result);
        Assert.NotNull(result.Value);
        Assert.Equal(employeeId, result.Value.Id); // Assuming Id is mapped to Dto
        Assert.Equal(employeeName, result.Value.Name);

        result.ShouldBeOfType<GenResult<EmployeeDto>>();
        result.Value.ShouldNotBeNull();
        result.Value!.Id.ShouldBe(employeeId);
        result.Value!.Name.ShouldBe(employeeName);
    }

    //--------------------------// 

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenEmployeeDoesNotExist()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        //_mockRepo.GetByIdAsync(employeeId).Returns((Employee)null);
        _mockRepo.Setup(repo => repo.FirstOrDefaultByIdAsync(employeeId))
                   .ReturnsAsync((Employee)null!);

    // Act
    var result = await _handler.Handle(new GetEmployeeByIdQry(employeeId), CancellationToken.None);

        // Assert
    
        result.ShouldBeOfType<GenResult<EmployeeDto>>();
        result.Succeeded.ShouldBeFalse();
        result.NotFound.ShouldBeTrue();
        result.Info.ShouldBe(JtMsgs.Error.NotFound<Employee>(employeeId));
    }

    //--------------------------// 

    [Fact]
    public async Task Handle_ShouldReturnBadRequest_WhenNameIsEmpty()
    {
        // Arrange
        var request = new GetEmployeeByIdQry(Guid.Empty);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        //Assert.IsType<GenResult<EmployeeDto>>(result);
        //Assert.False(result.Succeeded);
        //Assert.True(result.NotFound);
        result.ShouldBeOfType<GenResult<EmployeeDto>>();
        result.Succeeded.ShouldBeFalse();
        result.NotFound.ShouldBeTrue();
    }

    //--------------------------// 


}
