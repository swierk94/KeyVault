using KeyVault.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using Microsoft.Extensions.Configuration;

namespace KeyVault.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _config = config;
            _logger = logger;
        }

        public IActionResult Index(BaseModel model)
        {
            var kvUri = "https://kv-streater-test.vault.azure.net/";

            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

            var secret = client.GetSecretAsync("secretCoulor",
                "456dbc8934ab4f109872ab672bc7f873").Result.Value.Value;

            ViewBag.secretColour = secret;

            ViewBag.secretPerson = _config.GetSection("secretPerson").Value;

            ModelState.Clear();

            return View(model);
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
