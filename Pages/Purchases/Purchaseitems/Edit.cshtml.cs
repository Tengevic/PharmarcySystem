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

namespace PharmarcySystem.Pages.Purchaseitems
{
    public class EditModel : PageModel
    {
        private readonly PharmarcySystem.Data.PharmarcySystemContext _context;

        public EditModel(PharmarcySystem.Data.PharmarcySystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PurchaseItem PurchaseItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PurchaseItem = await _context.PurchaseItem
                .Include(p => p.medicineInfo)
                .Include(p => p.purchaseEntryVM).FirstOrDefaultAsync(m => m.ID == id);

            if (PurchaseItem == null)
            {
                return NotFound();
            }
           ViewData["MedicineInfoID"] = new SelectList(_context.MedicineInfo, "Id", "Id");
           ViewData["PurchaseEntryVMID"] = new SelectList(_context.PurchaseEntryVM, "ID", "ID");
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

            _context.Attach(PurchaseItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseItemExists(PurchaseItem.ID))
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

        private bool PurchaseItemExists(int id)
        {
            return _context.PurchaseItem.Any(e => e.ID == id);
        }
    }
}
