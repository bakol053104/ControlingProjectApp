using ControlingProjectApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlingProjectApp.Data.Repositories;

public class SqlRepository<T> : IRepository<T> where T : class, IEntity, new()
{
    private readonly ControlingProjectAppDbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public event EventHandler<T?>? ItemAdded;
    public event EventHandler<T?>? ItemRemoved;

    public SqlRepository(ControlingProjectAppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public T? GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public void Add(T item)
    {
        _dbSet.Add(item);
        _dbContext.SaveChanges();
        ItemAdded?.Invoke(this, item);
    }

    public void Remove(T item)
    {
        _dbSet.Remove(item);
        _dbContext.SaveChanges();
        ItemRemoved?.Invoke(this, item);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}



