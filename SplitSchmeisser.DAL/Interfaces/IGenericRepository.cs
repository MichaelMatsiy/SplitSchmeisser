using SplitSchmeisser.DAL.Entities.Base;
using System.Threading.Tasks;
using System.Linq;


namespace SplitSchmeisser.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(int id);

        Task Insert(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task Delete(int id);
    }
}
