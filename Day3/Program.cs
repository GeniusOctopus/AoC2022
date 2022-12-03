namespace Day3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sum = 0;
            var sumBadges = 0;
            var rucksacks = new List<string>();

            try
            {
                using var fs = new FileStream(Path.GetFullPath("../../../Input/Rucksacks.txt"), FileMode.Open, FileAccess.Read);
                using var sr = new StreamReader(fs);

                var counter = 0;
                var rucksack = sr.ReadLine();

                while (rucksack != null)
                {
                    rucksacks.Add(rucksack);
                    var compartment1 = rucksack[..(rucksack.Length / 2)];
                    var compartment2 = rucksack.Substring(startIndex: rucksack.Length / 2, rucksack.Length - (rucksack.Length / 2));

                    foreach (char c in compartment1)
                    {
                        if (compartment2.Contains(c))
                        {
                            sum += ValueOf(c);
                            break;
                        }
                    }
                    counter++;
                    rucksack = sr.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }
            finally
            {
                Console.WriteLine($"Sum of all contents: {sum}");
                Console.ReadLine();
            }

            for (int i = 0; i < rucksacks.Count; i += 3)
            {
                foreach (char c in rucksacks[i])
                {
                    if (rucksacks[i + 1].Contains(c) && rucksacks[i + 2].Contains(c))
                    {
                        sumBadges += ValueOf(c);
                        Console.WriteLine(i);
                        break;
                    }
                }
            }

            Console.WriteLine($"Sum of all Badges: {sumBadges}");
            Console.ReadLine();
        }

        static int ValueOf(char c)
        {
            if (c >= 97)
            {
                return c - 96;
            }
            else
            {
                return c - 38;
            }
        }
    }
}