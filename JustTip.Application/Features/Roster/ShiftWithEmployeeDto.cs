using JustTip.Application.Features.Employees;

namespace JustTip.Application.Features.Roster;
public class ShiftWithEmployeeDto
{
    public Guid Id { get; set; }

    public DateTime StartTimeUtc { get; set; }
    public DateTime EndTimeUtc { get; set; }
    public DateTime Date { get; set; }
    public Guid EmployeeId { get; set; }
    public EmployeeDto Employee { get; set; }

    //--------------------------// 

    #region ModelBindingCtor
    public ShiftWithEmployeeDto() { }
    #endregion

    public ShiftWithEmployeeDto(Shift mdl)
    {
        StartTimeUtc = mdl.StartTimeUtc;
        EndTimeUtc = mdl.EndTimeUtc;    
        Date = mdl.Date;
        EmployeeId = mdl.EmployeeId;
        Employee = new EmployeeDto()
        {
            Id = mdl.Employee!.Id,
            Name = mdl.Employee!.Name,
            Description = mdl.Employee!.Description
        };
        Id = mdl.Id;

    }

}//Cls

