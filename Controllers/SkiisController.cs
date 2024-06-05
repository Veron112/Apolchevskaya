using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Apolchevskaya.Data;
using Ski.Domain.Entities;
using Apolchevskaya.Services;
using Apolchevskaya.Dto;

namespace Apolchevskaya.Controllers
{
    public class SkiisController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IProductService _productService;
        private ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;
        public SkiisController(
            IWebHostEnvironment env,
            ApplicationDbContext context, 
            IProductService productService, 
            ICategoryService categoryService)
        {
            _env = env;
            _context = context;
            _productService = productService;
            _categoryService = categoryService;
        }



        // GET: Skiis
        public async Task<IActionResult> Index(string? category, int pageNo = 1)
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
            var currentCategory = category == null ? "Все"
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

        // GET: Skiis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skii = await _context.Skii
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.SkiId == id);
            if (skii == null)
            {
                return NotFound();
            }

            return View(skii);
        }

        // GET: Skiis/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "NormalizedName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] PostSkiiDto skii)
        {
            if (ModelState.IsValid)
            {
                Skii skiiCreate = new()
                {
                    SkiName = skii.SkiName,
                    Description = skii.Description,
                    Price = skii.Price,
                    CategoryId = skii.CategoryId,
                };
                var imagesPath = Path.Combine(_env.WebRootPath, "Images");
                var randomName = Path.GetRandomFileName();
                var extension = Path.GetExtension(skii.Image.FileName);
                var fileName = Path.ChangeExtension(randomName, extension);
                var filePath = Path.Combine(imagesPath, fileName);
                using var stream = System.IO.File.OpenWrite(filePath);
                await skii.Image.CopyToAsync(stream);
                var host = "https://" + Request.Host;
                var url = $"{host}/Images/{fileName}";
                skiiCreate.Image = url;
                await _context.SaveChangesAsync();

                _context.Add(skiiCreate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "NormalizedName", skii.CategoryId);
            return View(skii);
        }

        // GET: Skiis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skii = await _context.Skii.FindAsync(id);
            if (skii == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "NormalizedName", skii.CategoryId);
            return View(skii);
        }

        // POST: Skiis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SkiId,SkiName,Description,Image,Price,CategoryId")] Skii skii)
        {
            if (id != skii.SkiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skii);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkiiExists(skii.SkiId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "NormalizedName", skii.CategoryId);
            return View(skii);
        }

        // GET: Skiis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skii = await _context.Skii
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.SkiId == id);
            if (skii == null)
            {
                return NotFound();
            }

            return View(skii);
        }

        // POST: Skiis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skii = await _context.Skii.FindAsync(id);
            if (skii != null)
            {
                _context.Skii.Remove(skii);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkiiExists(int id)
        {
            return _context.Skii.Any(e => e.SkiId == id);
        }
    }
}
