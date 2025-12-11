using JustTip.Application.Abstractions;
using JustTip.Application.Features.Employees.Cmd;

namespace JustTip.Application.Features.Maintenance.Initialize;
public class InitializeDbCmdHandler(IDbInitializer _dbInitializer) : IJtCommandHandler_NonUow<InitializeDbCmd, List<EmployeeDto>>
{
    public async Task<GenResult<List<EmployeeDto>>> Handle(InitializeDbCmd request, CancellationToken cancellationToken)
    {
        var employees = GenerateEmployees();
        //foreach (var employee in employees)
        //{
        //    GenerateShifts(employee);
        //}


        await _dbInitializer.InitializeAsync(employees);

        List<EmployeeDto> dtos = [.. employees.Select(g => g.ToDto())];

        return GenResult<List<EmployeeDto>>.Success(dtos);
    }

    //--------------------------//

    private static List<Employee> GenerateEmployees()
    {
        var employee1 = Employee.Create(
           name: "Bob",
           description: "This is the default employee created during database initialization.");

        var employee2 = Employee.Create(
            name: "Alice",
            description: "This is the default employee created during database initialization.");

        var employee3 = Employee.Create(
            name: "Charlie",
            description: "This is the default employee created during database initialization.");

        var employee4 = Employee.Create(
            name: "Mary",
            description: "This is the default employee created during database initialization.");


        return [employee1, employee2, employee3, employee4];
    }



}//Cls
