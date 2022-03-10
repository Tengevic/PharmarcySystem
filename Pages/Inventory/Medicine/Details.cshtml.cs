using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PharmarcySystem.Data;
using PharmarcySystem.Models;

namespace PharmarcySystem.Pages.Inventory.Medicine
{
    public class DetailsModel : PageModel
    {
        private readonly PharmarcySystem.Data.PharmarcySystemContext _context;

        public DetailsModel(PharmarcySystem.Data.PharmarcySystemContext context)
        {
            _context = context;
        }

        public MedicineInfo MedicineInfo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MedicineInfo = await _context.MedicineInfo
                .Include(m => m.Generic)
                .Include(m => m.Manufacturer).FirstOrDefaultAsync(m => m.Id == id);

            if (MedicineInfo == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
