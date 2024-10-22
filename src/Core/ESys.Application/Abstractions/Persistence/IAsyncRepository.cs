using System.Numerics;

namespace ESys.Application.Abstractions.Persistence;
public interface IAsyncRepository<T> where T : class
{
    Task<T?> GetById(int id);
    Task<IReadOnlyList<T>> ListAll();
    Task<T> Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}
