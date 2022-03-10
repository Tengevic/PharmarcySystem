namespace PharmarcySystem.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<MedicineInfo> medicinenIfos { get; set; }
    }
}