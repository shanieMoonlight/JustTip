using JustTip.Application.Features.Employees;
using JustTip.Application.Features.Roster;

namespace JustTip.Testing.Utils.DataFactories.Dtos;

public static class EmployeeDtoDataFactory
{
    public static List<EmployeeDto> CreateMany(int count = 20) =>
        [.. IdGenerator.GetGuidIdsList(count).Select(id => Create(id))];

    //- - - - - - - - - - - - - //

    public static EmployeeDto Create(
        Guid? id = null,
        string? name = null,
        string? description = null
        )
    {

        name ??= $"{RandomStringGenerator.Generate(20)}";
        description ??= $"{RandomStringGenerator.Generate(20)}";

        var paramaters = new[]
            {
                         new PropertyAssignment(nameof(Employee.Name),  () => name ),
                  new PropertyAssignment(nameof(Employee.Description),  () => description ),
                  new PropertyAssignment(nameof(Employee.Id),  () => id )
            };

        return ConstructorInvoker.CreateNoParamsInstance<EmployeeDto>(paramaters);
    }

    //--------------------------// 

    public static EmployeeDto Update(
        EmployeeDto employeeDto,
        Guid? id = null,
        IEnumerable<ShiftDto>? shifts = null,
         string? name = null,
         string? description = null
         )
    {

        List<PropertyAssignment> propertAssignments = [];



        if (name is not null)
            propertAssignments.Add(new PropertyAssignment(nameof(Employee.Name), () => name));

        if (description is not null)
            propertAssignments.Add(new PropertyAssignment(nameof(Employee.Description), () => description));

        if (shifts is not null)
            propertAssignments.Add(new PropertyAssignment(nameof(Employee.Shifts), () => shifts));

        if (id is not null)
            propertAssignments.Add(new PropertyAssignment(nameof(Employee.Id), () => id));


        return PrivatePropertyUpdater.UpdateProperties(employeeDto, [.. propertAssignments]);
    }



}//Cls

