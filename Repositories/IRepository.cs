using ControlingProjectApp.Entities;

namespace ControlingProjectApp.Repositories
{
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
        where T : class, IEntity
    {
    }
}
