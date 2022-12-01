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

                var maxCalories = elfes.Max(x => x.CarriedCalories.Sum());
                var nameOfElf = elfes.First(x => x.CarriedCalories.Sum() == maxCalories).Name;

                Console.WriteLine($"Elve count: {elfes.Count}");
                Console.WriteLine($"Max calories: {maxCalories}");
                Console.WriteLine($"Elve with most calories: {nameOfElf}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }
            finally
            {
                Console.ReadLine();
            }

            var sumTopThree = 0;

            // Part 2
            for (int i = 1; i < 4; i++)
            {
                var maxCalories = elfes.Max(x => x.CarriedCalories.Sum());
                Console.WriteLine($"Max calories: {maxCalories}");
                sumTopThree += maxCalories;
                var indexOfTopElf = elfes.IndexOf(elfes.First(x => x.CarriedCalories.Sum() == maxCalories));
                Console.WriteLine($"{i}. Elve: {elfes[indexOfTopElf].Name}");
                elfes.RemoveAt(indexOfTopElf);
            }

            Console.WriteLine($"Sum of top three Elve's calories: {sumTopThree}");
            Console.ReadLine();
        }
    }
}