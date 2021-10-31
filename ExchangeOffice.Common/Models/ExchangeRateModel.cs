using System;

namespace ExchangeOffice.Application.Models
{
    public class ExchangeRateModel
    {
        public int? Id { get; set; }
        public float? BuyPrice { get; set; }
        public float? SellPrice { get; set; }
        public string CurrencyCharCode { get; set; }
        
        public DateTime OnDate { get; set; }
    }
}