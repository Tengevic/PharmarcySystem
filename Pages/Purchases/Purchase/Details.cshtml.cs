using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PharmarcySystem.Data;
using PharmarcySystem.Models;

namespace PharmarcySystem.Pages.Purchase
{
    public class DetailsModel : PageModel
    {
        private readonly PharmarcySystem.Data.PharmarcySystemContext _context;

        public DetailsModel(PharmarcySystem.Data.PharmarcySystemContext context)
        {
            _context = context;
        }

        public PurchaseEntryVM PurchaseEntryVM { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PurchaseEntryVM = await _context.PurchaseEntryVM
                                .Include(p => p.PurchaseItems)
                                    .ThenInclude(p => p.medicineInfo)
                                .Include(p => p.supplier).FirstOrDefaultAsync(m => m.ID == id);

            if (PurchaseEntryVM == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
