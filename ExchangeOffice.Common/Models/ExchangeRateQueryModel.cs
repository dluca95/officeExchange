using System;

namespace ExchangeOffice.Application.Models
{
    public class ExchangeRateQueryModel
    {
        public string CurrencyCharCode { get; set; }
        
        public DateTime? OnDate { get; set; }
    }
}