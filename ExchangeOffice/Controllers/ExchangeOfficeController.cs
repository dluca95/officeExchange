using System.Threading.Tasks;
using ExchangeOffice.Application;
using ExchangeOffice.Application.Models;
using ExchangeOffice.Persistence.Entities;
using ExchangeOffice.Persistence.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeOffice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeOfficeController: ControllerBase
    {
        private readonly IAppService<ExchangeRateModel> _exchangeRateAppService;
        
        public ExchangeOfficeController( IAppService<ExchangeRateModel> exchangeRateAppService)
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
    }
}