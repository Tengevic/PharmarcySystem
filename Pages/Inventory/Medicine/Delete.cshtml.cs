﻿using System;
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
    public class DeleteModel : PageModel
    {
        private readonly PharmarcySystem.Data.PharmarcySystemContext _context;

        public DeleteModel(PharmarcySystem.Data.PharmarcySystemContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MedicineInfo = await _context.MedicineInfo.FindAsync(id);

            if (MedicineInfo != null)
            {
                _context.MedicineInfo.Remove(MedicineInfo);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
