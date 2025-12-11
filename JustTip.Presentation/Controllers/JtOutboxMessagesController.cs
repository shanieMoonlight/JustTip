using JustTip.Application.Features.JtOutboxMessages;
using JustTip.Application.Features.JtOutboxMessages.Cmd.Delete;
using JustTip.Application.Features.JtOutboxMessages.Qry.GetAll;
using JustTip.Application.Features.JtOutboxMessages.Qry.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JustTip.Presentation.Controllers;

/// <summary>
/// API controller that exposes endpoints to manage <see cref="JtOutboxMessageDto"/> resources.
/// </summary>
/// <param name="sender">The <see cref="ISender"/> used to dispatch commands and queries via MediatR.</param>
[ApiController]
[Route("api/[controller]/[action]")]
public class JtOutboxMessagesController(ISender sender) : Controller
{

    /// <summary>
    /// Deletes the <see cref="JtOutboxMessageDto"/> with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the outbox message to delete.</param>
    /// <returns>
    /// An <see cref="ActionResult{T}"/> containing the deleted <see cref="JtOutboxMessageDto"/> on success,
    /// or an appropriate error result (for example, <see cref="Microsoft.AspNetCore.Mvc.NotFoundResult"/>).
    /// </returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<JtOutboxMessageDto>> Delete(Guid id) =>
        this.ProcessResult(await sender.Send(new DeleteJtOutboxMessageCmd(id)));

    //--------------------------// 

    /// <summary>
    /// Retrieves all <see cref="JtOutboxMessageDto"/> items.
    /// </summary>
    /// <returns>An array of <see cref="JtOutboxMessageDto"/> representing all outbox messages.</returns>
    [HttpGet]
    public async Task<ActionResult<JtOutboxMessageDto[]>> GetAll() =>
        this.ProcessResult(await sender.Send(new GetAllJtOutboxMessagesQry()));

    //--------------------------// 

    /// <summary>
    /// Gets the <see cref="JtOutboxMessageDto"/> with Id = <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The unique identifier of the outbox message to retrieve.</param>
    /// <returns>
    /// The <see cref="JtOutboxMessageDto"/> matching the specified id, or a <see cref="Microsoft.AspNetCore.Mvc.NotFoundResult"/> if not found.
    /// </returns>
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