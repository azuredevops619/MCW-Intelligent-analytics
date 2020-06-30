using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ChatWebApp31.Controllers
{
    public class SearchController : Controller
    {
        private readonly IConfiguration configuration;

        public SearchController(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.ChatSearchApiBase = configuration["AppSetting.ChatSearchApiBase"];
            ViewBag.ChatSearchApiIndexName = configuration["AppSettings:ChatSearchApiIndexName"];
            ViewBag.ChatSearchApiKey = configuration["AppSettings:ChatSearchApiKey"];
            return View();
        }
    }
}