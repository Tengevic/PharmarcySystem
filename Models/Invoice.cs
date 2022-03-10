namespace PharmarcySystem.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int Invoice_No { get; set; }
        public double Total_Amount { get; set; }
        public double Discount { get; set; }
        public double Discount_Amount { get; set; }
        public double Total_Payable { get; set; }
        public bool Paid { get; set; }
        public bool Returned { get; set; }
        public DateTime Date { get; set; }

        public ICollection<Sales> SalesItems { get; set; }
    }
}