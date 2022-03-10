using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PharmarcySystem.Data;
using PharmarcySystem.Models;

namespace PharmarcySystem.Pages.Purchaseitems
{
    public class DeleteModel : PageModel
    {
        private readonly PharmarcySystem.Data.PharmarcySystemContext _context;

        public DeleteModel(PharmarcySystem.Data.PharmarcySystemContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PurchaseItem = await _context.PurchaseItem.FindAsync(id);
            PurchaseEntryVM purchaseEntryVM = await _context.PurchaseEntryVM.FindAsync(PurchaseItem.PurchaseEntryVMID);
            var Id = purchaseEntryVM.Purchase_ID;

            if (PurchaseItem != null)
            {
                MedicineInfo MedicineInfo = await _context.MedicineInfo
             .FirstOrDefaultAsync(m => m.Id == PurchaseItem.MedicineInfoID);

                MedicineInfo.Stock = MedicineInfo.Stock - PurchaseItem.Quantity;

                _context.Attach(MedicineInfo).State = EntityState.Modified;

                _context.PurchaseItem.Remove(PurchaseItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Purchases/NewPurchase/PurchaseFinal", new { id = Id});
        }
    }
}
