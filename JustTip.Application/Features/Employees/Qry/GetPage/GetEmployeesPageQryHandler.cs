//using Helpers;
//using MyResults;
//using PaginationHelpers;
//using JustTip.Application.Mediatr.CQRS.Qry;
//using JustTip.Domain.Entities.Employees;

//namespace JustTip.Application.Features.Employees.Qry.GetPage;
//internal class GetEmployeesPageQryHandler(IEmployeeRepo repo) : IJtQueryHandler<GetEmployeesPageQry, PagedResponse<EmployeeDto>>
//{

//    public async Task<GenResult<PagedResponse<EmployeeDto>>> Handle(GetEmployeesPageQry request, CancellationToken cancellationToken)
//    {
//        var pgRequest = request.PagedRequest ?? PagedRequest.Empty();

//        var page = (await repo.PageAsync(pgRequest.PageNumber, pgRequest.PageSize, pgRequest.SortList, pgRequest.FilterList))
//                   .Transform((d) => d.ToDto());

//        var response = new PagedResponse<EmployeeDto>(page, pgRequest);

//        return GenResult<PagedResponse<EmployeeDto>>.Success(response);

//    }//Handle


//}//Cls
