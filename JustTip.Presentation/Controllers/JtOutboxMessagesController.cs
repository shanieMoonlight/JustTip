using JustTip.Application.Features.JtOutboxMessages;
using JustTip.Application.Features.JtOutboxMessages.Cmd.Delete;
using JustTip.Application.Features.JtOutboxMessages.Qry.GetAll;
using JustTip.Application.Features.JtOutboxMessages.Qry.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JustTip.Presentation.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class JtOutboxMessagesController(ISender sender) : Controller
{

    [HttpDelete("{id}")]
    public async Task<ActionResult<JtOutboxMessageDto>> Delete(Guid id) =>
        this.ProcessResult(await sender.Send(new DeleteJtOutboxMessageCmd(id)));

    //--------------------------// 

    [HttpGet]
    public async Task<ActionResult<JtOutboxMessageDto[]>> GetAll() =>
        this.ProcessResult(await sender.Send(new GetAllJtOutboxMessagesQry()));

    //--------------------------// 

    /// <summary>
    /// Gets the JtOutboxMessage with Id = <paramref name="id"/> 
    /// </summary>
    /// <returns>The JtOutboxMessage matching the id or NotFound</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<JtOutboxMessageDto>> Get(Guid id) =>
        this.ProcessResult(await sender.Send(new GetJtOutboxMessageByIdQry(id)));

    //--------------------------// 

    ///// <summary>
    ///// Gets a paginated list of JtOutboxMessages
    ///// </summary>
    ///// <param name="request">Filtering and Sorting Info</param>
    ///// <returns>Paginated list of JtOutboxMessages</returns>
    //[HttpPost]
    //[AllowAnonymous]
    //public async Task<ActionResult<PagedResponse<JtOutboxMessageDto>>> Page(PagedRequest? request) =>
    //    this.ProcessResult(await sender.Send(new GetJtOutboxMessagesPageQry(request)));


} //Cls