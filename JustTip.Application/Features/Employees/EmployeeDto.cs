namespace JustTip.Application.Features.Employees;
public class EmployeeDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }

    public IEnumerable<ShiftDto> Shifts { get; set; } = [];


    //--------------------------// 

    #region ModelBindingCtor
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public EmployeeDto() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    #endregion

    public EmployeeDto(Employee mdl)
    {
        Id = mdl.Id;
        Name = mdl.Name;
        Description = mdl.Description;
        Shifts = mdl.Shifts.Select(s => new ShiftDto(s));
    }


}//Cls

