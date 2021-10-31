using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExchangeOffice.Application.Models;
using ExchangeOffice.Persistence.Entities;
using ExchangeOffice.Persistence.Services;

namespace ExchangeOffice.Application
{
    public class ExchangeRateAppService: IAppService<ExchangeRateModel, ExchangeRateQueryModel>
    {
        private readonly IAppService<CurrencyApiModel, CurrencyQueryModel> _currencyAppService;
        private readonly IDbService<ExchangeRate> _exchangeRateDbService;

        public ExchangeRateAppService(IDbService<ExchangeRate> exchangeRateDbService,
            IAppService<CurrencyApiModel, CurrencyQueryModel> currencyAppService)
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

            var currency = await _currencyAppService
                .GetByQuery(new CurrencyQueryModel {CharCode = model.CurrencyCharCode});

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
            var currency = await _currencyAppService
                .GetByQuery(new CurrencyQueryModel { CharCode = model.CurrencyCharCode});
            
            if (currency == null)
            {
                throw new ApplicationException("Currency is invalid");
            }
            
            if (!CurrencyHelper.ExchangeRateIsValid(model, currency))
            {
                throw new ArgumentException("Exchange rate prices are not according to official rates");
            }

            var entity = await _exchangeRateDbService.Get(model.Id.Value);

            entity.BuyPrice = model.BuyPrice.Value;
            entity.SellPrice = model.SellPrice.Value;
            await _exchangeRateDbService.Update();
        }
        public async Task<IEnumerable<ExchangeRateModel>> GetManyByQuery(ExchangeRateQueryModel queryModel)
        {

            var result = await _exchangeRateDbService
                .GetManyByQuery(e => 
                    queryModel.OnDate != null && e.CreatedAt.Date == queryModel.OnDate.Value.Date ||
                    queryModel.CurrencyCharCode != null && e.Currency.CharCode == queryModel.CurrencyCharCode);
            
            return result.Select(e => new ExchangeRateModel
                {
                    Id = e.Id,
                    BuyPrice = e.BuyPrice,
                    CurrencyCharCode = e.Currency.CharCode,
                    SellPrice = e.SellPrice
                });
        }

        public async Task<ExchangeRateModel> GetByQuery(ExchangeRateQueryModel queryModel)
        {
            var result = await _exchangeRateDbService.GetByQuery(e =>
                queryModel.OnDate != null && e.CreatedAt.Date == queryModel.OnDate.Value.Date ||
                queryModel.CurrencyCharCode != null && e.Currency.CharCode == queryModel.CurrencyCharCode);
                
            
            return new ExchangeRateModel
            {
                BuyPrice = result.BuyPrice,
                SellPrice = result.SellPrice,
                CurrencyCharCode = result.Currency.CharCode,
                Id = result.Id
            };
        }
    }
}