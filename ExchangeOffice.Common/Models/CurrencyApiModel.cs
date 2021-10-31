using System.Collections.Generic;

namespace ExchangeOffice.Application.Models
{
    public class CurrencyApiModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string CharCode { get; set; }
        
        public float? Value { get; set; }

        public IEnumerable<ExchangeRateModel> ExchangeRateModels => new List<ExchangeRateModel>();
    }
}