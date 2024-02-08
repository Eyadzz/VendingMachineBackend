namespace Application.Contracts.Persistence;

/// <summary>
/// Represents a generic interface for basic repository operations on entities of type T.
/// </summary>
/// <typeparam name="T">The type of entities that the repository manages.</typeparam>
public interface IBaseRepository<T> where T : class
{
    /// <summary>
    /// Asynchronously retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous operation, yielding the entity with the specified ID, or null if not found.</returns>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Asynchronously retrieves a collection of all entities of type T in the repository.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous operation, yielding a collection of all entities of type T.</returns>
    Task<ICollection<T>> ListAllAsync();

    /// <summary>
    /// Asynchronously adds an entity of type T to the repository.
    /// </summary>
    /// <param name="entity">The entity to be added.</param>
    /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous operation, yielding the added entity.</returns>
    Task<T> AddAsync(T entity);
    
    /// <summary>
    /// Deletes an entity of type T in the repository.
    ///  </summary>
    ///  <param name="entity">The entity to be deleted.</param>
    ///  <returns>A <see cref="Task{TResult}"/> that represents the asynchronous operation.</returns>
    void Delete(T entity);
}