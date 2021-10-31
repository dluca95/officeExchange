using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExchangeOffice.Application.Models;
using ExchangeOffice.Persistence.Entities;
using ExchangeOffice.Persistence.Services;

namespace ExchangeOffice.Application
{
    public class ExchangeRateAppService: IAppService<ExchangeRateModel>
    {
        private readonly IDbService<ExchangeRate> _exchangeRateDbService;
        private readonly IAppService<CurrencyApiModel> _currencyAppService;
        private readonly IDbService<Currency> _currencyDbService;


        public ExchangeRateAppService(IDbService<ExchangeRate> exchangeRateDbService,
            IAppService<CurrencyApiModel> currencyAppService, IDbService<Currency> currencyDbService
            )
        {
            _exchangeRateDbService = exchangeRateDbService;
            _currencyAppService = currencyAppService;
            _currencyDbService = currencyDbService;
        }
        
        public async Task<ExchangeRateModel> Add(ExchangeRateModel model)
        {
            if (!model.BuyPrice.HasValue || !model.SellPrice.HasValue)
            {
                throw new ArgumentException("New Exchange rate requires both buy and sell price");
            }

            var currency = await _currencyAppService.Get(model.CurrencyCharCode);
            
            var currencyEntity = await _currencyDbService.Get(currency.Id.Value);
            
            var newExchangeRate = new ExchangeRate
            {
                BuyPrice = model.BuyPrice.Value,
                SellPrice = model.SellPrice.Value,
                CreatedAt = DateTime.Now,
                Currency = currencyEntity
            };

            var result = await _exchangeRateDbService.Add(newExchangeRate);
            model.Id = result.Id;

            return model;
        }

        public Task Update(ExchangeRateModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<ExchangeRateModel> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ExchangeRateModel> Get(string identifier)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ExchangeRateModel>> Get()
        {
            throw new System.NotImplementedException();
        }
    }
}