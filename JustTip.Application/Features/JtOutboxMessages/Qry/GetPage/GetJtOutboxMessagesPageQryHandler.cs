//using JustTip.Application.Features.JtOutboxMessages.Dtos;

//namespace JustTip.Application.Features.JtOutboxMessages.Qry.GetPage;
//internal class GetJtOutboxMessagesPageQryHandler(IJtOutboxMessageRepo repo) : IJtQueryHandler<GetJtOutboxMessagesPageQry, PagedResponse<JtOutboxMessageDto>>
//{

//    public async Task<GenResult<PagedResponse<JtOutboxMessageDto>>> Handle(GetJtOutboxMessagesPageQry request, CancellationToken cancellationToken)
//    {
//        var pgRequest = request.PagedRequest ?? PagedRequest.Empty();

//        var page = (await repo.PageAsync(pgRequest.PageNumber, pgRequest.PageSize, pgRequest.SortList, pgRequest.FilterList))
//                   .Transform((d) => d.ToDto());

//        var response = new PagedResponse<JtOutboxMessageDto>(page, pgRequest);

//        return GenResult<PagedResponse<JtOutboxMessageDto>>.Success(response);

//    }//Handle


//}//Cls
