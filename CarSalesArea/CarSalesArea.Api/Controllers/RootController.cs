using CarSalesArea.Core.Infrastructure;
using CarSalesArea.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesArea.Api.Controllers
{
    [ApiController]
    [Route("/")]
    [ApiVersion("1.0")]
    public class RootController: ControllerBase
    {
        [HttpGet(Name = nameof(GetRoot))]
        [ProducesResponseType(200)]
        [ProducesResponseType(304)]
        [ResponseCache(CacheProfileName = "Static")]
        [Etag]
        public IActionResult GetRoot()
        {
            var response = new RootResponse()
            {
                Self = Link.To(nameof(GetRoot)),
                Managers = Link.ToCollection(nameof(ManagerController.GetAllManagersAsync)),
                Cars = Link.ToCollection(nameof(CarController.GetAllCarsAsync)),
                Areas = Link.ToCollection(nameof(SalesAreaController.GetAllSalesAreaAsync))
            };

            if (!Request.GetEtagHandler().NoneMatch(response))
            {
                return StatusCode(304, response);
            }

            return Ok(response);
        }
    }
}
