using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PharmarcySystem.Data;
using PharmarcySystem.Models;

namespace PharmarcySystem.Pages.Inventory.GenericData
{
    public class DeleteModel : PageModel
    {
        private readonly PharmarcySystem.Data.PharmarcySystemContext _context;

        public DeleteModel(PharmarcySystem.Data.PharmarcySystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Generic Generic { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Generic = await _context.Generic.FirstOrDefaultAsync(m => m.Id == id);

            if (Generic == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Generic = await _context.Generic.FindAsync(id);

            if (Generic != null)
            {
                _context.Generic.Remove(Generic);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
