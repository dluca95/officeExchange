using System;

namespace ExchangeOffice.Persistence.Entities
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        
        public int CurrencyId { get; set; }
        
        public Currency Currency { get; set; }
        
        public  float BuyPrice { get; set; }
        
        public  float  SellPrice { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}