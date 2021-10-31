using System;
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
        
        public void DeleteMany();
        
        public Task<TEntity> Get(int entityId);
        
        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> queryExpression);
    }
}