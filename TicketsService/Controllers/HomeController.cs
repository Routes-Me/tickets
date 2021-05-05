using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace TicketsService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Obsolete]
        public readonly IHostingEnvironment _hostingEnv;

        [Obsolete]
        public HomeController(IHostingEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
        }
        [HttpGet]
        [Obsolete]
        public string Get()
        {
            return "Tickets service started successfully. Environment - " + _hostingEnv.EnvironmentName + "";
        }
    }
}
