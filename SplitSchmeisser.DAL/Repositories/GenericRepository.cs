using Microsoft.EntityFrameworkCore;
using SplitSchmeisser.DAL.Context;
using SplitSchmeisser.DAL.Entities.Base;
using SplitSchmeisser.DAL.Extentions;
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

        public IQueryable<TEntity> GetAll()
        {
            return context.Set<TEntity>().IncludeAll();
        }

        public PagedList<TEntity> GetPagedList(int pageSize, int pageIndex)
        {
            var count = this.GetAll().Count();
            var items = this.GetAll().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<TEntity>(items, count, pageIndex, pageSize);
        }

        public async Task<TEntity> GetById(int id)
        {
            return await context.Set<TEntity>().IncludeAll()
                        .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Insert(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        //TODO: investidate
        //method was added to create new users in a sync mode
        //issue with Async creation
        public void InsertSync(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            context.SaveChanges();
        }

        public async Task Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
        }

        
    }
}
