namespace JustTip.Application.Features.Employees;
internal static class ShiftExtensions
{
    public static Shift Update(this Shift model, ShiftDto dto) =>
        model.Update(
            date: dto.Date,
            startTimeUtc: dto.StartTimeUtc,
            endTimeUtc: dto.EndTimeUtc
        );

    //--------------------------// 


    public static ShiftDto ToDto(this Shift model) =>
        new(model);


}//Cls
