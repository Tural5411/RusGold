using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RusGold.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RusGold.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Kontakt")]
        public IActionResult Contact()
        {
            return View();
        }
        [Route("RussianPost")]
        public IActionResult RussianPost()
        {
            return View();
        }
        [Route("RadioPreparation")]
        public IActionResult RadioPreparation()
        {
            return View();
        }
        [Route("SdekRadioShipping")]
        public IActionResult SdekRadioShipping()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
