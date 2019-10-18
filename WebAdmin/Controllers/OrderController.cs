using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAdmin.Constants;
using WebAdmin.Extentions;
using WebAdmin.Models;

namespace WebAdmin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public async Task<ActionResult> Index()
        {
            if (TempData["Success"] != null)
            {
                ViewBag.Success = TempData["Success"];
            }
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }
            TokenViewModel _token = HttpContext.Session.Get<TokenViewModel>(Constant.TOKEN);
            if (_token != null)
            {
                using (var client = new HttpClient())
                {
                    // TODO: Add insert logic here
                    client.BaseAddress = new Uri("https://cocshopwebapi20190925023900.azurewebsites.net/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token.Access_token}");

                    HttpResponseMessage response = await client.GetAsync("api/Order/GetAll?InClude=DeliveryUser,CreatedUser,Location");
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var body = JsonConvert.DeserializeObject<BaseViewModel<PagingResult<OrderViewModel>>>(jsonString);
                    if (response.IsSuccessStatusCode)
                    {

                        IndexOrderVewModel RoleIndexViewModel = new IndexOrderVewModel
                        {
                            User = _token,
                            Orders = body.Data.Results.ToList()
                        };
                        return View(RoleIndexViewModel);
                    }
                    else
                    {
                        ViewBag.Error = body.Description;
                        IndexOrderVewModel RoleIndexViewModel = new IndexOrderVewModel
                        {
                            User = _token,
                        };
                        return View(RoleIndexViewModel);
                    }

                }
            }
            return RedirectToAction("Login", "Auth");
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}