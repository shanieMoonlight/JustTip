using JustTip.Application.Domain.Entities.Common;
using JustTip.Application.Domain.Entities.Shifts.Events;
using JustTip.Application.Domain.Utils;
using JustTip.Application.Utility.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace JustTip.Application.Domain.Entities.Shifts;
public class Shift : JtBaseDomainEntity
{
    public DateTime StartTimeUtc { get; private set; }

    public DateTime EndTimeUtc { get; private set; }

    public DateTime Date { get; private set; }

    public Guid EmployeeId { get; private set; }
    public Employee? Employee { get; private set; }


    [NotMapped]
    public TimeSpan Duration => EndTimeUtc - StartTimeUtc;

    //--------------------------// 


    #region Ef Core Ctor
    protected Shift() { }
    #endregion


    private Shift(
        Employee employee,
        DateTime date,
        DateTime startTimeUtc,
        DateTime endTimeUtc)
    {
        EmployeeId = employee.Id;
        Employee = employee;
        Date = date.Date;

        StartTimeUtc = (date.Date + startTimeUtc.TimeOfDay).RoundToNearestMinute(5);
        EndTimeUtc = (date.Date + endTimeUtc.TimeOfDay).RoundToNearestMinute(5);
        RaiseDomainEvent(new ShiftCreatedDomainEvent(Id));
    }


    //--------------------------// 


    internal static Shift Create(
        Employee employee,
        DateTime date,
        DateTime startTimeUtc,
        DateTime endTimeUtc)
    {
        ValidateShiftTimes(startTimeUtc, endTimeUtc);

        //Skipping because it causes errors when initializing the DB with past dates
        //this is not a real app, just a demo/test app
        //ValidateDate(date);

        return new(
            employee,
            date,
            startTimeUtc,
            endTimeUtc
        );
    }


    //- - - - - - - - - - - - - // 

    public Shift Update(
        DateTime date,
        DateTime startTimeUtc,
        DateTime endTimeUtc)
    {
        ValidateShiftTimes(startTimeUtc, endTimeUtc);

        //Skipping because it causes errors when initializing the DB with past dates
        //this is not a real app, just a demo/test app
        //ValidateDate(date);

        Date = date;
        StartTimeUtc = startTimeUtc;
        EndTimeUtc = endTimeUtc;
        RaiseDomainEvent(new ShiftUpdatedDomainEvent(Id));
        return this;
    }

    //- - - - - - - - - - - - - // 

    private static void ValidateShiftTimes(DateTime startTimeUtc, DateTime endTimeUtc)
    {
        if (startTimeUtc >= endTimeUtc)
            throw new InvalidDomainDataException(nameof(Shift), "Start time must be earlier than end time.");
    }


    private static void ValidateDate(DateTime date)
    {
        if (date < DateTime.Now)
            throw new InvalidDomainDataException(nameof(Shift), "Date must be in the future.");
    }



}//Cls
