namespace Day11.Models
{
    internal class Monkey
    {
        public ulong Id { get; set; }
        public List<ulong> Items { get; set; }
        public ulong InspectedItemsCount { get; set; }
        public char Operator { get; set; }
        public string Operant1 { get; set; }
        public string Operant2 { get; set; }
        public ulong Divisor { get; set; }
        public ulong MonkeyIdIfTrue { get; set; }
        public ulong MonkeyIdIfFalse { get; set; }

        public bool Test(ulong worry)
        {
            return worry % Divisor == 0;
        }
    }
}
