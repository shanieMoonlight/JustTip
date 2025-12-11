using JustTip.Application.Domain.Entities.Shifts;

namespace JustTip.Application.Features.Employees.Cmd;
public class ShiftDto
{
    public Guid Id { get; set; }

    public DateTime StartTimeUtc { get; set; }
    public DateTime EndTimeUtc { get; set; }
    public DateTime Date { get; set; }
    public Guid EmployeeId { get; set; }

    //--------------------------// 

    #region ModelBindingCtor
    public ShiftDto() { }
    #endregion

    public ShiftDto(Shift mdl)
    {
        StartTimeUtc = mdl.StartTimeUtc;
        EndTimeUtc = mdl.EndTimeUtc;
        Date = mdl.Date;
        EmployeeId = mdl.EmployeeId;
        Id = mdl.Id;

    }

}//Cls

