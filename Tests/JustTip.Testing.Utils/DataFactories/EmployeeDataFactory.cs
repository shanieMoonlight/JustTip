namespace JustTip.Testing.Utils.DataFactories;

public static class EmployeeDataFactory
{

    public static List<Employee> CreateMany(int count = 20) => 
        [.. IdGenerator.GetGuidIdsList(count).Select(id => Create(id))];

    //- - - - - - - - - - - - - //

    public static Employee Create(
          Guid? id = null,
		string? name = null,
		string? description = null,
		List<Shift>? shifts = null,
		DateTime? createdDate = null,
		DateTime? lastModifiedDate = null
        ) 
    {

       name ??= $"{RandomStringGenerator.Generate(20)}";
		description ??= $"{RandomStringGenerator.Generate(20)}";
		shifts ??= [];
		id ??= Guid.NewGuid();

        var paramaters = new[]
            {
                         new PropertyAssignment(nameof(Employee.Name),  () => name ),
		          new PropertyAssignment(nameof(Employee.Description),  () => description ),
		          new PropertyAssignment(nameof(Employee.Shifts),  () => shifts ),
		          new PropertyAssignment(nameof(Employee.Id),  () => id ),
		          new PropertyAssignment(nameof(Employee.CreatedDate),  () => createdDate ),
		          new PropertyAssignment(nameof(Employee.LastModifiedDate),  () => lastModifiedDate )
            };

       return ConstructorInvoker.CreateNoParamsInstance<Employee>(paramaters);
    }

    //--------------------------// 

   public static Employee Update(
          Employee employee,
          Guid? id = null,
		string? name = null,
		string? description = null,
		List<Shift>? shifts = null,
		DateTime? createdDate = null,
		DateTime? lastModifiedDate = null
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
		
            if (createdDate is not null)
                    propertAssignments.Add(new PropertyAssignment(nameof(Employee.CreatedDate), () => createdDate));
		
            if (lastModifiedDate is not null)
                    propertAssignments.Add(new PropertyAssignment(nameof(Employee.LastModifiedDate), () => lastModifiedDate));
 

       return PrivatePropertyUpdater.UpdateProperties(employee, [.. propertAssignments]);
    }



}//Cls

