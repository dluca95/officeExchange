using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExchangeOffice.Application.Models;
using ExchangeOffice.Persistence.Entities;
using ExchangeOffice.Persistence.Services;

namespace ExchangeOffice.Application
{
    public class CurrencyAppService: IAppService<CurrencyApiModel, CurrencyQueryModel>
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

        public async Task<IEnumerable<CurrencyApiModel>> GetManyByQuery(CurrencyQueryModel queryModel)
        {
            var result = await _dbService
                .GetManyByQuery(e => e.CharCode == queryModel.CharCode);

            return result.Select(e => new CurrencyApiModel(e));
        }

        public async Task<CurrencyApiModel> GetByQuery(CurrencyQueryModel queryModel)
        {
            var result = await _dbService
                .GetByQuery(e => e.CharCode == queryModel.CharCode);

            if (result != null) return new CurrencyApiModel(result, await _bnm.GetCurrencyValue(queryModel.CharCode));
            
            var newCurrency = await _bnm.GetCurrencyByCode(queryModel.CharCode);
            if (newCurrency == null)
            {
                throw new ApplicationException("Given Currency is invalid");
            }
                
            result = await _dbService.Add(new Currency
            {
                Name = newCurrency.Name,
                CharCode = newCurrency.CharCode,
                Nominal = newCurrency.Nominal,
            });

            return new CurrencyApiModel(result, await _bnm.GetCurrencyValue(queryModel.CharCode));
        }
    }
}