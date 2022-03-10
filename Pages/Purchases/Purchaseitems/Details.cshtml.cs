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
    public class DetailsModel : PageModel
    {
        private readonly PharmarcySystem.Data.PharmarcySystemContext _context;

        public DetailsModel(PharmarcySystem.Data.PharmarcySystemContext context)
        {
            _context = context;
        }

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
    }
}
