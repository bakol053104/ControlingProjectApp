using ControlingProjectApp.Data.Entities;

namespace ControlingProjectApp.Data.Repositories;

public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
    where T : class, IEntity
{
    event EventHandler<T?>? ItemAdded;
    event EventHandler<T?>? ItemRemoved;
}
