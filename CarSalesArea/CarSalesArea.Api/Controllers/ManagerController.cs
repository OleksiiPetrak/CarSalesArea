using CarSalesArea.Core.Infrastructure;
using CarSalesArea.Core.Models;
using CarSalesArea.Core.Services.Interfaces;
using CarSalesArea.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSalesArea.Api.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("/[controller]")]
    [ApiController]
    public class ManagerController: ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpGet ("managers", Name = nameof(GetAllManagersAsync))]
        [ProducesResponseType(200)]
        [ResponseCache(CacheProfileName = "Static")]
        [Etag]
        public async Task<ActionResult<IEnumerable<ManagerModel>>> GetAllManagersAsync()
        {
            var managers = (await _managerService.GetAllManagersAsync()).ToList();

            return Ok(managers);
        }

        [HttpGet("{id}", Name = nameof(GetManagerByIdAsync))]
        [ProducesResponseType(200)]
        [ProducesResponseType(304)]
        [ResponseCache(CacheProfileName = "Static")]
        [Etag]
        public async Task<ActionResult<ManagerModel>> GetManagerByIdAsync(long id)
        {
            var manager = await _managerService.GetManagerByIdAsync(id);

            if (!Request.GetEtagHandler().NoneMatch(manager))
            {
                return StatusCode(304, manager);
            }

            return Ok(manager);
        }

        [HttpPost("{areaId}/manager", Name = nameof(CreateManagerAsync))]
        [ProducesResponseType(201)]
        public async Task<ActionResult> CreateManagerAsync(
            long areaId,
            [FromBody] ManagerModel manager)
        {
            var managerId = await _managerService.CreateManagerAsync(manager);

            var link = Url.Link(nameof(GetManagerByIdAsync),
                new {id = managerId});

            return Created(
                link,
                null);
        }

        [HttpDelete("{managerId}", Name = nameof(DeleteManagerAsync))]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteManagerAsync(long managerId)
        {
            await _managerService.RemoveManagerAsync(managerId);

            return NoContent();
        }

        [HttpPut("{managerId}", Name = nameof(UpdateManagerAsync))]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateManagerAsync(
            long managerId,
            [FromBody] ManagerModel manager)
        {
            manager.Id = managerId;
            await _managerService.UpdateManagerAsync(manager);

            return Ok();
        }
    }
}
