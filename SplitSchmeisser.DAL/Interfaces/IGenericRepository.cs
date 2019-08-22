using SplitSchmeisser.DAL.Entities.Base;
using SplitSchmeisser.DAL.Infrasructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitSchmeisser.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll();

        PagedList<TEntity> GetPagedList(int pageSize, int pageIndex);

        Task<TEntity> GetById(int id);

        Task Insert(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task Delete(int id);
    }
}
