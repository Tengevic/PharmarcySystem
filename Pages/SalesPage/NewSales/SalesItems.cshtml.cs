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

namespace PharmarcySystem.Pages.SalesPage.NewSales
{
    public class SalesItemsModel : PageModel
    {
        private readonly PharmarcySystem.Data.PharmarcySystemContext _context;

        public SalesItemsModel(PharmarcySystem.Data.PharmarcySystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Invoice Invoice { get; set; }
        public IList<Sales> Sales { get; set; }
        public Invoice Invoice2 { get; set; }
        public double Sum { get; set; }
        public double Discount_Amount { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Invoice2 = await _context.Invoice
                        .Include(p => p.SalesItems)
                            .ThenInclude(p => p.medicineInfo)
                        .FirstOrDefaultAsync(m => m.Invoice_No == id);

            Sales = await _context.Sales
                .Where(m => m.InvoiceId == Invoice2.Id)
                .ToListAsync();
            // find the sum of items in the list
            Sum = 0;
            foreach(var item in Sales)
            {
                Sum = (int)item.Amount + Sum;
            }

            Invoice = new Invoice {
                Id = Invoice2.Id,
                Invoice_No = Invoice2.Invoice_No,
                Date = Invoice2.Date,
                Total_Amount = Sum,
                Discount = Invoice2.Discount,
                Discount_Amount = Invoice2.Discount_Amount,
                Total_Payable = Invoice2.Total_Payable,
                SalesItems =Invoice2.SalesItems,
            };

            if (Invoice == null)
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
            if (Invoice.Discount > 0)
            {
                Invoice.Discount_Amount = Invoice.Discount * Invoice.Total_Amount/100;
            }

            Invoice.Total_Payable = Invoice.Total_Amount - Invoice.Discount_Amount;
            _context.Attach(Invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(Invoice.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/SalesPage/Invoices/Index");
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoice.Any(e => e.Id == id);
        }
    }
}
