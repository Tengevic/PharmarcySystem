using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmarcySystem.Data;
using PharmarcySystem.Models;

namespace PharmarcySystem.Pages.SalesPage.NewSales
{
    public class NewSalesModel : PageModel
    {
        private readonly PharmarcySystem.Data.PharmarcySystemContext _context;

        public NewSalesModel(PharmarcySystem.Data.PharmarcySystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Invoice Invoice { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Invoice.Total_Payable = 0;
            Invoice.Discount = 0;
            Invoice.Total_Amount = 0;
            Invoice.Discount = 0;
            Invoice.Discount_Amount = 0;

            if (!ModelState.IsValid)
            {
                return Page();
            }


            _context.Invoice.Add(Invoice);
            var id = Invoice.Invoice_No;
            await _context.SaveChangesAsync();

            return RedirectToPage("./SalesItems", new { id = id });
        }
    }
}
