using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ExchangeOffice.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeOffice.Persistence.Services
{
    public class ExchangeRateDbService: IDbService<ExchangeRate>
    {
        private readonly AppDbContext _appDbContext;

        public ExchangeRateDbService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        public async Task<ExchangeRate> Add(ExchangeRate exchangeRate)
        {
            var result = await _appDbContext.CurrencyExchangeRates.AddAsync(exchangeRate);
            await _appDbContext.SaveChangesAsync();

            return result.Entity;
            
        }

        public async Task Update()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(int exchangeRateId)
        {
            var exchangeRate = _appDbContext.CurrencyExchangeRates.FirstOrDefault(e => e.Id == exchangeRateId);
            
            _appDbContext.CurrencyExchangeRates.Remove(exchangeRate);
            await _appDbContext.SaveChangesAsync();
            
        }

        public void DeleteMany()
        {
            throw new NotImplementedException();
        }

        public async Task<ExchangeRate> Get(int exchangeRateId)
        {
            return await _appDbContext.CurrencyExchangeRates.FirstAsync(e => e.Id == exchangeRateId);
        }

        public async Task<ExchangeRate> GetByQuery(Expression<Func<ExchangeRate, bool>> expression)
        {
            return await _appDbContext.CurrencyExchangeRates
                .Include(e => e.Currency)
                .FirstAsync(expression);
        }

        public async Task<IEnumerable<ExchangeRate>> GetManyByQuery(Expression<Func<ExchangeRate, bool>> expression)
        {
            return await _appDbContext.CurrencyExchangeRates
                .Include(e => e.Currency)
                .Where(expression)
                .ToListAsync();
        }

        public IQueryable<ExchangeRate> Get(Expression<Func<ExchangeRate, bool>> queryExpression)
        {
            return _appDbContext.CurrencyExchangeRates.Where(queryExpression);
        }
    }
}