using JustTip.Application.Domain.Abstractions.Repos;
using JustTip.Application.Domain.Entities.Common;

namespace JustTip.Infrastructure.Persistence.Repos.Abstractions;
internal abstract class AGenCrudRepo<T>(JtDbContext db) 
    : AGenReadUpdateDeleteRepo<T>(db), IGenCrudRepo<T> where T : JtBaseDomainEntity
{

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default) =>
        (await DbCtx.Set<T>().AddAsync(entity, cancellationToken))
            .Entity;

    //--------------------------// 

    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default) =>
        await DbCtx.Set<T>().AddRangeAsync(entities, cancellationToken);

}//Cls
