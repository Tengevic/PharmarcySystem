using System.ComponentModel.DataAnnotations;

namespace PharmarcySystem.Models
{
    public class PurchaseItem
    {
        public int ID { get; set; }
        public int Batch_ID { get; set; }
        [Display(Name = "Units")]
        public int Quantity { get; set; }
        public int Cost_Price { get; set; }
        public System.DateTime Production_Date { get; set; }
        public System.DateTime Expire_Date { get; set; }
        public int PurchaseEntryVMID { get; set; }
        public int MedicineInfoID { get; set; }

        //Add list from sales

        public MedicineInfo medicineInfo { get; set; }
        public PurchaseEntryVM purchaseEntryVM { get; set; }
    }
}