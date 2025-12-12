using JustTip.Application.Domain.Entities.Common;

namespace JustTip.Application.Domain.Abstractions.Repos;

/// <summary>
/// Create, Read, Update, Delete repo
/// </summary>
public interface IGenCrudRepo<T> :
    IGenCreateRepo<T>,
    IGenReadRepo<T>,
    IGenUpdateRepo<T>,
    IGenDeleteRepo<T> where T : JtBaseDomainEntity
{
}
