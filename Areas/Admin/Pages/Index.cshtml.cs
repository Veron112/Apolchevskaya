﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apolchevskaya.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ski.API.Data;
using Ski.Domain.Entities;

namespace Apolchevskaya.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        public IndexModel(IProductService productService)
        {
            //_context = context;
            _productService = productService;
        }

        public IList<Skii> Skii { get; set; } = default!;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;

        public async Task OnGetAsync(int? pageNo = 1)
        {
            var response = await _productService.GetProductListAsync(null, pageNo.Value);
            if (response.Success)
            {
                Skii = response.Data.Items;
                CurrentPage = response.Data.CurrentPage;
                TotalPages = response.Data.TotalPages;
              

            }
        }
    }
}
