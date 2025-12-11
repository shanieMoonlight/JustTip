using JustTip.Application.Domain.Abstractions.Repos;
using JustTip.Application.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace JustTip.Infrastructure.Persistence.Repos.Abstractions;
internal abstract class AGenReadRepo<T>(JtDbContext db) : IGenReadRepo<T> where T : JtBaseDomainEntity
{

    protected readonly JtDbContext DbCtx = db;

    //----------------------//

    public Task<int> CountAsync() => 
        DbCtx.Set<T>().CountAsync();

    //----------------------//

    public async Task<bool> ExistsAsync(Guid? id)
    {
        if (id == null)
            return false;

        return await FirstOrDefaultByIdAsync(id) is not null;

    }

    //-----------------------//

    public async Task<IReadOnlyList<T>> ListAllTrackedAsync() =>
        await DbCtx.Set<T>()
            .ToListAsync();

    //- - - - - - - - - - - //

    public async Task<IReadOnlyList<T>> ListAllAsync() =>
        await DbCtx.Set<T>()
            .AsNoTracking()
            .ToListAsync();

    //----------------------//

    public async Task<T?> FirstOrDefaultByIdAsync(Guid? id)
    {
        if (id == null)
            return default;

        return await DbCtx.Set<T>().FindAsync(id);
    }

    //----------------------//


    //public virtual Task<Page<T>> PageAsync(int pageNumber, int pageSize, IEnumerable<SortRequest> sortList, IEnumerable<FilterRequest>? filterList = null)
    //{
    //    var skip = (pageNumber - 1) * pageSize;

    //    var data = DbCtx.Set<T>()
    //       .OrderBy(x => x.LastModifiedDate) //Prefer most recent if not order not set. Can be overridden below
    //       .AddFiltering(filterList, GetFilteringPropertySelectorLambda)
    //       .AddEfSorting(sortList, GetCustomSortBuider())
    //       .AsNoTracking();

    //    return Task.FromResult(
    //        new Page<T>(data, pageNumber, pageSize)
    //    );

    //}

    ////- - - - - - - - - - - //

    //protected virtual string GetFilteringPropertySelectorLambda(string field) =>
    //     string.IsNullOrWhiteSpace(field) ? field : field.First().ToString().ToUpper() + field[1..];

    ////- - - - - - - - - - - //

    //protected virtual CustomSortExpressionBuilder<T>? GetCustomSortBuider() => null;

}//Cls
