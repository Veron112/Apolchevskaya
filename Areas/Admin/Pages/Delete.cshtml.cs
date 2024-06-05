using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ski.API.Data;
using Ski.Domain.Entities;

namespace Apolchevskaya.Areas.Admin.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly Ski.API.Data.AppDbContext _context;

        public DeleteModel(Ski.API.Data.AppDbContext context)
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

            var skii = await _context.Skii.FirstOrDefaultAsync(m => m.SkiId == id);

            if (skii == null)
            {
                return NotFound();
            }
            else
            {
                Skii = skii;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skii = await _context.Skii.FindAsync(id);
            if (skii != null)
            {
                Skii = skii;
                _context.Skii.Remove(Skii);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
