using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult GetRoot()
        {
            var response = new RootResponse()
            {
                Self = Link.To(nameof(GetRoot)),
                Managers = Link.To(nameof(ManagerController.GetAllManagersAsync))
            };

            return Ok(response);
        }
    }
}
