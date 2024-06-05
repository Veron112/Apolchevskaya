using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ski.API.Data;
using Ski.Domain.Entities;

namespace Apolchevskaya.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly Ski.API.Data.AppDbContext _context;

        public EditModel(Ski.API.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Skii Skii { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skii =  await _context.Skii.FirstOrDefaultAsync(m => m.SkiId == id);
            if (skii == null)
            {
                return NotFound();
            }
            Skii = skii;
           ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "NormalizedName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Skii).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkiiExists(Skii.SkiId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SkiiExists(int id)
        {
            return _context.Skii.Any(e => e.SkiId == id);
        }
    }
}
