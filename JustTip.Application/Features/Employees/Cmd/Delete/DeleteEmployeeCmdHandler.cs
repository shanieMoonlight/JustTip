namespace JustTip.Application.Features.Employees.Cmd.Delete;
public class DeleteEmployeeCmdHandler(IEmployeeRepo _repo ) : IJtCommandHandler<DeleteEmployeeCmd, DeletedResult>
{

    public async Task<GenResult<DeletedResult>> Handle(DeleteEmployeeCmd request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        await _repo.DeleteAsync(id);
        var result = new DeletedResult(id);
        return GenResult<DeletedResult>.Success((result));

    }

}//Cls
