﻿using Apolchevskaya.Services;
using Microsoft.AspNetCore.Mvc;

namespace Apolchevskaya.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;

        }
        [Route("Catalog")]
        [Route("Catalog/{category}")]

        public async Task<IActionResult> Index(string? category, int pageNo=1)
            {
                // получить список категорий
                var categoriesResponse = await
                _categoryService.GetCategoryListAsync();

                // если список не получен, вернуть код 404
                if (!categoriesResponse.Success)
                    return NotFound(categoriesResponse.ErrorMessage);

                // передать список категорий во ViewData
                ViewData["categories"] = categoriesResponse.Data;

                // передать во ViewData имя текущей категории
                var currentCategory = category == null? "Все"
                : categoriesResponse.Data.FirstOrDefault(c =>
                c.NormalizedName == category)?.SkiGroupName;
                ViewData["currentCategory"] = currentCategory;
                var productResponse =
                await
                _productService.GetProductListAsync(category, pageNo);
                if (!productResponse.Success)
                    ViewData["Error"] = productResponse.ErrorMessage;
                return View(productResponse.Data);
            }
        }

    }
