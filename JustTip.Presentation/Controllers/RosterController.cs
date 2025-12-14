using JustTip.Application.Features.Roster;
using JustTip.Application.Features.Roster.Cmd.UpdateShift;
using JustTip.Application.Features.Roster.Qry.GetCurrentWeekRoster;
using JustTip.Application.Features.Roster.Qry.GetRosterByWeek;
using JustTip.Application.Features.Roster.Qry.GetShift.GetById;
using JustTip.Application.Features.Roster.Qry.GetUpcomingWeekRoster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JustTip.Presentation.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class RosterController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<RosterDto>> GetCurrentWeek() =>
        this.ProcessResult(await sender.Send(new GetCurrentWeekRosterQry()));


    [HttpGet]
    public async Task<ActionResult<RosterDto>> GetUpcomingWeek() =>
        this.ProcessResult(await sender.Send(new GetUpcomingWeekRosterQry()));


    [HttpGet("{weekNumber?}")]
    public async Task<ActionResult<RosterDto>> GetByWeek(int? weekNumber) =>
        this.ProcessResult(await sender.Send(new GetRosterByWeekQry(weekNumber)));


    [HttpGet("{shiftId}")]
    public async Task<ActionResult<RosterDto>> GetShift(Guid shiftId) =>
        this.ProcessResult(await sender.Send(new GetShiftByIdQry(shiftId)));


    [HttpPatch("{shiftId}")]
    public async Task<ActionResult<RosterDto>> EditShift(Guid shiftId, ShiftDto shift) =>
        this.ProcessResult(await sender.Send(new UpdateShiftCmd(shiftId, shift)));
}