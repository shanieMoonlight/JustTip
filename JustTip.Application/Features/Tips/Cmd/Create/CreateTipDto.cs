using JustTip.Application.Domain.Entities.Tips;

namespace JustTip.Application.Features.Tips.Cmd.Create;
public class CreateTipDto
{
    public DateTime? DateTime { get; set; }
    public decimal AmountEuros { get; set; }
    
    #region ModelBindingCtor
    public CreateTipDto() { }
    #endregion


}//Cls

