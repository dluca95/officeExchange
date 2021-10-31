using System.Collections.Generic;

namespace ExchangeOffice.Persistence.Entities
{
    public class Currency: EntityModel
    {
        public  string Name { get; set; }
        
        public string CharCode { get; set; }
        
        public  int Nominal { get; set; }
        
   //     public IEnumerable<int> ExchangeRateIds { get; set; }

        public IEnumerable<ExchangeRate> ExchangeRates { get; set; }
    }
}