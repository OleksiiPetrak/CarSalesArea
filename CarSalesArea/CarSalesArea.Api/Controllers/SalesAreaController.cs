using CarSalesArea.Core.Infrastructure;
using CarSalesArea.Core.Models;
using CarSalesArea.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarSalesArea.Api.ViewModels;

namespace CarSalesArea.Api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class SalesAreaController : ControllerBase
    {
        private readonly ISalesAreaService _salesAreaService;
        private readonly IMapper _mapper;

        public SalesAreaController(ISalesAreaService salesAreaService, IMapper mapper)
        {
            _salesAreaService = salesAreaService;
            _mapper = mapper;
        }

        [HttpGet("sales-areas", Name = nameof(GetAllSalesAreaAsync))]
        [ProducesResponseType(200)]
        [Etag]
        public async Task<ActionResult<Collection<SalesAreaViewModel>>> GetAllSalesAreaAsync()
        {
            var result = await _salesAreaService.GetAllSalesAreasAsync();
            var areaViewModels = _mapper.Map<IEnumerable<SalesAreaViewModel>>(result);

            var collection = new Collection<SalesAreaViewModel>()
            {
                Self = Link.ToCollection(nameof(GetAllSalesAreaAsync)),
                Value = areaViewModels.ToArray()
            };

            return Ok(collection);
        }

        [HttpGet("{areaId}", Name = nameof(GetSalesAreaByIdAsync))]
        [ProducesResponseType(200)]
        [ProducesResponseType(304)]
        [ResponseCache(CacheProfileName = "Static")]
        [Etag]
        public async Task<ActionResult<SalesAreaViewModel>> GetSalesAreaByIdAsync(long areaId)
        {
            var result = await _salesAreaService.GetSalesAreaByIdAsync(areaId);
            
            var areaViewModel = _mapper.Map<SalesAreaViewModel>(result);

            if (!Request.GetEtagHandler().NoneMatch(areaViewModel))
            {
                return StatusCode(304, areaViewModel);
            }

            return Ok(areaViewModel);
        }

        [HttpPost("sales-area", Name = nameof(CreateSalesAreaAsync))]
        [ProducesResponseType(201)]
        public async Task<ActionResult> CreateSalesAreaAsync([FromBody] SalesAreaViewModel area)
        {
            var areaModel = _mapper.Map<SalesAreaModel>(area);
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
            [FromBody]SalesAreaViewModel area)
        {
            var areaModel = _mapper.Map<SalesAreaModel>(area);
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
