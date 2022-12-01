namespace Day1.Models
{
    internal class Elve
    {
        public Elve() {}

        public Elve(string name, List<int> carriedCalories)
        {
            Name = name;
            CarriedCalories = carriedCalories;
        }

        public string Name { get; set; }
        public List<int> CarriedCalories { get; set; }
    }
}
