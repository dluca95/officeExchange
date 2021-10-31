using System.Collections.Generic;
using ExchangeOffice.Persistence.Entities;

namespace ExchangeOffice.Application.Models
{
    public class CurrencyApiModel
    {
        public CurrencyApiModel(Currency currency, float? value = null)
        {
            Id = currency.Id;
            Name = currency.Name;
            CharCode = currency.CharCode;
            Value = value;
        }
        
        public int? Id { get; set; }
        public string Name { get; set; }
        public string CharCode { get; set; }
        
        public float? Value { get; set; }

        public IEnumerable<ExchangeRateModel> ExchangeRateModels => new List<ExchangeRateModel>();
    }
}