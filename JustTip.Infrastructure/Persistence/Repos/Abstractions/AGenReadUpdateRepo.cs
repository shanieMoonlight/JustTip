using JustTip.Application.Domain.Abstractions.Repos;
using JustTip.Application.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace JustTip.Infrastructure.Persistence.Repos.Abstractions;
internal abstract class AGenReadUpdateRepo<T>(JtDbContext db) 
    : AGenReadRepo<T>(db), IGenUpdateRepo<T>
    where T : JtBaseDomainEntity
{

    public Task<T> UpdateAsync(T entity)
    {
        DbCtx.Entry(entity).State = EntityState.Modified;
        return Task.FromResult(entity);
    }

    //- - - - - - - - - - - //

    public Task UpdateRangeAsync(IEnumerable<T> entities)
    {
         DbCtx.Set<T>().UpdateRange(entities);
        return Task.CompletedTask;
    }

}//Cls
