using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmarcySystem.Data;
using PharmarcySystem.Models;

namespace PharmarcySystem.Pages.Purchases.NewPurchase
{
    public class tsetModel : PageModel
    {
        private readonly PharmarcySystem.Data.PharmarcySystemContext _context;

        public tsetModel(PharmarcySystem.Data.PharmarcySystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierId", "SupplierId");
            return Page();
        }

        [BindProperty]
        public PurchaseEntryVM PurchaseEntryVM { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PurchaseEntryVM.Add(PurchaseEntryVM);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
