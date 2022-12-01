namespace Day1.Models
{
    internal class Elf
    {
        public Elf() {}

        public Elf(string name, List<int> carriedCalories)
        {
            Name = name;
            CarriedCalories = carriedCalories;
        }

        public string Name { get; set; }
        public List<int> CarriedCalories { get; set; }
    }
}
