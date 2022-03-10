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

namespace PharmarcySystem.Pages.NewPurchase
{
    public class NewPurchaseItwemModel : PageModel
    {
        private readonly PharmarcySystem.Data.PharmarcySystemContext _context;

        public NewPurchaseItwemModel(PharmarcySystem.Data.PharmarcySystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PurchaseEntryVM = await _context.PurchaseEntryVM
                        .FirstOrDefaultAsync(m => m.Purchase_ID == id);

            ViewData["MedicineInfoID"] = new SelectList(_context.MedicineInfo, "Id", "MedicineName");
        ViewData["PurchaseEntryVMID"] = new SelectList(_context.PurchaseEntryVM.Where(p => p.Purchase_ID == id), "ID", "Purchase_ID");
            return Page();
        }

        [BindProperty]
        public PurchaseItem PurchaseItem { get; set; }
        public PurchaseEntryVM PurchaseEntryVM { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
  
            MedicineInfo MedicineInfo = await _context.MedicineInfo
              .FirstOrDefaultAsync(m => m.Id == PurchaseItem.MedicineInfoID);

            MedicineInfo.Stock = MedicineInfo.Stock + PurchaseItem.Quantity;

            _context.Attach(MedicineInfo).State = EntityState.Modified;

            _context.PurchaseItem.Add(PurchaseItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineInfoExists(MedicineInfo.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return RedirectToPage("./PurchaseFinal", new { id = id });
        }
        private bool MedicineInfoExists(int id)
        {
            return _context.MedicineInfo.Any(e => e.Id == id);
        }
    }
}
