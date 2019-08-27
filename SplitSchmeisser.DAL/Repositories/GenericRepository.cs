using SplitSchmeisser.DAL.Context;
using SplitSchmeisser.DAL.Entities.Base;
using SplitSchmeisser.DAL.Infrasructure;
using SplitSchmeisser.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitSchmeisser.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private SchmeisserContext context;

        public GenericRepository(SchmeisserContext context)
        {
            this.context = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return context.Set<TEntity>();
        }

        public PagedList<TEntity> GetPagedList(int pageSize, int pageIndex)
        {
            var count = this.GetAll().Count();
            var items = this.GetAll().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<TEntity>(items, count, pageIndex, pageSize);
        }

        public async Task<TEntity> GetById(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);                
        }

        public async Task Insert(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
                return null;

            TEntity existing = await context.Set<TEntity>().FindAsync(entity.Id);
            if (existing != null)
            {
                context.Entry(existing).CurrentValues.SetValues(entity);
                await context.SaveChangesAsync();
            }
            return existing;
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
        }        
    }
}
