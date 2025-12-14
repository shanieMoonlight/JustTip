using JustTip.Application.Features.Employees;
using JustTip.Application.Features.Employees.Cmd.Create;
using JustTip.Application.Features.Employees.Cmd.Delete;
using JustTip.Application.Features.Employees.Cmd.Update;
using JustTip.Application.Features.Employees.Qry.GetAll;
using JustTip.Application.Features.Employees.Qry.GetById;
using JustTip.Application.Features.Employees.Qry.GetEmployeeWeeklySummary;
using JustTip.Application.Features.Employees.Qry.GetEmployeeWeeklySummaryByWeek;
using JustTip.Application.Features.Employees.Qry.GetFiltered;
using JustTip.Application.Features.Employees.Qry.GetUpcomingShifts;
using JustTip.Application.Features.Roster;
using JustTip.Application.Features.Roster.Cmd.AddShift;
using JustTip.Application.Features.Roster.Cmd.RemoveShift;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JustTip.Presentation.Controllers;

/// <summary>
/// API controller that exposes endpoints to manage employees and their shifts.
/// Uses MediatR <see cref="ISender"/> to dispatch commands and queries.
/// </summary>
/// <param name="sender">MediatR sender used to dispatch commands and queries.</param>
[ApiController]
[Route("api/[controller]/[action]")]
public class EmployeesController(ISender sender) : Controller
{


    /// <summary>
    /// Creates a new employee.
    /// </summary>
    /// <param name="dto">The employee data transfer object containing details for creation.</param>
    /// <returns>
    /// An <see cref="ActionResult{EmployeeDto}"/> containing the created <see cref="EmployeeDto"/>
    /// on success or an appropriate error result.
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> Add([FromBody] EmployeeDto dto) =>
        this.ProcessResult(await sender.Send(new CreateEmployeeCmd(dto)));

    //--------------------------// 

    /// <summary>
    /// Updates an existing employee.
    /// </summary>
    /// <param name="dto">The employee data transfer object containing updated details.</param>
    /// <returns>
    /// An <see cref="ActionResult{EmployeeDto}"/> containing the updated <see cref="EmployeeDto"/>
    /// or an appropriate error result (for example NotFound).
    /// </returns>
    [HttpPatch("{id}")]
    public async Task<ActionResult<EmployeeDto>> Edit([FromRoute] Guid id, [FromBody] EmployeeDto dto) =>
        this.ProcessResult(await sender.Send(new UpdateEmployeeCmd(id, dto)));

    //--------------------------// 

    /// <summary>
    /// Deletes the employee with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the employee to delete.</param>
    /// <returns>
    /// An <see cref="ActionResult{EmployeeDto}"/> containing the deleted <see cref="EmployeeDto"/>
    /// or an appropriate error result (for example NotFound).
    /// </returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<EmployeeDto>> Delete(Guid id) =>
        this.ProcessResult(await sender.Send(new DeleteEmployeeCmd(id)));

    //--------------------------// 

    /// <summary>
    /// Retrieves all employees.
    /// </summary>
    /// <returns>
    /// An <see cref="ActionResult{EmployeeDto[]}"/> containing an array of employees.
    /// </returns>
    [HttpGet]
    public async Task<ActionResult<EmployeeDto[]>> GetAll() =>
        this.ProcessResult(await sender.Send(new GetAllEmployeesQry()));

    //--------------------------// 

    /// <summary>
    /// Gets the employee with Id = <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The unique identifier of the employee to retrieve.</param>
    /// <returns>
    /// An <see cref="ActionResult{EmployeeDto}"/> containing the matching <see cref="EmployeeDto"/>
    /// or <c>NotFound</c> if no employee matches the id.
    /// </returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> Get(Guid id) =>
        this.ProcessResult(await sender.Send(new GetEmployeeByIdQry(id)));

    //--------------------------// 

    /// <summary>
    /// Gets employees that match the provided name.
    /// </summary>
    /// <param name="name">The name to filter employees by. If <c>null</c>, behavior depends on query implementation.</param>
    /// <returns>
    /// An <see cref="ActionResult{IEnumerable{EmployeeDto}}"/> containing matching employees
    /// or an empty collection when no matches are found.
    /// </returns>
    [HttpGet("{name}")]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllByName(string? name) =>
        this.ProcessResult(await sender.Send(new GetAllEmployeesByNameQry(name)));

    //--------------------------// 


    /// <summary>
    /// Adds a shift to an employee.
    /// </summary>
    /// <param name="shift">The shift DTO describing the shift to add (including employee id and shift details).</param>
    /// <returns>
    /// An <see cref="ActionResult{EmployeeDto}"/> containing the updated <see cref="EmployeeDto"/>
    /// after the shift has been added, or an appropriate error result.
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> AddShift([FromBody] AddShiftDto shift) =>
        this.ProcessResult(await sender.Send(new AddShiftToEmployeeCmd(shift)));

    //--------------------------// 

    /// <summary>
    /// Removes a shift from an employee.
    /// </summary>
    /// <param name="shift">The DTO specifying which shift to remove (including employee id and shift id).</param>
    /// <returns>
    /// An <see cref="ActionResult{EmployeeDto}"/> containing the updated <see cref="EmployeeDto"/>
    /// after the shift removal, or an appropriate error result.
    /// </returns>
    [HttpDelete("{employeeId}/{shiftId}")]
    public async Task<ActionResult<EmployeeDto>> RemoveShift([FromRoute] Guid employeeId, [FromRoute] Guid shiftId) =>
        this.ProcessResult(await sender.Send(new RemoveShiftFromEmployeeCmd(employeeId, shiftId)));


    //--------------------------// 

    [HttpGet("{employeeId}")]
    public async Task<ActionResult<EmployeeDto>> GetUpcomingShifts([FromRoute] Guid employeeId) =>
        this.ProcessResult(await sender.Send(new GetUpcomingShiftsQry(employeeId)));

    //--------------------------// 

    /// <summary>
    /// Gets the weekly roster summary for the specified employee for the current week.
    /// </summary>
    /// <param name="id">The unique identifier of the employee whose weekly summary to retrieve.</param>
    /// <returns>
    /// An <see cref="ActionResult{RosterDto}"/> containing the employee's weekly roster summary,
    /// or an appropriate error result (for example <c>NotFound</c> if the employee does not exist).
    /// </returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeWeeklySummaryDto>> GetEmployeeWeeklySummary(Guid id) =>
        this.ProcessResult(await sender.Send(new GetEmployeeWeeklySummaryQry(id)));

    //--------------------------// 
    
    /// <summary>
    /// Gets the weekly roster summary for the specified employee for a specific week.
    /// </summary>
    /// <param name="id">The unique identifier of the employee whose weekly summary to retrieve.</param>
    /// <param name="weekNumber">
    /// Optional week number to retrieve. If <c>null</c>, the current week will be used.
    /// The interpretation of week numbers follows the application's configured week indexing (typically ISO week).
    /// </param>
    /// <returns>
    /// An <see cref="ActionResult{RosterDto}"/> containing the employee's roster summary for the requested week,
    /// or an appropriate error result (for example <c>NotFound</c>).
    /// </returns>
    [HttpGet("{id}/{weekNumber?}")]
    public async Task<ActionResult<EmployeeWeeklySummaryDto>> GetEmployeeWeeklySummaryByWeek(Guid id, int? weekNumber) =>
        this.ProcessResult(await sender.Send(new GetEmployeeWeeklySummaryByWeekQry(id, weekNumber)));

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