using Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repo
{
    public abstract class Repository<TEntity, TContext>
        where TEntity : Entity
        where TContext : DbContext, new()
    {
        protected TContext context { get; set; }
        public Repository()
        {
            this.context = new TContext();
        }
        public async virtual Task<bool> Add(TEntity entity)
        {
            try
            {
                this.context.Set<TEntity>().Add(entity);
                await this.context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> AddRange(List<TEntity> entities)
        {
            try
            {
                this.context.Set<TEntity>().AddRange(entities);
                await this.context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async virtual Task<bool> Update(TEntity entity)
        {
            try
            {
                this.context.Set<TEntity>().Update(entity);
                await this.context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> UpdateRange(List<TEntity> entities)
        {
            try
            {
                this.context.Set<TEntity>().UpdateRange(entities);
                await this.context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async virtual Task<bool> Delete(Guid id)
        {
            try
            {
                var entity = this.context.Set<TEntity>().Where(x => x.Id.Equals(id)).FirstOrDefault();
                this.context.Set<TEntity>().Remove(entity);
                await this.context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> DeleteRange(List<TEntity> entities)
        {
            try
            {
                this.context.Set<TEntity>().RemoveRange(entities);
                await this.context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<TEntity> Get(Guid id)
        {
            try
            {
                var entity = this.context.Set<TEntity>().Where(x => x.Id.Equals(id)).FirstOrDefault();
                return await Task.FromResult(entity);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IQueryable<TEntity>> GetAll(Guid id)
        {
            try
            {
                var entity = this.context.Set<TEntity>().Where(x => x.Id.Equals(id));
                return await Task.FromResult(entity);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IQueryable<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                var entities = this.context.Set<TEntity>().Where(expression);
                return await Task.FromResult(entities);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<TEntity> GetAny(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                var entities = this.context.Set<TEntity>().Where(expression).FirstOrDefault();
                return await Task.FromResult(entities);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Guid GetGuid()
        {
            return Guid.NewGuid();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
