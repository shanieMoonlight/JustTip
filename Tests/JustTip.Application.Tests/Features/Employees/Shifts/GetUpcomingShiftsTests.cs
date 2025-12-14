using JustTip.Application.Features.Employees.Qry.GetUpcomingShifts;
using JustTip.Application.Features.Roster;

namespace JustTip.Application.Tests.Features.Employees.Shifts;

public class GetUpcomingShiftsTests
{
    [Fact]
    public void Validator_Fails_WhenEmployeeIdEmpty()
    {
        var validator = new GetUpcomingShiftsQryValidator();
        var cmd = new GetUpcomingShiftsQry(Guid.Empty);

        var res = validator.Validate(cmd);

        res.IsValid.ShouldBeFalse();
        res.Errors.Count.ShouldBe(1);
    }

    [Fact]
    public async Task Handler_ReturnsFilteredShifts_ForEmployee()
    {
        var mockRepo = new Mock<IShiftRepo>();
        var mockUtils = new Mock<IRosterUtils>();

        var handler = new GetUpcomingShiftsQryHandler(mockRepo.Object, mockUtils.Object);

        var startUtc = DateTime.UtcNow.Date;

        var empId = Guid.NewGuid();
        var otherId = Guid.NewGuid();
        var emp1 = EmployeeDataFactory.Create(id: empId, name: "Employee 1");
        var emp2 = EmployeeDataFactory.Create(id: otherId, name: "Employee 2"); 

        var s1 = ShiftDataFactory.Create(employee: emp1, startTimeUtc: startUtc.AddDays(1), endTimeUtc: startUtc.AddDays(1).AddHours(9), date: startUtc.AddDays(1).AddHours(17));
        var s2 = ShiftDataFactory.Create(employee: emp2, startTimeUtc: startUtc.AddDays(2), endTimeUtc: startUtc.AddDays(2).AddHours(8), date: startUtc.AddDays(2).AddHours(12));

        var all = new List<Shift> { s1, s2 };
        mockRepo.Setup(r => r.ListAllFromByDateWithEmployeeAsync(startUtc, It.IsAny<CancellationToken>())).ReturnsAsync(all);

        mockUtils.Setup(u => u.ToLocalTime(It.IsAny<DateTime>())).Returns((DateTime dt) => dt);

        var res = await handler.Handle(new GetUpcomingShiftsQry(empId), CancellationToken.None);

        res.ShouldBeOfType<GenResult<List<ShiftRosterItemDto>>>();
        res.Value!.Count.ShouldBe(1);
        res.Value![0].EmployeeId.ShouldBe(empId);
        res.Value![0].EmployeeName.ShouldBe(s1.Employee!.Name);
    }
}
