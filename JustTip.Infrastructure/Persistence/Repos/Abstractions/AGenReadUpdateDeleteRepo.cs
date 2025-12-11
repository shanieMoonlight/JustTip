using JustTip.Application.Domain.Abstractions.Repos;
using JustTip.Application.Domain.Entities.Common;

namespace JustTip.Infrastructure.Persistence.Repos.Abstractions;
internal abstract class AGenReadUpdateDeleteRepo<T>(JtDbContext db) 
    : AGenReadUpdateRepo<T>(db), IGenUpdateRepo<T>, IGenDeleteRepo<T>
    where T : JtBaseDomainEntity
{

    /// <summary>
    /// Delete item with id <paramref name="id"/>
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <returns></returns>
    /// <exception cref="CantDeleteException"></exception>
    public async Task DeleteAsync(Guid? id)
    {
        if (id == null)
            return;

        var entity = await DbCtx.Set<T>().FindAsync(id);
        if (entity == null) //Already Deleted
            return;

        DbCtx.Set<T>().Remove(entity);

        return;
    }

    //- - - - - - - - - - - //

    /// <summary>
    /// Delete item with id <paramref name="id"/>
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <returns></returns>
    /// <exception cref="CantDeleteException"></exception>
    public Task DeleteAsync(T entity)
    {
        if (entity == null) //Already Deleted
            return Task.CompletedTask;

        DbCtx.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    //- - - - - - - - - - - //

    public Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        DbCtx.Set<T>().RemoveRange(entities);
        return Task.FromResult(0);
    }

}//Cls
