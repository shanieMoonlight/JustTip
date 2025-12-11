using JustTip.Application.Features.Employees.Cmd;
using JustTip.Application.Features.Maintenance.Initialize;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JustTip.Presentation;

[ApiController]
[Route("api/[controller]/[action]")]
public class MaintenanceController(ISender sender) : Controller
{

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> InitializeDb() =>
        this.ProcessResult(await sender.Send(new InitializeDbCmd()));


       


} //Cls