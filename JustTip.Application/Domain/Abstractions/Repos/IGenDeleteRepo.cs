using JustTip.Application.Domain.Entities.Common;

namespace JustTip.Application.Domain.Abstractions.Repos;

/// <summary>
///  Delete repo
/// </summary>
public interface IGenDeleteRepo<T> where T : JtBaseDomainEntity
{

    /// <summary>
    /// Delete <paramref name="entity"/>
    /// </summary>
    /// <param name="entity">Database item</param>
    /// <returns></returns>
    Task DeleteAsync(T entity);



    //- - - - - - - - - - - - - //
    
    //TODO: DeleteMe ???/**/
    /// <summary>
    /// Delete entity with id, <paramref name="id"/>
    /// </summary>
    /// <param name="id">Entity identifier</param>
    Task DeleteAsync(Guid? id);

    //- - - - - - - - - - - - - //

    /// <summary>
    /// Delete range of <paramref name="entities"/>
    /// </summary>
    Task DeleteRangeAsync(IEnumerable<T> entities);

}//Int
