using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ExchangeOffice.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeOffice.Persistence.Services
{
    public class CurrencyDbService: IDbService<Currency>
    {
        private readonly AppDbContext _appDbContext;

        public CurrencyDbService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        public async Task<Currency> Add(Currency currency)
        {
            var result = await _appDbContext.Currencies.AddAsync(currency);
            await _appDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task Update()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(int currencyId)
        {
            var currency = _appDbContext.Currencies.FirstOrDefault(e => e.Id == currencyId);
            
            _appDbContext.Currencies.Remove(currency);
            
            await _appDbContext.SaveChangesAsync();
        }

        public void DeleteMany()
        {
            throw new NotImplementedException();
        }

        public async Task<Currency> Get(int currencyId)
        {
            return await _appDbContext.Currencies.FirstAsync(e => e.Id == currencyId);
        }

        public async Task<Currency> GetByQuery(Expression<Func<Currency, bool>> expression)
        {
            var result = await _appDbContext.Currencies.FirstOrDefaultAsync(expression);
          
            return result;
        }

        public Task<IEnumerable<Currency>> GetManyByQuery(Expression<Func<Currency, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Currency> Get(Expression<Func<Currency, bool>> queryExpression)
        {
            return _appDbContext.Currencies.Where(queryExpression);
        }
    }
}