using Jt.Application.Utility.Results;
using JustTip.Application.Domain.Entities.Employees;
using JustTip.Application.Mediatr.CQRS;


namespace JustTip.Application.Features.Employees.Cmd.Create;
public class CreateEmployeeCmdHandler(IEmployeeRepo _repo ) : IJtCommandHandler<CreateEmployeeCmd, EmployeeDto>
{
    public async Task<GenResult<EmployeeDto>> Handle(CreateEmployeeCmd request, CancellationToken cancellationToken)
    {
     
            var mdl = request.Dto.ToModel();

            var dbMdl = await _repo.AddAsync(mdl, cancellationToken);

            return GenResult<EmployeeDto>.Success(dbMdl.ToDto());
            
        //await uow.SaveChangesAsync(cancellationToken);       
    }


}//Cls
