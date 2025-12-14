namespace JustTip.Testing.Utils.DataFactories;

public static class ShiftDataFactory
{

    public static List<Shift> CreateMany(int count = 20) =>
        [.. IdGenerator.GetGuidIdsList(count).Select(id => Create(id))];

    //- - - - - - - - - - - - - //

    public static Shift Create(
          Guid? id = null,
        DateTime? startTimeUtc = null,
        DateTime? endTimeUtc = null,
        DateTime? date = null,
        Guid? employeeId = null,
        Employee? employee = null,
        DateTime? createdDate = null,
        DateTime? lastModifiedDate = null
        )
    {

        employeeId ??= employee?.Id ?? Guid.NewGuid();
        id ??= Guid.NewGuid();

        var paramaters = new[]
            {
                         new PropertyAssignment(nameof(Shift.StartTimeUtc),  () => startTimeUtc ),
                  new PropertyAssignment(nameof(Shift.EndTimeUtc),  () => endTimeUtc ),
                  new PropertyAssignment(nameof(Shift.Date),  () => date ),
                  new PropertyAssignment(nameof(Shift.EmployeeId),  () => employeeId ),
                  new PropertyAssignment(nameof(Shift.Employee),  () => employee ),
                  new PropertyAssignment(nameof(Shift.Id),  () => id ),
                  new PropertyAssignment(nameof(Shift.CreatedDate),  () => createdDate ),
                  new PropertyAssignment(nameof(Shift.LastModifiedDate),  () => lastModifiedDate )
            };

        return ConstructorInvoker.CreateNoParamsInstance<Shift>(paramaters);
    }

    //--------------------------// 

    public static Shift Update(
           Shift shift,
           Guid? id = null,
         DateTime? startTimeUtc = null,
         DateTime? endTimeUtc = null,
         DateTime? date = null,
         Guid? employeeId = null,
         Employee? employee = null,
         DateTime? createdDate = null,
         DateTime? lastModifiedDate = null
         )
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

        if (employee is not null)
            propertAssignments.Add(new PropertyAssignment(nameof(Shift.Employee), () => employee));

        if (id is not null)
            propertAssignments.Add(new PropertyAssignment(nameof(Shift.Id), () => id));

        if (createdDate is not null)
            propertAssignments.Add(new PropertyAssignment(nameof(Shift.CreatedDate), () => createdDate));

        if (lastModifiedDate is not null)
            propertAssignments.Add(new PropertyAssignment(nameof(Shift.LastModifiedDate), () => lastModifiedDate));


        return PrivatePropertyUpdater.UpdateProperties(shift, [.. propertAssignments]);
    }



}//Cls

