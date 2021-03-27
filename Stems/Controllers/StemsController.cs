using Microsoft.AspNetCore.Mvc;
using Stems.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Stems.Controllers
{
    [Route("")]
    [Route("{stem?}")]
    [ApiController]
    public class StemsController : ControllerBase
    {
        private readonly ISteamsFactory _steamsFactory;
        public StemsController(ISteamsFactory steamsFactory)
        {
            _steamsFactory = steamsFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetStems([FromQuery] string stem)
        {
            try
            {
                var result = await _steamsFactory.CreateSteams(stem);

                if (result?.Count() == 0)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

    }
}
