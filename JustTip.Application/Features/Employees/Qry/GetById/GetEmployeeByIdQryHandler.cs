using JustTip.Application.Features.Employees.Cmd;
using JustTip.Application.Features.JtOutboxMessages;

namespace JustTip.Application.Features.Employees.Qry.GetById;
internal class GetEmployeeByIdQryHandler(IEmployeeRepo repo) : IJtQueryHandler<GetEmployeeByIdQry, EmployeeDto>
{

    public async Task<GenResult<EmployeeDto>> Handle(GetEmployeeByIdQry request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var mdl = await repo.FirstOrDefaultByIdAsync(id);
        if (mdl is null)
            return GenResult<EmployeeDto>.NotFoundResult(JtMsgs.Error.NotFound<Employee>(id));

        return GenResult<EmployeeDto>.Success(mdl.ToDto());

    }//Handle


}//Cls
