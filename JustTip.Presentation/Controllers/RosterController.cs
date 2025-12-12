using JustTip.Application.Features.Roster;
using JustTip.Application.Features.Roster.Qry.GetCurrentWeekRoster;
using JustTip.Application.Features.Roster.Qry.GetUpcomingWeekRoster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JustTip.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RosterController(ISender sender) : ControllerBase
{
    [HttpGet("current-week")]
    public async Task<ActionResult<RosterDto>> GetCurrentWeek() =>
        this.ProcessResult(await sender.Send(new GetCurrentWeekRosterQry()));


    [HttpGet("upcoming-week")]
    public async Task<ActionResult<RosterDto>> GetUpcomingWeek() =>
        this.ProcessResult(await sender.Send(new GetUpcomingWeekRosterQry()));
}