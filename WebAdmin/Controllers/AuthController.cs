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
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAdmin.Enum;

namespace WebAdmin.Controllers
{
    public class AuthController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            if (TempData["Success"] != null)
            {
                ViewBag.Success = TempData["Success"];
            }
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
                            else
                            {
                                ViewBag.Error = "You don't have permission to access this website.";
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
        public async Task<ActionResult> Profile()
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

                    HttpResponseMessage response = await client.GetAsync("api/Auth/Profile");

                    var jsonString = await response.Content.ReadAsStringAsync();
                    var body = JsonConvert.DeserializeObject<BaseViewModel<UserViewModel>>(jsonString);
                    body.Data.Genders = GetAllGender();
                    if (response.IsSuccessStatusCode)
                    {
                        return View(body.Data);
                    }
                    else
                    {
                        ViewBag.Error = body.Description;
                        return View(body.Data);
                    }
                }
            }
            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        public async Task<ActionResult> Profile(UserViewModel user)
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
                    UpdateUserRequestViewModel updateUserRequestViewModel = new UpdateUserRequestViewModel()
                    {
                        AvatarPath = user.AvatarPath,
                        FullName = user.FullName,
                        Gender = user.Gender,
                        Email = user.Email,
                        Birthday = user.Birthday.Value.ToString("yyyyMMdd")
                    };
                    HttpResponseMessage response = await client.PutAsJsonAsync("api/Auth/UpdateInfo", updateUserRequestViewModel);

                    var jsonString = await response.Content.ReadAsStringAsync();
                    var body = JsonConvert.DeserializeObject<BaseViewModel<UserViewModel>>(jsonString);
                    user.Genders = GetAllGender();
                    if (response.IsSuccessStatusCode)
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token.Access_token}");
                        response = await client.GetAsync("api/Auth/GetToken");
                        jsonString = await response.Content.ReadAsStringAsync();
                        var token = JsonConvert.DeserializeObject<BaseViewModel<TokenViewModel>>(jsonString);
                        if (response.IsSuccessStatusCode)
                        {

                            if (token.Data.Roles.Any(_ => _.ToUpper().Contains(Role.Admin.ToUpper())))
                            {
                                HttpContext.Session.Set<TokenViewModel>(Constant.TOKEN, token.Data);
                            }
                            else
                            {
                                HttpContext.Session.Clear();
                                return RedirectToAction("Login", "Auth");
                            }
                        }
                        else
                        {
                            ViewBag.Error = token.Description;
                            return View(user);
                        }
                        ViewBag.Success = "Update profile successfully";
                        return View(user);
                    }
                    else
                    {
                        ViewBag.Error = body.Description;
                        return View(user);
                    }
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        private List<SelectListItem> GetAllGender()
        {
            var result = new List<SelectListItem>();
            var enumerationType = typeof(MyEnum.Gender);
            foreach (int value in System.Enum.GetValues(enumerationType))
            {
                var name = System.Enum.GetName(enumerationType, value);
                var selectItem = new SelectListItem()
                {
                    Value = name,
                    Text = name
                };
                result.Add(selectItem);
            }
            return result;
        }
        public ActionResult ChangePassword()
        {
            TokenViewModel _token = HttpContext.Session.Get<TokenViewModel>(Constant.TOKEN);
            if (_token != null)
            {
                ChangePasswordViewModel changePasswordViewModel = new ChangePasswordViewModel()
                {
                    User = _token,
                };

                return View(changePasswordViewModel);
            }
            return RedirectToAction("Login", "Auth");
        }
        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
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
                    HttpResponseMessage response = await client.PostAsJsonAsync("api/Auth/ChangePassword", changePasswordViewModel);
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var body = JsonConvert.DeserializeObject<BaseViewModel<ChangePasswordViewModel>>(jsonString);
                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.Clear();
                        TempData["Success"]  = "Change password successfully. You must relogin to continute.";
                        return RedirectToAction("Login", "Auth");
                    }
                    else
                    {
                        ViewBag.Error = body.Description;
                    }
                }
                return View(changePasswordViewModel);
            }
            return RedirectToAction("Login", "Auth");
        }

        public ActionResult Logout(ChangePasswordViewModel changePasswordViewModel)
        {
            TokenViewModel _token = HttpContext.Session.Get<TokenViewModel>(Constant.TOKEN);
            if (_token != null)
            {
                HttpContext.Session.Clear();
            }
            return RedirectToAction("Login", "Auth");
        }
    }
}