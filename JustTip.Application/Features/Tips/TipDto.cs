using JustTip.Application.Domain.Entities.Tips;

namespace JustTip.Application.Features.Tips;
public class TipDto
{
    public Guid Id { get; set; }

    public DateTime DateTime { get; set; }
    public decimal AmountEuros { get; set; }

    //--------------------------// 

    #region ModelBindingCtor
    public TipDto() { }
    #endregion

    public TipDto(Tip mdl)
    {
        DateTime = mdl.DateTime;
        AmountEuros = mdl.AmountEuros;
        Id = mdl.Id;

    }


}//Cls

