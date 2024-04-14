using ControlingProjectApp.Data.Entities;

namespace ControlingProjectApp.Data.Repositories;

public interface IReadRepository<out T> where T : class, IEntity
{
    IEnumerable<T> GetAll();

    T? GetById(int id);
}
