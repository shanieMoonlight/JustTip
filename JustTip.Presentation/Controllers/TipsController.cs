using JustTip.Application.Features.Tips;
using JustTip.Application.Features.Tips.Cmd.Create;
using JustTip.Application.Features.Tips.Cmd.Delete;
using JustTip.Application.Features.Tips.Cmd.Update;
using JustTip.Application.Features.Tips.Qry.GetAll;
using JustTip.Application.Features.Tips.Qry.GetById;
using JustTip.Application.Features.Tips.Qry.GetCurrentWeekTotalTips;
using JustTip.Application.Features.Tips.Qry.GetTipsTotalByWeek;
using JustTip.Application.Features.Tips.Qry.GetUpcomingWeekTotalTips;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JustTip.Presentation.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TipsController(ISender sender, ILogger<TipsController> logger) : Controller
{

    //--------------------------// 

    [HttpPost]
    public async Task<ActionResult<TipDto>> Add([FromBody] CreateTipDto dto) =>
        this.ProcessResult(await sender.Send(new CreateTipCmd(dto)), logger);

    //--------------------------// 

    [HttpPatch]
    public async Task<ActionResult<TipDto>> Edit([FromBody] UpdateTipDto dto) =>
        this.ProcessResult(await sender.Send(new UpdateTipCmd(dto)), logger);

    //--------------------------// 

    [HttpDelete("{id}")]
    public async Task<ActionResult<TipDto>> Delete(Guid id) =>
        this.ProcessResult(await sender.Send(new DeleteTipCmd(id)), logger);

    //--------------------------// 

    [HttpGet]
    public async Task<ActionResult<TipDto[]>> GetAll() =>
        this.ProcessResult(await sender.Send(new GetAllTipsQry()), logger);

    //--------------------------// 

    /// <summary>
    /// Gets the Tip with Id = <paramref name="id"/> 
    /// </summary>
    /// <returns>The Tip matching the id or NotFound</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<TipDto>> Get(Guid id) =>
        this.ProcessResult(await sender.Send(new GetTipByIdQry(id)), logger);

    //--------------------------// 

    [HttpGet]
    public async Task<ActionResult<double>> GetTotalTipsCurrentWeek() =>
        this.ProcessResult(await sender.Send(new GetCurrentWeekTotalTipsQry()), logger);

    //--------------------------// 

    [HttpGet("{weekNumber}")]
    public async Task<ActionResult<double>> GetTotalTipsByWeek(int? weekNumber) =>
        this.ProcessResult(await sender.Send(new GetTipsTotalByWeekQry(weekNumber)), logger);

    //--------------------------// 

    [HttpGet("{weekNumber}")]
    public async Task<ActionResult<double>> GetAllTipsByWeek(int? weekNumber) =>
        this.ProcessResult(await sender.Send(new GetAllTipsByWeekQry(weekNumber ?? 0)), logger);

    //--------------------------// 


    ///// <summary>
    ///// Gets a paginated list of Tips
    ///// </summary>
    ///// <param name="request">Filtering and Sorting Info</param>
    ///// <returns>Paginated list of Tips</returns>
    //[HttpPost]
    //public async Task<ActionResult<PagedResponse<TipDto>>> Page(PagedRequest? request) =>
    //    this.ProcessResult(await sender.Send(new GetTipsPageQry(request)), logger);


} //Cls