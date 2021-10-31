using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExchangeOffice.Application.Models;
using ExchangeOffice.Persistence.Entities;
using ExchangeOffice.Persistence.Services;

namespace ExchangeOffice.Application
{
    public class CurrencyAppService: IAppService<CurrencyApiModel>
    {
        private readonly IDbService<Currency> _dbService;
        private readonly BnmService _bnm;
        
        public CurrencyAppService(IDbService<Currency> dbService, BnmService bnmService)
        {
            _dbService = dbService;
            _bnm = bnmService;
        }
        
        public Task<CurrencyApiModel> Add(CurrencyApiModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(CurrencyApiModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<CurrencyApiModel> Get(int id)
        {
            throw new System.NotImplementedException();
        }
        
        public async Task<CurrencyApiModel> Get(string charCode)
        {
            var result = await _dbService
                .GetByQuery(c => c.CharCode == charCode);
            var value = new float();
            
            if (result == null)
            {
                var newCurrency = await _bnm.GetCurrencyByCode(charCode);
                if (newCurrency == null)
                {
                    throw new ApplicationException("Given Currency is invalid");
                }
                
                value = newCurrency.Value;
                
                result = await _dbService.Add(new Currency
                {
                    Name = newCurrency.Name,
                    CharCode = newCurrency.CharCode,
                    Nominal = newCurrency.Nominal
                });
            }

            return new CurrencyApiModel
            {
                Name = result.Name,
                CharCode = result.CharCode,
                Value = value,
                Id = result.Id
            };
        }

        public Task<IEnumerable<CurrencyApiModel>> Get()
        {
            throw new System.NotImplementedException();
        }
    }
}