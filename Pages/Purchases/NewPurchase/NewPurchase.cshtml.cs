using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmarcySystem.Data;
using PharmarcySystem.Models;

namespace PharmarcySystem.Pages.NewPurchase
{
    public class NewPurchaseModel : PageModel
    {
        private readonly PharmarcySystem.Data.PharmarcySystemContext _context;

        public NewPurchaseModel(PharmarcySystem.Data.PharmarcySystemContext context)
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
            PurchaseEntryVM.Amount = 0;
            PurchaseEntryVM.Discount = 0;
            PurchaseEntryVM.Discount_Amount = 0;
            PurchaseEntryVM.Grand_Total = 0;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PurchaseEntryVM.Add(PurchaseEntryVM);
            int id = PurchaseEntryVM.Purchase_ID;
            await _context.SaveChangesAsync();

            return RedirectToPage("./PurchaseFinal", new { id = id });
        }
    }
}
