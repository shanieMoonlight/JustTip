namespace JustTip.Application.Features.Employees.Cmd.Delete;
public class DeleteEmployeeCmdHandler(IEmployeeRepo _repo ) : IJtCommandHandler<DeleteEmployeeCmd>
{

    public async Task<BasicResult> Handle(DeleteEmployeeCmd request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        await _repo.DeleteAsync(id);

        return BasicResult.Success(JustTipMsgs.Info.Deleted<Employee>(id));

    }

}//Cls
