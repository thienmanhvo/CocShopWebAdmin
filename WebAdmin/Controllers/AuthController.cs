using System;
using System.Collections.Generic;
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
    public class AuthController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            TokenViewModel _token = HttpContext.Session.Get<TokenViewModel>(Constant.TOKEN);
            if (_token != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        // TODO: Add insert logic here
                        client.BaseAddress = new Uri("https://cocshopwebapi20190925023900.azurewebsites.net/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


                        HttpResponseMessage response = await client.PostAsJsonAsync("api/Auth/Login", loginViewModel);
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var body = JsonConvert.DeserializeObject<BaseViewModel<TokenViewModel>>(jsonString);
                        if (response.IsSuccessStatusCode)
                        {
                            
                            if (body.Data.Roles.Any(_ => _.ToUpper().Contains(Role.Admin.ToUpper())))
                            {
                                HttpContext.Session.Set<TokenViewModel>(Constant.TOKEN, body.Data);
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        else
                        {
                            ViewBag.Error = body.Description;
                        }
                    }
                }
                catch
                {

                }
            }
            return View();
        }
    }
}