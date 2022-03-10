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
    public class IndexModel : PageModel
    {
        private readonly PharmarcySystem.Data.PharmarcySystemContext _context;

        public IndexModel(PharmarcySystem.Data.PharmarcySystemContext context)
        {
            _context = context;
        }

        public IList<PurchaseItem> PurchaseItem { get;set; }

        public async Task OnGetAsync()
        {
            PurchaseItem = await _context.PurchaseItem
                .Include(p => p.medicineInfo)
                .Include(p => p.purchaseEntryVM).ToListAsync();
        }
    }
}
