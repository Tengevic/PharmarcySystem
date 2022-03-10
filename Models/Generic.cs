namespace PharmarcySystem.Models
{
    public class Generic
    {
        public int Id { get; set; }
        public string GenericName { get; set; }
        public String Decsription { get; set; }

        public ICollection<MedicineInfo> MedicineInfos { get; set; }
    }
}
