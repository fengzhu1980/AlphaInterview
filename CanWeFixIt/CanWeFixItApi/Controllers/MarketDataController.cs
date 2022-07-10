using System.Collections.Generic;
using System.Threading.Tasks;
using CanWeFixItService;
using CanWeFixItService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CanWeFixItApi.Controllers
{
    [ApiController]
    [Route("v1")]
    public class MarketDataController : ControllerBase
    {
        private readonly MarketDataService _marketDataService;
        public MarketDataController(MarketDataService marketDataService)
        {
            _marketDataService = marketDataService;
        }

        [HttpGet("marketdata")]
        // GET
        public async Task<ActionResult<IEnumerable<MarketDataDto>>> GetMarketdata()
        {
            var result = await _marketDataService.GetAllMarketdataWithInstrumentIdByActiveAsync(true);

            return Ok(result);
        }

        [HttpGet("valuations")]
        // GET
        public async Task<ActionResult<IEnumerable<MarketValuationVM>>> GetValuations()
        {
            var result = await _marketDataService.GetAllValuationByActiveAsync(true);

            return Ok(result);
        }
    }
}