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
    public class ProductController : Controller
    {
        // GET: Product
        public async Task<ActionResult> Index()
        {
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

                    HttpResponseMessage response = await client.GetAsync("api/Products/GetAll");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var body = JsonConvert.DeserializeObject<BaseViewModel<ProductViewModel>>(jsonString);
                        ProductIndexViewModel productIndexViewModel = new ProductIndexViewModel
                        {
                            User = _token,
                            Product = body.Data
                        };
                        return View(productIndexViewModel);
                    }
                    else
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        throw new Exception(jsonString.ToString());
                    }

                }
            }
            return RedirectToAction("Login", "Auth");
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
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

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
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

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
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