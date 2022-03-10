using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmarcySystem.Data;
using PharmarcySystem.Models;

namespace PharmarcySystem.Pages.Purchaseitems
{
    public class CreateModel : PageModel
    {
        private readonly PharmarcySystem.Data.PharmarcySystemContext _context;

        public CreateModel(PharmarcySystem.Data.PharmarcySystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["MedicineInfoID"] = new SelectList(_context.MedicineInfo, "Id", "Id");
        ViewData["PurchaseEntryVMID"] = new SelectList(_context.PurchaseEntryVM, "ID", "ID");
            return Page();
        }

        [BindProperty]
        public PurchaseItem PurchaseItem { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PurchaseItem.Add(PurchaseItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
