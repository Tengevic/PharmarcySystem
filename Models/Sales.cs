namespace PharmarcySystem.Models
{
    public class Sales
    {
        public int SalesId { get; set; }
        public int Quantity { get; set; }
        public double? Cost { get; set; }
        public double? Amount { get; set; }
        public int MedicineInfoID { get; set; }
        public int InvoiceId { get; set; }

        //add BatchId from purchaseItems
        
        public MedicineInfo medicineInfo { get; set; }
        public Invoice invoice { get; set; }

    }
}