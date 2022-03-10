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
    public class CreateSaleItemModel : PageModel
    {
        private readonly PharmarcySystem.Data.PharmarcySystemContext _context;

        public CreateSaleItemModel(PharmarcySystem.Data.PharmarcySystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
        ViewData["InvoiceId"] = new SelectList(_context.Invoice.Where(i => i.Invoice_No == id), "Id", "Invoice_No");
        ViewData["MedicineInfoID"] = new SelectList(_context.MedicineInfo, "Id", "MedicineName");
            return Page();
        }

        [BindProperty]
        public Sales Sales { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int id, [Bind("SalesId ,Quantity,Cost ,Amount,MedicineInfoID,InvoiceId ")] Sales sales)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            MedicineInfo Sell_price = _context.MedicineInfo.FirstOrDefault(m => m.Id == sales.MedicineInfoID);
            Sales salesToDb = new Sales {
                Quantity = sales.Quantity,
                Cost = Sell_price.Sell_Price,
                Amount = sales.Quantity * Sell_price.Sell_Price,
                MedicineInfoID = sales.MedicineInfoID,
                InvoiceId = sales.InvoiceId
            };

            Sell_price.Stock = Sell_price.Stock - sales.Quantity;
            _context.Sales.Add(salesToDb);
            _context.Attach(Sell_price).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineInfoExists(Sell_price.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("./SalesItems", new { id = id });
        }
        private bool MedicineInfoExists(int id)
        {
            return _context.MedicineInfo.Any(e => e.Id == id);
        }
    }
}
