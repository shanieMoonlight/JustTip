using JustTip.Application.Features.Employees.Cmd;
using JustTip.Application.Features.JtOutboxMessages;

namespace JustTip.Application.Features.Employees.Qry.GetAll;
internal class GetAllEmployeesQryHandler(IEmployeeRepo repo) : IJtQueryHandler<GetAllEmployeesQry, IEnumerable<EmployeeDto>>
{

    public async Task<GenResult<IEnumerable<EmployeeDto>>> Handle(GetAllEmployeesQry request, CancellationToken cancellationToken)
    {
        var mdls = await repo.ListAllAsync();
        var dtos = mdls.Select(mdl => mdl.ToDto());
        return GenResult<IEnumerable<EmployeeDto>>.Success(dtos);

    }

}//Cls
