using JustTip.Application.Features.Employees.Cmd;
using JustTip.Application.Features.Employees.Cmd.Create;
using JustTip.Application.Features.Employees.Cmd.Delete;
using JustTip.Application.Features.Employees.Cmd.Update;
using JustTip.Application.Features.Employees.Qry.GetAll;
using JustTip.Application.Features.Employees.Qry.GetById;
using JustTip.Application.Features.Employees.Qry.GetFiltered;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JustTip.Presentation.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class EmployeesController(ISender sender) : Controller
{

    //--------------------------// 

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> Add([FromBody] EmployeeDto dto) =>
        this.ProcessResult(await sender.Send(new CreateEmployeeCmd(dto)));

    //--------------------------// 

    [HttpPatch]
    public async Task<ActionResult<EmployeeDto>> Edit([FromBody] EmployeeDto dto) =>
        this.ProcessResult(await sender.Send(new UpdateEmployeeCmd(dto)));

    //--------------------------// 

    [HttpDelete("{id}")]
    public async Task<ActionResult<EmployeeDto>> Delete(Guid id) =>
        this.ProcessResult(await sender.Send(new DeleteEmployeeCmd(id)));

    //--------------------------// 

    [HttpGet]
    public async Task<ActionResult<EmployeeDto[]>> GetAll() =>
        this.ProcessResult(await sender.Send(new GetAllEmployeesQry()));

    //--------------------------// 

    /// <summary>
    /// Gets the Employee with Id = <paramref name="id"/> 
    /// </summary>
    /// <returns>The Employee matching the id or NotFound</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> Get(Guid id) =>
        this.ProcessResult(await sender.Send(new GetEmployeeByIdQry(id)));

    //--------------------------// 

    /// <summary>
    /// Gets the Employee with Name = <paramref name="name"/> 
    /// </summary>
    /// <returns>The Employee matching the id or NotFound</returns>
    [HttpGet("{name}")]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllByName(string? name) =>
        this.ProcessResult(await sender.Send(new GetAllEmployeesByNameQry(name)));

    ////--------------------------// 

    ///// <summary>
    ///// Gets a paginated list of Employees
    ///// </summary>
    ///// <param name="request">Filtering and Sorting Info</param>
    ///// <returns>Paginated list of Employees</returns>
    //[HttpPost]
    //[AllowAnonymous]
    //public async Task<ActionResult<PagedResponse<EmployeeDto>>> Page(PagedRequest? request) =>
    //    this.ProcessResult(await sender.Send(new GetEmployeesPageQry(request)));


} //Cls