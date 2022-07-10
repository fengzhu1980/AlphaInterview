using System.Collections.Generic;
using System.Threading.Tasks;
using CanWeFixItService;
using CanWeFixItService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CanWeFixItApi.Controllers
{
    [ApiController]
    [Route("v1/instruments")]
    public class InstrumentController : ControllerBase
    {
        private readonly InstrumentService _instrumentService;
        
        public InstrumentController(InstrumentService instrumentService)
        {
            _instrumentService = instrumentService;
        }
        
        // GET
        public async Task<ActionResult<IEnumerable<InstrumentVM>>> Get()
        {
            // Get filter from front end
            // "SELECT id, active FROM instrument WHERE active = 1 and id IN (2, 4, 6, 8)";
            var filter = new GetInstrumentFilter()
            {
                Active = true,
                Ids = new List<int> { 2, 4, 6, 8 }
            };
            var instrumentsInDB = await _instrumentService.GetAllInstrumentsByFilterAsync(filter);
            return Ok(instrumentsInDB);
        }
    }
}