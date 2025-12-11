using JustTip.Application.Domain.Entities.Common;
using JustTip.Application.Domain.Entities.Employees.Events;
using JustTip.Application.Domain.Entities.Shifts;
using MassTransit;

namespace JustTip.Application.Domain.Entities.Employees;
public class Employee : JtBaseDomainEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }


    //Navigation properties
    /// <summary>
    /// Shifts that belong to this Grid
    /// </summary>
    public List<Shift> Shifts { get; private set; } = [];

    //--------------------------// 


    #region Ef Core Ctor
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    protected Employee() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    #endregion


    public Employee(string name, string description)
    {
        Name = name;
        Description = description;
        RaiseDomainEvent(new EmployeeCreatedDomainEvent(Id));
    }


    //--------------------------// 


    public static Employee Create(string name, string description) =>
        new(name, description);


    //- - - - - - - - - - - - - // 

    public Employee Update(string name, string description)
    {
        Name = name;
        Description = description;

        RaiseDomainEvent(new EmployeeUpdatedDomainEvent(Id));
        return this;
    }


    //- - - - - - - - - - - - - // 

    public Shift AddShift(
        DateTime date,
        DateTime startTimeUtc,
        DateTime endTimeUtc)
    {
        var shift = Shift.Create(this, date, startTimeUtc, endTimeUtc);
        Shifts.Add(shift);
        RaiseDomainEvent(new ShiftAddedDomainEvent(Id, shift.Id));
        return shift;
    }

    //- - - - - - - - - - - - - // 


    public Employee RemoveShift(Shift shift) => RemoveShift(shift.Id);

    //- - - - - - - - - - - - - // 


    public Employee RemoveShift(Guid shiftId)
    {
        var existing = Shifts.FirstOrDefault(n => n.Id == shiftId);
        //Already deleted or not found
        if (existing is null)
            return this;

        Shifts.Remove(existing);
        // OPTIONAL: if you want to detach the relationship for EF state tracking:
        // existing.Grid = null;
        // existing.GridId = Guid.Empty;    

        RaiseDomainEvent(new ShiftRemovedDomainEvent(Id, existing.Id));
        return this;
    }




}//Cls
