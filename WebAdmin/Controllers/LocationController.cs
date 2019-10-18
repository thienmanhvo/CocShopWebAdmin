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
    public class LocationController : Controller
    {
        // GET: Location
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

                    HttpResponseMessage response = await client.GetAsync("api/Locations/GetAll");
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var body = JsonConvert.DeserializeObject<BaseViewModel<PagingResult<LocationViewModel>>>(jsonString);
                    if (response.IsSuccessStatusCode)
                    {

                        IndexLocationVewModel locationIndexViewModel = new IndexLocationVewModel
                        {
                            User = _token,
                            Locations = body.Data.Results.ToList()
                        };
                        return View(locationIndexViewModel);
                    }
                    else
                    {
                        ViewBag.Error = body.Description;
                        IndexLocationVewModel locationIndexViewModel = new IndexLocationVewModel
                        {
                            User = _token,
                        };
                        return View(locationIndexViewModel);
                    }

                }
            }
            return RedirectToAction("Login", "Auth");
        }

        // GET: Location/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Location/Create
        public ActionResult Create()
        {
            TokenViewModel _token = HttpContext.Session.Get<TokenViewModel>(Constant.TOKEN);
            if (_token != null)
            {
                CreateLocationVewModel locationIndexViewModel = new CreateLocationVewModel
                {
                    User = _token,
                };
                return View(locationIndexViewModel);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        // POST: Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLocationVewModel locationViewModel)
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
                            HttpResponseMessage response = await client.PostAsJsonAsync($"api/Locations", locationViewModel.Location);
                            var jsonString = await response.Content.ReadAsStringAsync();
                            var body = JsonConvert.DeserializeObject<BaseViewModel<UpdateLocationViewModel>>(jsonString);
                            if (response.IsSuccessStatusCode)
                            {
                                TempData["Success"] = "Create Successfully";
                                return RedirectToAction("Index", "Location");
                            }
                            else
                            {
                                locationViewModel = new CreateLocationVewModel
                                {
                                    User = _token,
                                    Location = locationViewModel.Location,
                                };
                                ViewBag.Error = body.Description;
                                return View(locationViewModel);
                            }
                        }
                    }
                    else
                    {
                        CreateLocationVewModel locationEditViewModel = new CreateLocationVewModel
                        {
                            User = _token,
                            Location = locationViewModel.Location
                        };
                        return View(locationEditViewModel);
                    }
                }
                catch
                {
                    CreateLocationVewModel locationEditViewModel = new CreateLocationVewModel
                    {
                        User = _token,
                        Location = locationViewModel.Location
                    };
                    return View(locationEditViewModel);
                }
            }
            return RedirectToAction("Login", "Auth");
        }

        // GET: Location/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            TokenViewModel _token = HttpContext.Session.Get<TokenViewModel>(Constant.TOKEN);
            if (_token != null)
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    using (var client = new HttpClient())
                    {
                        // TODO: Add insert logic here
                        client.BaseAddress = new Uri("https://cocshopwebapi20190925023900.azurewebsites.net/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token.Access_token}");

                        HttpResponseMessage response = await client.GetAsync($"api/Locations/{id}");
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var body = JsonConvert.DeserializeObject<BaseViewModel<UpdateLocationViewModel>>(jsonString);
                        EditLocationVewModel locationEditViewModel = new EditLocationVewModel
                        {
                            User = _token,
                            Location = body.Data
                        };

                        if (!response.IsSuccessStatusCode)
                        {
                            TempData["Error"] = body.Description;
                            RedirectToAction("Index", "Location");
                        }

                        return View(locationEditViewModel);
                    }

                }
            }
            return RedirectToAction("Login", "Auth");
        }

        // POST: Location/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, EditLocationVewModel locationViewModel)
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
                            EditLocationVewModel locationEditViewModel = null;

                            client.BaseAddress = new Uri("https://cocshopwebapi20190925023900.azurewebsites.net/");
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token.Access_token}");
                            HttpResponseMessage response = await client.PutAsJsonAsync($"api/Locations/{id}", locationViewModel.Location);
                            var jsonString = await response.Content.ReadAsStringAsync();
                            var body = JsonConvert.DeserializeObject<BaseViewModel<UpdateLocationViewModel>>(jsonString);
                            if (response.IsSuccessStatusCode)
                            {
                                TempData["Success"] = "Update Successfully";
                                return RedirectToAction("Index", "Location");
                            }
                            else
                            {
                                locationEditViewModel = new EditLocationVewModel
                                {
                                    User = _token,
                                    Location = locationViewModel.Location,
                                };
                                ViewBag.Error = body.Description;
                                return View(locationEditViewModel);
                            }
                        }
                    }
                    else
                    {
                        EditLocationVewModel locationEditViewModel = new EditLocationVewModel
                        {
                            User = _token,
                            Location = locationViewModel.Location,
                        };
                        return View(locationEditViewModel);
                    }
                }
                catch
                {
                    EditLocationVewModel locationEditViewModel = new EditLocationVewModel
                    {
                        User = _token,
                        Location = locationViewModel.Location,
                    };
                    return View(locationEditViewModel);
                }
            }
            return RedirectToAction("Login", "Auth");
        }

        // GET: Location/Delete/5
        public async Task<ActionResult> Delete(string id)
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

                    HttpResponseMessage response = await client.DeleteAsync($"api/Locations/{id}");
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var body = JsonConvert.DeserializeObject<BaseViewModel<string>>(jsonString);
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { status = true });
                    }
                    else
                    {
                        return Json(new { status = false, error = body.Description });
                    }

                }
            }
            return RedirectToAction("Login", "Auth");
        }
    }
}