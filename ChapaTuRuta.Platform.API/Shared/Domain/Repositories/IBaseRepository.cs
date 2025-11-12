namespace ChapaTuRuta.Platform.API.Shared.Domain.Repositories;


/// <summary>
///     Base repository interface for all repositories
/// </summary>
/// <remarks>
///     This interface defines the basic CRUD operation for all repositories
/// </remarks>
/// <typeparam name="TEntity">The entity type</typeparam>
public interface IBaseRepository<TEntity>
{
    /// <summary>
    /// Add entity to repository
    /// </summary>
    /// <param name="entity">Entity object to add</param>
    /// <returns></returns>
    Task AddAsync(TEntity entity);

    /// <summary>
    /// Find entity by ID
    /// </summary>
    /// <param name="id">The entity ID to find</param>
    /// <returns>Entity object if found</returns>
    Task<TEntity?> FindByIdAsync(int id);
    
    /// <summary>
    ///     Update entity
    /// </summary>
    /// <param name="entity">The entity object to update</param>
    void Update(TEntity entity);
    
    /// <summary>
    ///     Remove an entity
    /// </summary>
    /// <param name="entity">The entity object to remove</param>
    void Remove(TEntity entity);

    /// <summary>
    ///     Get all entities
    /// </summary>
    /// <returns>An enumerable containing all entity objects</returns>
    Task<IEnumerable<TEntity>> ListAsync();
}