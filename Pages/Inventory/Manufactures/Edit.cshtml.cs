using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmarcySystem.Data;
using PharmarcySystem.Models;

namespace PharmarcySystem.Pages.Manufactures
{
    public class EditModel : PageModel
    {
        private readonly PharmarcySystem.Data.PharmarcySystemContext _context;

        public EditModel(PharmarcySystem.Data.PharmarcySystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Manufacturer Manufacturer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Manufacturer = await _context.Manufacturer.FirstOrDefaultAsync(m => m.Id == id);

            if (Manufacturer == null)
            {
                return NotFound();
            }
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

            _context.Attach(Manufacturer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManufacturerExists(Manufacturer.Id))
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

        private bool ManufacturerExists(int id)
        {
            return _context.Manufacturer.Any(e => e.Id == id);
        }
    }
}
