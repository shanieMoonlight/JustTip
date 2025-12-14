using JustTip.Application.Domain.Entities.Shifts;

namespace JustTip.Application.Features.Roster;
public class AddShiftDto
{

    public DateTime StartTimeUtc { get; set; }
    public DateTime EndTimeUtc { get; set; }
    public DateTime Date { get; set; }
    public Guid EmployeeId { get; set; }

    //--------------------------// 

    #region ModelBindingCtor
    public AddShiftDto() { }
    #endregion

    public AddShiftDto(Shift mdl)
    {
        StartTimeUtc = mdl.StartTimeUtc;
        EndTimeUtc = mdl.EndTimeUtc;
        Date = mdl.Date;
        EmployeeId = mdl.EmployeeId;

    }

}//Cls

