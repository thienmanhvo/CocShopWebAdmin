using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

                    HttpResponseMessage response = await client.GetAsync("api/Products/GetAll?Include=Category");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var body = JsonConvert.DeserializeObject<BaseViewModel<PagingResult<ProductViewModel>>>(jsonString);
                        ProductIndexViewModel productIndexViewModel = new ProductIndexViewModel
                        {
                            User = _token,
                            Products = body.Data.Results.ToList()
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


        // GET: Product/Create
        public async Task<ActionResult> Create()
        {
            TokenViewModel _token = HttpContext.Session.Get<TokenViewModel>(Constant.TOKEN);
            if (_token != null)
            {
                ProductCreateViewModel productIndexViewModel = new ProductCreateViewModel
                {
                    User = _token,
                    Categories = await GetAllCate(_token),
                    Product = new UpdateProductViewModel()
                    {
                        ImagePath = Constant.DEFAUT_IMAGE_PATH,
                    }
                };
                return View(productIndexViewModel);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductCreateViewModel productViewModel)
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
                            ProductEditViewModel productEditViewModel = null;
                            if (productViewModel.Product.IsSale ?? false)
                            {
                                if (productViewModel.Product.PriceSale == 0)
                                {
                                    ViewBag.Error = "Price Sale is required when IsSale is True";
                                    productEditViewModel = new ProductCreateViewModel
                                    {
                                        User = _token,
                                        Product = productViewModel.Product,
                                        Categories = await GetAllCate(_token),
                                    };
                                    return View(productEditViewModel);
                                }
                            }
                            else
                            {
                                productViewModel.Product.PriceSale = null;
                            }

                            client.BaseAddress = new Uri("https://cocshopwebapi20190925023900.azurewebsites.net/");
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token.Access_token}");
                            HttpResponseMessage response = await client.PostAsJsonAsync($"api/Products", productViewModel.Product);
                            var jsonString = await response.Content.ReadAsStringAsync();
                            var body = JsonConvert.DeserializeObject<BaseViewModel<UpdateProductViewModel>>(jsonString);
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction("Index", "Product");
                            }
                            else
                            {
                                productEditViewModel = new ProductEditViewModel
                                {
                                    User = _token,
                                    Product = productViewModel.Product,
                                    Categories = await GetAllCate(_token),
                                };
                                ViewBag.Error = body.Description;
                                return View(productEditViewModel);
                            }
                        }
                    }
                    else
                    {
                        ProductEditViewModel productEditViewModel = new ProductEditViewModel
                        {
                            User = _token,
                            Product = productViewModel.Product,
                            Categories = await GetAllCate(_token),
                        };
                        return View(productEditViewModel);
                    }
                }
                catch
                {
                    return View();
                }
            }
            return RedirectToAction("Login", "Auth");
        }
        private async Task<List<SelectListItem>> GetAllCate(TokenViewModel _token)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://cocshopwebapi20190925023900.azurewebsites.net/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token.Access_token}");

                var response = await client.GetAsync($"api/ProductCategories");

                var jsonString = await response.Content.ReadAsStringAsync();
                var cate = JsonConvert.DeserializeObject<BaseViewModel<PagingResult<ProductCategoryViewModel>>>(jsonString);
                if (response.IsSuccessStatusCode)
                {
                    List<SelectListItem> category = new List<SelectListItem>();
                    var listCate = cate.Data.Results.ToList();
                    foreach (var item in listCate)
                    {
                        var selectItem = new SelectListItem()
                        {
                            Value = item.Id,
                            Text = item.Name
                        };
                        category.Add(selectItem);
                    }
                    return category;
                }
                else
                {
                    ViewBag.Error = cate.Description;
                }
            }
            return null;
        }
        // GET: Product/Edit/5
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

                        HttpResponseMessage response = await client.GetAsync($"api/Products/{id}?Include=Category");
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var body = JsonConvert.DeserializeObject<BaseViewModel<UpdateProductViewModel>>(jsonString);
                        ProductEditViewModel productEditViewModel = new ProductEditViewModel
                        {
                            User = _token,
                            Product = body.Data
                        };

                        productEditViewModel.Categories = await GetAllCate(_token);
                        if (!response.IsSuccessStatusCode)
                        {
                            ViewBag.Error = body.Description;
                            RedirectToAction("Index", "Product");
                        }

                        return View(productEditViewModel);
                    }

                }
            }
            return RedirectToAction("Login", "Auth");
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, ProductEditViewModel productViewModel)
        {
            TokenViewModel _token = HttpContext.Session.Get<TokenViewModel>(Constant.TOKEN);
            var a = TempData["Category"];
            if (_token != null)
            {
                try
                {

                    if (ModelState.IsValid)
                    {
                        using (var client = new HttpClient())
                        {
                            // TODO: Add insert logic here
                            ProductEditViewModel productEditViewModel = null;
                            if (productViewModel.Product.IsSale ?? false)
                            {
                                if (productViewModel.Product.PriceSale == 0)
                                {
                                    ViewBag.Error = "Price Sale is required when IsSale is True";
                                    productEditViewModel = new ProductEditViewModel
                                    {
                                        User = _token,
                                        Product = productViewModel.Product,
                                        Categories = await GetAllCate(_token),
                                    };
                                    return View(productEditViewModel);
                                }
                            }
                            else
                            {
                                productViewModel.Product.PriceSale = null;
                            }

                            client.BaseAddress = new Uri("https://cocshopwebapi20190925023900.azurewebsites.net/");
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token.Access_token}");
                            HttpResponseMessage response = await client.PutAsJsonAsync($"api/Products/{id}", productViewModel.Product);
                            var jsonString = await response.Content.ReadAsStringAsync();
                            var body = JsonConvert.DeserializeObject<BaseViewModel<UpdateProductViewModel>>(jsonString);
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction("Index", "Product");
                            }
                            else
                            {

                                productEditViewModel = new ProductEditViewModel
                                {
                                    User = _token,
                                    Product = productViewModel.Product,
                                    Categories = await GetAllCate(_token),
                                };
                                ViewBag.Error = body.Description;
                                return View(productEditViewModel);
                            }
                        }
                    }
                    else
                    {
                        ProductEditViewModel productEditViewModel = new ProductEditViewModel
                        {
                            User = _token,
                            Product = productViewModel.Product,
                            Categories = await GetAllCate(_token),
                        };
                        return View(productEditViewModel);
                    }
                }
                catch
                {
                    return View();
                }
            }
            return RedirectToAction("Login", "Auth");
        }

        // GET: Product/Delete/5
        [HttpDelete]
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

                    HttpResponseMessage response = await client.DeleteAsync($"api/Products/{id}");
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