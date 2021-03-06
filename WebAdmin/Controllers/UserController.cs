﻿using System;
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
    public class UserController : Controller
    {
        // GET: User
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

                    HttpResponseMessage response = await client.GetAsync("api/MyUsers");
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var body = JsonConvert.DeserializeObject<BaseViewModel<PagingResult<UserViewModel>>>(jsonString);
                    if (response.IsSuccessStatusCode)
                    {

                        IndexUserVewModel RoleIndexViewModel = new IndexUserVewModel
                        {
                            User = _token,
                            Users = body.Data.Results.ToList()
                        };
                        return View(RoleIndexViewModel);
                    }
                    else
                    {
                        ViewBag.Error = body.Description;
                        IndexUserVewModel RoleIndexViewModel = new IndexUserVewModel
                        {
                            User = _token,
                        };
                        return View(RoleIndexViewModel);
                    }

                }
            }
            return RedirectToAction("Login", "Auth");
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
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

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5

    }
}