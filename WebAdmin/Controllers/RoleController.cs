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
    public class RoleController : Controller
    {
        // GET: Role
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

                    HttpResponseMessage response = await client.GetAsync("api/Role");
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var body = JsonConvert.DeserializeObject<BaseViewModel<PagingResult<RoleViewModel>>>(jsonString);
                    if (response.IsSuccessStatusCode)
                    {

                        IndexRoleVewModel RoleIndexViewModel = new IndexRoleVewModel
                        {
                            User = _token,
                            Roles = body.Data.Results.ToList()
                        };
                        return View(RoleIndexViewModel);
                    }
                    else
                    {
                        ViewBag.Error = body.Description;
                        IndexRoleVewModel RoleIndexViewModel = new IndexRoleVewModel
                        {
                            User = _token,
                        };
                        return View(RoleIndexViewModel);
                    }

                }
            }
            return RedirectToAction("Login", "Auth");
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            TokenViewModel _token = HttpContext.Session.Get<TokenViewModel>(Constant.TOKEN);
            if (_token != null)
            {
                CreateRoleVewModel RoleIndexViewModel = new CreateRoleVewModel
                {
                    User = _token,
                };
                return View(RoleIndexViewModel);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateRoleVewModel RoleViewModel)
        {
            TokenViewModel _token = HttpContext.Session.Get<TokenViewModel>(Constant.TOKEN);

            if (_token != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        using (var client = new HttpClient())
                        {
                            // TODO: Add insert logic here

                            client.BaseAddress = new Uri("https://cocshopwebapi20190925023900.azurewebsites.net/");
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token.Access_token}");
                            HttpResponseMessage response = await client.PostAsJsonAsync($"api/Role", RoleViewModel.Role);
                            var jsonString = await response.Content.ReadAsStringAsync();
                            var body = JsonConvert.DeserializeObject<BaseViewModel<CreateRoleRequestViewModel>>(jsonString);
                            if (response.IsSuccessStatusCode)
                            {
                                TempData["Success"] = "Create Successfully";
                                return RedirectToAction("Index", "Role");
                            }
                            else
                            {
                                RoleViewModel = new CreateRoleVewModel
                                {
                                    User = _token,
                                    Role = RoleViewModel.Role,
                                };
                                ViewBag.Error = body.Description;
                                return View(RoleViewModel);
                            }
                        }
                    }
                    else
                    {
                        CreateRoleVewModel RoleEditViewModel = new CreateRoleVewModel
                        {
                            User = _token,
                            Role = RoleViewModel.Role
                        };
                        return View(RoleEditViewModel);
                    }
                }
                catch
                {
                    CreateRoleVewModel RoleEditViewModel = new CreateRoleVewModel
                    {
                        User = _token,
                        Role = RoleViewModel.Role
                    };
                    return View(RoleEditViewModel);
                }
            }
            return RedirectToAction("Login", "Auth");
        }

        // GET: Role/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Role/Edit/5
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

        // GET: Role/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Role/Delete/5
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