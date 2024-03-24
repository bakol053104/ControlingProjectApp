using ControlingProjectApp.Entities;

namespace ControlingProjectApp.Repositories.Extensions
{
   public static class RepositoryExtensions
    {
        public static void AddBatch<T>(this IRepository<T> Repository, T[] items) where T : class, IEntity
        {
            foreach (var item in items)
            {
                Repository.Add(item);
            }
            Repository.Save();
        }
    }
}
