using JustTip.Application.Abstractions;
using JustTip.Application.Features.Employees;

namespace JustTip.Application.Features.Maintenance.Initialize;
public class InitializeDbCmdHandler(IDbInitializer _dbInitializer) : IJtCommandHandler_NonUow<InitializeDbCmd, List<EmployeeDto>>
{
    private readonly Random _rnd = new();

    //--------------------------//

    public async Task<GenResult<List<EmployeeDto>>> Handle(InitializeDbCmd request, CancellationToken cancellationToken)
    {
        var employees = GenerateEmployees();
        foreach (var employee in employees)
        {
            AddShifts(employee);
        }

        var tips = GenerateTips();
        await _dbInitializer.InitializeAsync(employees, tips);

        List<EmployeeDto> dtos = [.. employees.Select(g => g.ToDto())];

        return GenResult<List<EmployeeDto>>.Success(dtos);
    }

    //--------------------------//

    private static List<Employee> GenerateEmployees()
    {
        var desc = "This is the default employee created during database initialization.";

        return
        [
            Employee.Create(name: "Bob", description: desc),
            Employee.Create(name: "Alice", description: desc),
            Employee.Create(name: "Charlie", description: desc),
            Employee.Create(name: "Mary", description: desc),
            Employee.Create(name: "John", description: desc),
            Employee.Create(name: "Emma", description: desc),
            Employee.Create(name: "Oliver", description: desc),
            Employee.Create(name: "Olivia", description: desc),
            Employee.Create(name: "Liam", description: desc),
            Employee.Create(name: "Sophia", description: desc),
            Employee.Create(name: "Noah", description: desc),
            Employee.Create(name: "Ava", description: desc),
            Employee.Create(name: "Mason", description: desc),
            Employee.Create(name: "Isabella", description: desc),
            Employee.Create(name: "Lucas", description: desc),
            Employee.Create(name: "Mia", description: desc),
            Employee.Create(name: "Ethan", description: desc),
            Employee.Create(name: "Charlotte", description: desc),
            Employee.Create(name: "Logan", description: desc),
            Employee.Create(name: "Amelia", description: desc)
        ];
    }


    //- - - - - - - - - - - - - //

    private void AddShifts(Employee employee)
    {
        // target 50 shifts
        int target = 50;

        // start from two weeks ago
        var date = DateTime.Now.Date.AddDays(-14);
        var end = DateTime.Now.Date.AddDays(14);

        int attempts = 0;
        int maxAttempts = 1000;

        int consecutive = 0;
        const int maxConsecutive = 4;
        // probability an employee works on a given day
        double workProb = 0.4;

        while (employee.Shifts.Count < target && attempts < maxAttempts)
        {
            attempts++;

            // if already worked maxConsecutive days force a day off
            bool workToday = consecutive < maxConsecutive && _rnd.NextDouble() < workProb;

            if (!workToday)
            {
                consecutive = 0;
                // advance day
                date = date.AddDays(1);
                if (date > end)
                    end = end.AddDays(1);
                continue;
            }

            // pick duration between 4 and 12 hours
            int durationHours = _rnd.Next(4, 13); // 4..12

            // pick a start hour so that start + duration <= 23
            int latestStart = Math.Max(0, 23 - durationHours);
            int startHour = _rnd.Next(0, latestStart + 1);

            var start = date.AddHours(startHour);
            var endTime = start.AddHours(durationHours);

            try
            {
                employee.AddShift(date, start, endTime);
                consecutive++;
            }
            catch
            {
                // ignore invalid shifts (e.g., date in the past)
                consecutive = 0;
            }

            // advance day
            date = date.AddDays(1);
            if (date > end)
                end = end.AddDays(1);
        }

    }


    private List<Tip> GenerateTips()
    {
        var tips = new List<Tip>();

        int target = 2000;

        // range: from -14 days to +0 days around now
        var minMinutes = -14 * 24 * 60;
        var maxMinutes = 0;

        for (int i = 0; i < target; i++)
        {
            // pick random minutes offset in range
            var offsetMinutes = _rnd.Next(minMinutes, maxMinutes + 1);
            var dt = DateTime.Now.AddMinutes(offsetMinutes);

            // random amount between 0.25 and 50.00, rounded to 2 decimals
            var amountDouble = _rnd.NextDouble() * (50.0 - 0.25) + 0.25;
            var amount = Math.Round((decimal)amountDouble, 2);

            // create tip (Tip.Create will validate amount > 0)
            tips.Add(Tip.Create(dt, amount));
        }

        return tips;
    }


}//Cls
