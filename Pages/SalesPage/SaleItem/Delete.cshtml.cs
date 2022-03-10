using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PharmarcySystem.Data;
using PharmarcySystem.Models;

namespace PharmarcySystem.Pages.SalesPage.SalaeItem
{
    public class DeleteModel : PageModel
    {
        private readonly PharmarcySystem.Data.PharmarcySystemContext _context;

        public DeleteModel(PharmarcySystem.Data.PharmarcySystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Sales Sales { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Sales = await _context.Sales
                .Include(s => s.invoice)
                .Include(s => s.medicineInfo).FirstOrDefaultAsync(m => m.SalesId == id);

            if (Sales == null)
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

            Sales = await _context.Sales.FindAsync(id);
            Invoice invoice = await _context.Invoice.FindAsync(Sales.InvoiceId);
            var Id = invoice.Invoice_No;

            if (Sales != null)
            {   
                MedicineInfo Sell_price = _context.MedicineInfo.FirstOrDefault(m => m.Id == Sales.MedicineInfoID);
                Sell_price.Stock = Sell_price.Stock + Sales.Quantity;
                _context.Attach(Sell_price).State = EntityState.Modified;

                _context.Sales.Remove(Sales);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/SalesPage/NewSales/SalesItems", new { id =Id});
        }
    }
}
