using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PharmarcySystem.Data;
using PharmarcySystem.Models;

namespace PharmarcySystem.Data;

public class PharmarcySystemContext : IdentityDbContext<PharmarcySystemUser>
{
    public PharmarcySystemContext(DbContextOptions<PharmarcySystemContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<PharmarcySystem.Models.Manufacturer> Manufacturer { get; set; }

    public DbSet<PharmarcySystem.Models.MedicineInfo> MedicineInfo { get; set; }

    public DbSet<PharmarcySystem.Models.Generic> Generic { get; set; }

    public DbSet<PharmarcySystem.Models.PurchaseEntryVM> PurchaseEntryVM { get; set; }

    public DbSet<PharmarcySystem.Models.PurchaseItem> PurchaseItem { get; set; }

    public DbSet<PharmarcySystem.Models.Supplier> Supplier { get; set; }

    public DbSet<PharmarcySystem.Models.Sales> Sales { get; set; }

    public DbSet<PharmarcySystem.Models.Invoice> Invoice { get; set; }
}
