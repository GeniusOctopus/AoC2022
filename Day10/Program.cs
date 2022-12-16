using Microsoft.Win32;

namespace Day10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cycles = 0;
            var registerX = 1;
            var measuredSignalStrength = new Dictionary<int, int>();

            try
            {
                using var fs = new FileStream(Path.GetFullPath("../../../Input/Instructions.txt"), FileMode.Open, FileAccess.Read);
                using var sr = new StreamReader(fs);

                var instruction = sr.ReadLine();

                while (instruction is not null)
                {
                    var instructions = instruction.Split(' ');

                    switch (instructions[0])
                    {
                        case "noop":
                            cycles++;
                            measuredSignalStrength = SetSignalStrengths(cycles, registerX, measuredSignalStrength);
                            break;
                        case "addx":
                            for (int i = 0; i < 2; i++)
                            {
                                cycles++;
                                measuredSignalStrength = SetSignalStrengths(cycles, registerX, measuredSignalStrength);
                                if (i == 1)
                                    registerX += int.Parse(instructions[1]);
                            }
                            break;
                    }

                    instruction = sr.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }
            finally
            {
                Console.WriteLine($"Sum: {measuredSignalStrength.Sum(x => x.Value)}");
                Console.ReadLine();
            }
        }

        private static Dictionary<int, int> SetSignalStrengths(int cycle, int x, Dictionary<int, int> signals)
        {
            switch (cycle)
            {
                case 20:
                    signals.Add(20, x * cycle);
                    break;
                case 60:
                    signals.Add(60, x * cycle);
                    break;
                case 100:
                    signals.Add(100, x * cycle);
                    break;
                case 140:
                    signals.Add(140, x * cycle);
                    break;
                case 180:
                    signals.Add(180, x * cycle);
                    break;
                case 220:
                    signals.Add(220, x * cycle);
                    break;
            }

            return signals;
        }
    }
}