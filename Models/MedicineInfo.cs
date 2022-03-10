using System.ComponentModel.DataAnnotations;

namespace PharmarcySystem.Models
{
    public class MedicineInfo
    {
        public int Id { get; set; }
        public string MedicineName { get; set; }
        public string Category { get; set; }
        public int GenericId { get; set; }
        [Display(Name = "Selling price per unit")]
        public int Sell_Price { get; set; }
        public int ManufacturerId { get; set; }
        public int Stock { get; set; }
        public Generic Generic { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public ICollection<Sales> sales { get; set; }
        public ICollection<PurchaseItem> PurchaseItems { get; set; }
    }
}