using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ski.API.Data;
using Ski.Domain.Entities;

namespace Apolchevskaya.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly Ski.API.Data.AppDbContext _context;

        public CreateModel(Ski.API.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "NormalizedName");
            return Page();
        }

        [BindProperty]
        public Skii Skii { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Skii.Add(Skii);
            await _context.SaveChangesAsync();
           
           

            return RedirectToPage("./Index");
        }
    }
}
