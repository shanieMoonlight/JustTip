using JustTip.Application.Domain.Entities.Common;

namespace JustTip.Application.Domain.Abstractions.Repos;

/// <summary>
/// Read repo
/// </summary>
public interface IGenReadRepo<T> where T : JtBaseDomainEntity
{
    /// <summary>
    /// Gets the number of Items in the Db
    /// </summary>
    Task<int> CountAsync();

    //- - - - - - - - - - - - - //

    /// <summary>
    /// Does an Entity with id, <paramref name="id"/> exist?
    /// </summary>
    /// <param name="id">Entity identifier</param>
    /// <returns>True if entity was found</returns>
    Task<bool> ExistsAsync(Guid? id);

    //- - - - - - - - - - - - - //

    /// <summary>
    /// Get the entity with id, <paramref name="id"/>
    /// </summary>
    /// <param name="id">Entity identifier</param>
    /// <returns></returns>
    Task<T?> FirstOrDefaultByIdAsync(Guid? id);

    //- - - - - - - - - - - - - //


    /// <summary>
    /// Returns all entities as no-tracking
    /// </summary>
    /// <returns></returns>
    Task<IReadOnlyList<T>> ListAllAsync();

    //- - - - - - - - - - - - - //

    //Task<Page<T>> PageAsync(int pageNumber, int pageSize, IEnumerable<SortRequest> sortList, IEnumerable<FilterRequest>? filterList = null);

     

}//int
