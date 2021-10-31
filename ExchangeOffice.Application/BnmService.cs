using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Extensions.Caching.Memory;

namespace ExchangeOffice.Application
{
    public class BnmService
    {
        private HttpClient _httpClient { get; set; }
        private readonly IMemoryCache _cache;
        private const string bnmUrl = "https://www.bnm.md/en/official_exchange_rates?date=";

        public BnmService(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<IEnumerable<Valute>> GetCurrenciesForDate(DateTime? date)
        {
            date ??= DateTime.Now;
            var key = $"{date.Value.Day}.{date.Value.Month}.{date.Value.Year}";

            if (_cache.TryGetValue(key, out List<Valute> cachedCurrency)) return cachedCurrency;
            
            var url = $"{bnmUrl}{date.Value.Day}.{date.Value.Month}.{date.Value.Year}";
            var result = await _httpClient.GetAsync(url);
            var resBody = await result.Content.ReadAsStringAsync();
            XmlRootAttribute xRoot = new XmlRootAttribute
            {
                ElementName = "ValCurs",
                IsNullable = false
            };
            
            var serializer = new XmlSerializer(typeof(List<Valute>), xRoot);
    
            using (TextReader reader = new StringReader(resBody))
            {
                cachedCurrency = (List<Valute>)serializer.Deserialize(reader);
            }
                
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                // Keep in cache for this time, reset time if accessed.
                .SetSlidingExpiration(TimeSpan.FromSeconds(3));

            // Save data in cache.
            _cache.Set($"{DateTime.Now.Day}.${DateTime.Now.Month}.${DateTime.Now.Year}",
                cachedCurrency, 
                cacheEntryOptions);

            return cachedCurrency;
        }
        
        public async Task<Valute> GetCurrencyByCode(string charCode, DateTime? date = null)
        {
            var currencyResult = await GetCurrenciesForDate(date);
            
            return currencyResult.FirstOrDefault(r => r.CharCode == charCode);
        }

    }
    
    public class Valute
    {
        public string NumCode { get; set; }
        
        public float Value { get; set; }
        
        public int Nominal { get; set; }
        
        public string Name { get; set; }
        
        public string CharCode { get; set; }
    }
}