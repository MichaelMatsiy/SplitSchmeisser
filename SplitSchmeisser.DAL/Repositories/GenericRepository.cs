﻿using SplitSchmeisser.DAL.Entities.Base;
using SplitSchmeisser.DAL.Interfaces;
using SplitSchmeisser.DAL.Context;
using System.Threading.Tasks;
using System.Linq;

namespace SplitSchmeisser.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private SchmeisserContext context;
        private bool delay = false;

        public GenericRepository(SchmeisserContext context)
        {
            this.context = context;
        }

        public IQueryable<TEntity> GetAll()
        {
            return context.Set<TEntity>();
        }

        public async Task<TEntity> GetById(int id)
        {
            Logger.Write($"Record with id: {id} was requested", delay);
            return await context.Set<TEntity>().FindAsync(id);                
        }

        public async Task Insert(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();

            Logger.Write($"Record with id: {entity.Id} was created", delay);
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

                Logger.Write($"Record with id: {entity.Id} was updated", delay);
            }
            return existing;
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();

            Logger.Write($"Record with id: {entity.Id} was removed", delay);
        }        
    }
}
