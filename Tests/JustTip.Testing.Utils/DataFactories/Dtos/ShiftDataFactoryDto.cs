namespace JustTip.Testing.Utils.DataFactories.Dtos;

public static class ShiftDtoDataFactory
{
    private static int _counter = 0;

    //--------------------------// 

    public static List<ShiftDto> CreateMany(int count = 20) =>
        [.. IdGenerator.GetGuidIdsList(count).Select(id => Create(id))];

    //- - - - - - - - - - - - - //

    public static ShiftDto Create(
        Guid? id = null,
        Guid? employeeId = null
        )
    {

        employeeId ??= Guid.NewGuid();

        var paramaters = new[]
            {
                         new PropertyAssignment(nameof(Shift.EmployeeId),  () => employeeId ),
                  new PropertyAssignment(nameof(Shift.Id),  () => id )
            };

        return ConstructorInvoker.CreateNoParamsInstance<ShiftDto>(paramaters);
    }

    //--------------------------// 

    public static ShiftDto Update(
        ShiftDto shiftDto,
        Guid? id = null,
        Guid? employeeId = null,
        DateTime? date = null,
        DateTime? startTimeUtc = null,
        DateTime? endTimeUtc = null)
    {

        List<PropertyAssignment> propertAssignments = [];



        if (startTimeUtc is not null)
            propertAssignments.Add(new PropertyAssignment(nameof(Shift.StartTimeUtc), () => startTimeUtc));

        if (endTimeUtc is not null)
            propertAssignments.Add(new PropertyAssignment(nameof(Shift.EndTimeUtc), () => endTimeUtc));

        if (date is not null)
            propertAssignments.Add(new PropertyAssignment(nameof(Shift.Date), () => date));

        if (employeeId is not null)
            propertAssignments.Add(new PropertyAssignment(nameof(Shift.EmployeeId), () => employeeId));

        if (id is not null)
            propertAssignments.Add(new PropertyAssignment(nameof(Shift.Id), () => id));


        return PrivatePropertyUpdater.UpdateProperties(shiftDto, [.. propertAssignments]);
    }



}//Cls

