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

        public ExchangeRateAppService(IDbService<ExchangeRate> exchangeRateDbService,
            IAppService<CurrencyApiModel> currencyAppService)
        {
            _exchangeRateDbService = exchangeRateDbService;
            _currencyAppService = currencyAppService;
        }
        
        public async Task<ExchangeRateModel> Add(ExchangeRateModel model)
        {
            if (!model.BuyPrice.HasValue || !model.SellPrice.HasValue)
            {
                throw new ArgumentException("New Exchange rate requires both buy and sell price");
            }

            var currency = await _currencyAppService.Get(model.CurrencyCharCode);

            if (model.BuyPrice > (currency.Value + (currency.Value / 100))
                || model.BuyPrice < (currency.Value - (currency.Value / 100))
                || model.SellPrice > (currency.Value + (currency.Value/ 100))
                || model.SellPrice < (currency.Value) - (currency.Value / 100))
            {
                throw new ArgumentException("Exchange rate prices are not according to official rates");
            } 
            
            var newExchangeRate = new ExchangeRate
            {
                BuyPrice = model.BuyPrice.Value,
                SellPrice = model.SellPrice.Value,
                CreatedAt = DateTime.Now,
                CurrencyId = currency.Id ?? 0
            };

            var result = await _exchangeRateDbService.Add(newExchangeRate);
            model.Id = result.Id;

            return model;
        }

        public async Task Update(ExchangeRateModel model)
        {
            var currency = await _currencyAppService.Get(model.CurrencyCharCode);
            if (currency == null)
            {
                throw new ApplicationException("Currency is invalid");
            }
            
            if (model.BuyPrice > (currency.Value + (currency.Value / 100))
                || model.BuyPrice < (currency.Value - (currency.Value / 100))
                || model.SellPrice > (currency.Value + (currency.Value/ 100))
                || model.SellPrice < (currency.Value) - (currency.Value / 100))
            {
                throw new ArgumentException("Exchange rate prices are not according to official rates");
            }

            var entity = await _exchangeRateDbService.Get(model.Id.Value);

            entity.BuyPrice = model.BuyPrice.Value;
            entity.SellPrice = model.SellPrice.Value;
            await _exchangeRateDbService.Update();
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