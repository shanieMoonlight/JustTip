using JustTip.Application.Features.Employees;
using JustTip.Application.Features.Tips.Cmd.Create;

namespace JustTip.Application.Features.Tips;
internal static class TipExtensions
{
    public static Tip Update(this Tip model, UpdateTipDto dto) =>
        model.Update(
            dateTime: dto.DateTime, 
            amountEuros: dto.AmountEuros
        );

    //--------------------------// 

    public static Tip ToModel(this CreateTipDto dto) =>
        Tip.Create(
            dateTime: dto.DateTime ?? DateTime.UtcNow,
            amountEuros: dto.AmountEuros
        );

    //--------------------------// 


    public static TipDto ToDto(this Tip model) =>
        new(model);


}//Cls
