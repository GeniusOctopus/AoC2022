using Day1.Models;
using System.Text;

namespace Day1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var elfes = new List<Elve>();

            // Part 1
            try
            {
                using FileStream fs = new(Path.GetFullPath("../../../Input/Calories.txt"), FileMode.Open, FileAccess.Read);
                using StreamReader reader = new(fs, Encoding.UTF8);

                var elfCounter = 0;
                elfes.Add(new Elve($"Elve {elfCounter + 1}", new List<int>()));
                var line = reader.ReadLine();

                while (line != null)
                {
                    if (line != "")
                    {
                        elfes[elfCounter].CarriedCalories.Add(int.Parse(line));
                    }
                    else
                    {
                        elfCounter++;
                        elfes.Add(new Elve($"Elve {elfCounter + 1}", new List<int>()));
                    }

                    line = reader.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }
            finally
            {
                elfes = elfes.OrderByDescending(x => x.CarriedCalories.Sum()).ToList();
                var maxCalories = elfes.First().CarriedCalories.Sum();
                var nameOfElf = elfes.First().Name;

                Console.WriteLine($"Elve count: {elfes.Count}");
                Console.WriteLine($"Max calories: {maxCalories}");
                Console.WriteLine($"Elve with most calories: {nameOfElf}");
                Console.ReadLine();
            }


            // Part 2
            var sumTopThree = elfes.Take(3).Sum(x => x.CarriedCalories.Sum());

            Console.WriteLine($"Sum of top three Elve's calories: {sumTopThree}");
            Console.ReadLine();
        }
    }
}