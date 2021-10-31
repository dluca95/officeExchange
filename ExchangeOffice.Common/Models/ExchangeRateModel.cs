using System;
using ExchangeOffice.Persistence.Entities;

namespace ExchangeOffice.Application.Models
{
    public class ExchangeRateModel
    {
        public ExchangeRateModel()
        {
            
        }

        public ExchangeRateModel(ExchangeRate exchangeRate)
        {
            Id = exchangeRate.Id;
            BuyPrice = exchangeRate.BuyPrice;
            SellPrice = exchangeRate.SellPrice;
            CurrencyCharCode = exchangeRate.Currency.CharCode;
            OnDate = exchangeRate.CreatedAt;
        }
        
        public int? Id { get; set; }
        public float? BuyPrice { get; set; }
        public float? SellPrice { get; set; }
        public string CurrencyCharCode { get; set; }
        
        public DateTime OnDate { get; set; }
    }
}