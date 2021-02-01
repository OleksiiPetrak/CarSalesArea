using CarSalesArea.Core.Services.Interfaces;
using CarSalesArea.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSalesArea.Api.Controllers
{
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
        public async Task<ActionResult<IEnumerable<Manager>>> GetAllManagersAsync()
        {
            var managers = (await _managerService.GetAllManagersAsync()).ToList();

            return managers;
        }
    }
}
