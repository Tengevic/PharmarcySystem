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
    public class IndexModel : PageModel
    {
        private readonly PharmarcySystem.Data.PharmarcySystemContext _context;

        public IndexModel(PharmarcySystem.Data.PharmarcySystemContext context)
        {
            _context = context;
        }

        public IList<Sales> Sales { get;set; }

        public async Task OnGetAsync()
        {
            Sales = await _context.Sales
                .Include(s => s.invoice)
                .Include(s => s.medicineInfo).ToListAsync();
        }
    }
}
