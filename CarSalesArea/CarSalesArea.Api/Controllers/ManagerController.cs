using System.Collections;
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
    //[Authorize(Roles = UserRoles.Admin)]
    [Route("/[controller]")]
    [ApiController]
    public class ManagerController: ControllerBase
    {
        private readonly IManagerService _managerService;
        private readonly IMapper _mapper;

        public ManagerController(
            IManagerService managerService,
            IMapper mapper)
        {
            _managerService = managerService;
            _mapper = mapper;
        }

        [HttpGet ("managers", Name = nameof(GetAllManagersAsync))]
        [ProducesResponseType(200)]
        [ResponseCache(CacheProfileName = "Static")]
        [Etag]
        public async Task<ActionResult<Collection<ManagerViewModel>>> GetAllManagersAsync()
        {
            var managers = (await _managerService.GetAllManagersAsync()).ToList();

            var managerViewModels = _mapper.Map<IEnumerable<ManagerViewModel>>(managers);

            var collection = new Collection<ManagerViewModel>()
            {
                Self = Link.ToCollection(nameof(GetAllManagersAsync)),
                Value = managerViewModels.ToArray()
            };

            return Ok(collection);
        }

        [HttpGet("{id}", Name = nameof(GetManagerByIdAsync))]
        [ProducesResponseType(200)]
        [ProducesResponseType(304)]
        [ProducesResponseType(404)]
        [ResponseCache(CacheProfileName = "Static")]
        [Etag]
        public async Task<ActionResult<ManagerViewModel>> GetManagerByIdAsync(long id)
        {
            var manager = await _managerService.GetManagerByIdAsync(id);

            if (manager == null)
            {
                return NotFound();
            }

            manager.Href = Url.Link(nameof(GetManagerByIdAsync), new{id});

            var managerViewModel = _mapper.Map<ManagerViewModel>(manager);

            if (!Request.GetEtagHandler().NoneMatch(managerViewModel))
            {
                return StatusCode(304, managerViewModel);
            }

            return Ok(managerViewModel);
        }

        [HttpPost("{areaId}/manager", Name = nameof(CreateManagerAsync))]
        [ProducesResponseType(201)]
        public async Task<ActionResult> CreateManagerAsync(
            long areaId,
            [FromBody] ManagerViewModel manager)
        {
            var managerModel = _mapper.Map<ManagerModel>(manager);

            var managerId = await _managerService.CreateManagerAsync(managerModel);

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
            [FromBody] ManagerViewModel manager)
        {
            var managerModel = _mapper.Map<ManagerModel>(manager);
            managerModel.Id = managerId;
            await _managerService.UpdateManagerAsync(managerModel);

            return Ok();
        }
    }
}
