using JustTip.Application.Features.Employees.Cmd;

namespace JustTip.Application.Features.Employees.Qry.GetFiltered;


internal class GetAllEmployeesByNameQryHandler(IEmployeeRepo repo) : IJtQueryHandler<GetAllEmployeesByNameQry, IEnumerable<EmployeeDto>>
{

    public async Task<GenResult<IEnumerable<EmployeeDto>>> Handle(GetAllEmployeesByNameQry request, CancellationToken cancellationToken)
    {
        var mdls = await repo.GetAllByNameAsync(request.Filter);
        var dtos = mdls.Select(mdl => mdl.ToDto());
        return GenResult<IEnumerable<EmployeeDto>>.Success(dtos);

    }

}//Cls
