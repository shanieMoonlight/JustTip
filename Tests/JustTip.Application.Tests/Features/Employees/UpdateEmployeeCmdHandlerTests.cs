using JustTip.Application.Features.Employees.Cmd.Update;
using JustTip.Testing.Utils.DataFactories;
using JustTip.Testing.Utils.DataFactories.Dtos;

namespace JustTip.Application.Tests.Features.Employees;

public class UpdateEmployeeCmdHandlerTests
{
    private readonly Mock<IEmployeeRepo> _mockRepo;
    private readonly UpdateEmployeeCmdHandler _handler;

    //- - - - - - - - - - - - - //

    public UpdateEmployeeCmdHandlerTests()
    {
        _mockRepo = new Mock<IEmployeeRepo>();
        _handler = new UpdateEmployeeCmdHandler(_mockRepo.Object);
    }

    //--------------------------// 

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenUpdateSucceeds()
    {
        // Arrange

        var Id = Guid.NewGuid();

        var original = EmployeeDataFactory.Create(
           Id,
           "EmployeeName_ORIGINAL",
           "EmployeeDescription_ORIGINAL");


        var requestDto = EmployeeDtoDataFactory.Create(
           Id,
           "EmployeeName_NEWNEWNEW",
           "EmployeeDescription_NEWNEWNEW");

        var model = requestDto.ToModel();
        //var Model = requestDto.ToModel();

        _mockRepo.Setup(repo => repo.FirstOrDefaultByIdAsync(Id))
                 .ReturnsAsync(original);

        _mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Employee>()))
                 .ReturnsAsync((Employee entity) => entity);

        // Act
        var result = await _handler.Handle(new UpdateEmployeeCmd(requestDto), CancellationToken.None);

        // Assert
        result.ShouldBeOfType<GenResult<EmployeeDto>>();
        result.Value!.Name.ShouldBeEquivalentTo(requestDto.Name);
        result.Value!.Description.ShouldBeEquivalentTo(requestDto.Description);
    }

    //--------------------------// 

    //[Fact]
    //public async Task Handle_ShouldReturnBadRequest_WhenDtoIsNull()
    //{
    //    // Arrange
    //    var request = new UpdateEmployeeCmd(null);

    //    // Act
    //    var result = await _handler.Handle(request, CancellationToken.None);

    //    // Assert
    //    result.ShouldBeOfType<GenResult<EmployeeDto>>();
    //    result.Succeeded.ShouldBeFalse();
    //    result.Info.ShouldBe(JtMsgs.Error.NO_DATA_SUPPLIED); 
    //    result.BadRequest.ShouldBeTrue(); 
    //}

    //--------------------------// 

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenEmployeeNotFound()
    {
        // Arrange
        var Dto = new EmployeeDto { Id = Guid.NewGuid() };

        _mockRepo.Setup(repo => repo.FirstOrDefaultByIdAsync(Dto.Id))
                 .ReturnsAsync((Employee)null!);

        // Act
        var result = await _handler.Handle(new UpdateEmployeeCmd(Dto), CancellationToken.None);

        // Assert
        result.ShouldBeOfType<GenResult<EmployeeDto>>();
        result.Succeeded.ShouldBeFalse();
        result.Info.ShouldBe(JustTipMsgs.Error.NotFound<Employee>(Dto.Id));
    }

}