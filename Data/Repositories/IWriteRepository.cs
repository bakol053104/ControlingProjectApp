using ControlingProjectApp.Data.Entities;

namespace ControlingProjectApp.Data.Repositories;

public interface IWriteRepository<in T> where T : class, IEntity
{
    void Add(T item);

    void Remove(T item);

    void Update(T item);
}
