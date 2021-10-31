using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ExchangeOffice.Persistence.Entities;

namespace ExchangeOffice.Persistence.Services
{
    public interface IDbService<TEntity> where TEntity: EntityModel
    {
        public Task<TEntity> Add(TEntity entity);
        
        public Task Update();
        
        public Task Delete(int entityId);

        public Task<TEntity> Get(int entityId);
        
        public Task<TEntity> GetByQuery(Expression<Func<TEntity, bool>> expression);
        
        public Task<IEnumerable<TEntity>> GetManyByQuery(Expression<Func<TEntity, bool>> expression);
        
        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> queryExpression);
    }
}