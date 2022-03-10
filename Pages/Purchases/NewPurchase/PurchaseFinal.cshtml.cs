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
    public class PurchaseFinalModel : PageModel
    {
        private readonly PharmarcySystem.Data.PharmarcySystemContext _context;

        public PurchaseFinalModel(PharmarcySystem.Data.PharmarcySystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PurchaseEntryVM PurchaseEntryVM { get; set; }
        public PurchaseEntryVM purchase { get; set; }
        public double Sum { get; set; }
        public IList<PurchaseItem> Item { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id, int? ID)
        {
            if (id == null)
            {
                return NotFound();
            }
                purchase = await _context.PurchaseEntryVM
                       .Include(p => p.supplier)
                       .Include(p => p.PurchaseItems)
                           .ThenInclude(p => p.medicineInfo)
                       .AsNoTracking()
                       .FirstOrDefaultAsync(m => m.Purchase_ID == id);
     
            Item = await _context.PurchaseItem
                .Where(m => m.PurchaseEntryVMID == purchase.ID)    
                .ToListAsync();

            Sum = 0;
            foreach (var i in Item)
            {
                Sum = (int)i.Cost_Price + Sum;
            }

            PurchaseEntryVM = new PurchaseEntryVM { 
                ID = purchase.ID,
                Amount = Sum,
                Description = purchase.Description,
                Discount = purchase.Discount,
                Discount_Amount = purchase.Discount_Amount,
                Entry_Date = purchase.Entry_Date,
                Grand_Total = purchase.Grand_Total,
                IsPaid = purchase.IsPaid,
                PurchaseItems = purchase.PurchaseItems,
                Purchase_ID = purchase.Purchase_ID,
                supplier = purchase.supplier,
                SupplierID= purchase.SupplierID,
            };
                
            if (PurchaseEntryVM == null)
            {
                return NotFound();
            }
           ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierId", "SupplierName");
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
            
            

            if (PurchaseEntryVM.Discount > 0) 
            {
                PurchaseEntryVM.Discount_Amount = PurchaseEntryVM.Amount * PurchaseEntryVM.Discount/100;
            }

            PurchaseEntryVM.Grand_Total = PurchaseEntryVM.Amount - PurchaseEntryVM.Discount_Amount;
            _context.Attach(PurchaseEntryVM).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseEntryVMExists(PurchaseEntryVM.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Purchases/Purchase/Index");
        }

        private bool PurchaseEntryVMExists(int id)
        {
            return _context.PurchaseEntryVM.Any(e => e.ID == id);
        }

    }
}
