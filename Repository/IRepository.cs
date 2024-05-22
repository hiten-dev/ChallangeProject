using Domain.Models;

namespace Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(Guid id);
        Guid Insert(T entity);
        void Update(T entity);
        void SaveChanges();
    }
}
