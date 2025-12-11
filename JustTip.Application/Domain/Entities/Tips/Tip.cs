using JustTip.Application.Domain.Entities.Common;
using JustTip.Application.Utility.Exceptions;

namespace JustTip.Application.Domain.Entities.Tips;
public class Tip : JtBaseDomainEntity
{
    public DateTime DateTime { get; private set; }
    public decimal AmountEuros { get; private set; }


    //--------------------------// 

    #region Ef Core Ctor
    protected Tip() { }
    #endregion


    private Tip(DateTime dateTime, decimal amountEuros)
    {
        DateTime = dateTime;
        AmountEuros = amountEuros;
    }

    //--------------------------// 

    public static Tip Create(DateTime dateTime, decimal amountEuros)
    {
        if (amountEuros <= 0)
            throw new InvalidDomainDataException(nameof(Tip), $"Tip amount must be greater than zero. Received: {amountEuros}");

        return new Tip(dateTime, amountEuros);
    }

    //--------------------------// 

    public Tip Update(DateTime? dateTime, decimal? amountEuros)
    {
        if (amountEuros.HasValue && amountEuros.Value <= 0)
            throw new InvalidDomainDataException(nameof(Tip), $"Tip amount must be greater than zero. Received: {amountEuros}");

        if(dateTime.HasValue)
            DateTime = dateTime.Value;

        if(amountEuros.HasValue)
            AmountEuros = amountEuros.Value;

        return this;
    }


}
