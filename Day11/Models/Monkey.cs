namespace Day11.Models
{
    internal class Monkey
    {
        public int Id { get; set; }
        public List<int> Items { get; set; }
        public int InspectedItemsCount { get; set; }
        public char Operator { get; set; }
        public string Operant1 { get; set; }
        public string Operant2 { get; set; }
        public int Divisor { get; set; }
        public int MonkeyIdIfTrue { get; set; }
        public int MonkeyIdIfFalse { get; set; }

        public bool Test(int worry)
        {
            return worry % Divisor == 0;
        }
    }
}
