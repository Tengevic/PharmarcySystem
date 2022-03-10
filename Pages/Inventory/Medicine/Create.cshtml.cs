using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmarcySystem.Data;
using PharmarcySystem.Models;

namespace PharmarcySystem.Pages.Inventory.Medicine
{
    public class CreateModel : PageModel
    {
        private readonly PharmarcySystem.Data.PharmarcySystemContext _context;

        public CreateModel(PharmarcySystem.Data.PharmarcySystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["GenericId"] = new SelectList(_context.Generic, "Id", "Id");
        ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public MedicineInfo MedicineInfo { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MedicineInfo.Add(MedicineInfo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
