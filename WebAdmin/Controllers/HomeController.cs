using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAdmin.Constants;
using WebAdmin.Models;
using WebAdmin.Extentions;

namespace WebAdmin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            TokenViewModel _token = HttpContext.Session.Get<TokenViewModel>(Constant.TOKEN);
            if (_token != null)
            {
                return View(_token);
              
            }
            return RedirectToAction("Login", "Auth");
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
