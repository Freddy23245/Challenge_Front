using ChallengeFront.Interfaces;
using ChallengeFront.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChallengeFront.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClienteService _service;
        public HomeController(ILogger<HomeController> logger, IClienteService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            var result = _service.GetAll();
            int i = 0;
            return View();
        }

        public IActionResult Privacy()
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
