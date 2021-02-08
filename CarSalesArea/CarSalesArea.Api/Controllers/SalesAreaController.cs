using CarSalesArea.Core.Infrastructure;
using CarSalesArea.Core.Models;
using CarSalesArea.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSalesArea.Api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class SalesAreaController : ControllerBase
    {
        private readonly ISalesAreaService _salesAreaService;

        public SalesAreaController(ISalesAreaService salesAreaService)
        {
            _salesAreaService = salesAreaService;
        }

        [HttpGet("sales-areas", Name = nameof(GetAllSalesAreaAsync))]
        [ProducesResponseType(200)]
        [Etag]
        public async Task<ActionResult<IEnumerable<SalesAreaModel>>> GetAllSalesAreaAsync()
        {
            var result = await _salesAreaService.GetAllSalesAreasAsync();
            return Ok(result);
        }

        [HttpGet("{areaId}", Name = nameof(GetSalesAreaByIdAsync))]
        [ProducesResponseType(200)]
        [ProducesResponseType(304)]
        [ResponseCache(CacheProfileName = "Static")]
        [Etag]
        public async Task<ActionResult<SalesAreaModel>> GetSalesAreaByIdAsync(long areaId)
        {
            var result = await _salesAreaService.GetSalesAreaByIdAsync(areaId);

            if (!Request.GetEtagHandler().NoneMatch(result))
            {
                return StatusCode(304, result);
            }

            return Ok(result);
        }

        [HttpPost("sales-area", Name = nameof(CreateSalesAreaAsync))]
        [ProducesResponseType(201)]
        public async Task<ActionResult> CreateSalesAreaAsync([FromBody] SalesAreaModel areaModel)
        {
            var areaId = await _salesAreaService.CreateSalesAreaAsync(areaModel);

            var link = Url.Link(nameof(GetSalesAreaByIdAsync),
                new {id = areaId});

            return Created(
                link,
                null);
        }

        [HttpPut("{areaId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateSalesAreaAsync(
            long areaId,
            [FromBody]SalesAreaModel areaModel)
        {
            areaModel.Id = areaId;
            await _salesAreaService.UpdateSalesAreaAsync(areaModel);

            return Ok();
        }

        [HttpDelete("{areaId}", Name = nameof(DeleteSalesAreaByIdAsync))]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteSalesAreaByIdAsync(long areaId)
        {
            await _salesAreaService.RemoveSalesAreaAsync(areaId);

            return NoContent();
        }
    }
}
