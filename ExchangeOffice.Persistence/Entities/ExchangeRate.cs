using System;

namespace ExchangeOffice.Persistence.Entities
{
    public class ExchangeRate: EntityModel
    {
        public int CurrencyId { get; set; }
        
        public Currency Currency { get; set; }
        
        public  float BuyPrice { get; set; }
        
        public  float  SellPrice { get; set; }
    }
}