using PharmarcySystem.Data;
using PharmarcySystem.Models;

namespace PharmarcySystem.Areas.Identity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PharmarcySystemContext context)
        {
            if (context.MedicineInfo.Any())
            {
                return;
            }

            var Generic = new Generic[]
            {
                new Generic { GenericName = "Genericname", Decsription="Test"},
                new Generic { GenericName = "Genericname2", Decsription ="Test2"},
                new Generic { GenericName = "Genericname3", Decsription ="Test3"},
                new Generic { GenericName = "Genericname4", Decsription ="Test4"},
                new Generic { GenericName = "Genericname5", Decsription ="Test5"}
            };
            context.Generic.AddRange(Generic);
            context.SaveChanges();

            var Manufacture = new Manufacturer[]
            {
                new Manufacturer { Name ="Manufacture", Description ="Test1"},
                new Manufacturer { Name ="Manufacture1", Description ="Test2"},
                new Manufacturer { Name ="Manufacture2", Description ="Test3"},
                new Manufacturer { Name ="Manufacture4", Description ="Test"},
                new Manufacturer { Name ="Manufacture5", Description ="Test5"}

            };
            context.Manufacturer.AddRange(Manufacture);
            context.SaveChanges();

            var medicine = new MedicineInfo[]
            {
                new MedicineInfo { ManufacturerId=1, MedicineName="Panadol", GenericId=1, Category="Pain Killars",Sell_Price = 20, Stock = 0},
                new MedicineInfo { ManufacturerId=2, MedicineName="Bruffen", GenericId=2, Category="Pain Killars",Sell_Price = 20, Stock = 0},
                new MedicineInfo { ManufacturerId=3, MedicineName="Asprin", GenericId=3, Category="Pain Killars",Sell_Price = 20,Stock = 0},
                new MedicineInfo { ManufacturerId=4, MedicineName="Medicinename2", GenericId=4, Category="Antibiotics",Sell_Price = 20,Stock = 0},
                new MedicineInfo { ManufacturerId=5, MedicineName="Panadol", GenericId=5, Category="Pain Killars",Sell_Price = 20,Stock = 0},
                new MedicineInfo { ManufacturerId=4, MedicineName="Medicinename2", GenericId=5, Category="Antibiotics",Sell_Price = 20,Stock = 0},
                new MedicineInfo { ManufacturerId=1, MedicineName="Medicinename2", GenericId=3, Category="Antibiotics",Sell_Price = 20,Stock = 0}

            };
            context.MedicineInfo.AddRange(medicine);
            context.SaveChanges();

            var supplier = new Supplier[]
            {
                new Supplier{SupplierName = "Supplier", SupplierDescription = "Description"},
                new Supplier{SupplierName = "Supplier1", SupplierDescription = "Description2"},
                new Supplier{SupplierName = "Supplier2", SupplierDescription = "Description3"},
                new Supplier{SupplierName = "Supplier3", SupplierDescription = "Description4"},
                new Supplier{SupplierName = "Supplier4", SupplierDescription = "Description5"},
            };
            context.Supplier.AddRange(supplier);
            context.SaveChanges();

           

       
        }
    }
}
