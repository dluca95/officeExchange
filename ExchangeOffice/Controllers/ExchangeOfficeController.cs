using System.Collections.Generic;
using System.Threading.Tasks;
using ExchangeOffice.Application;
using ExchangeOffice.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeOffice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeOfficeController: ControllerBase
    {
        private readonly IAppService<ExchangeRateModel, ExchangeRateQueryModel> _exchangeRateAppService;
        
        public ExchangeOfficeController(IAppService<ExchangeRateModel, ExchangeRateQueryModel> exchangeRateAppService)
        {
            _exchangeRateAppService = exchangeRateAppService;
        }

        [HttpPost]
        [Route("exchange-rate")]
        public async Task<ExchangeRateModel> AddExchangeRate([FromBody] ExchangeRateModel model)
        {
            return await _exchangeRateAppService.Add(model);
        }
        
        [HttpPatch]
        [Route("exchange-rate")]
        public async Task UpdateExchangeRate([FromBody] ExchangeRateModel model)
        {
            await _exchangeRateAppService.Update(model);
        }

        [HttpGet]
        [Route("exchange-rate")]
        public async Task<ExchangeRateModel> GetExchangeRateByDateAndCurrency([FromQuery] ExchangeRateQueryModel model)
        {
            var result = await _exchangeRateAppService.GetByQuery(model);
            
            return result;
        }

        [HttpGet]
        [Route("exchange-rates")]
        public async Task<IEnumerable<ExchangeRateModel>> GetExchangeRateByDate([FromQuery] ExchangeRateQueryModel model)
        {
            return await _exchangeRateAppService.GetManyByQuery(model);
        }
        
    }
}