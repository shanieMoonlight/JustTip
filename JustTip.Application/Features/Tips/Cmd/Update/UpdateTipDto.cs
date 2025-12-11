using JustTip.Application.Domain.Entities.Tips;

namespace JustTip.Application.Features.Tips;
public class UpdateTipDto
{
    public Guid Id { get; set; }
    public DateTime? DateTime { get; set; }
    public decimal? AmountEuros { get; set; }
    
    #region ModelBindingCtor
    public UpdateTipDto() { }
    #endregion


}//Cls

