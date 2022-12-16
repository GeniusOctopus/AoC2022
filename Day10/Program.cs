namespace Day10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cycle = 0;
            var rx = 1;
            var signalStrengths = new Dictionary<int, int>();
            var crtScreen = InitScreen(width: 40, height: 6);
            var crtLine = 0;

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
                            cycle++;
                            crtLine = SetLine(cycle, crtLine);
                            crtScreen[crtLine][cycle % 40] = ShouldDrawPixel(cycle, rx);
                            signalStrengths = SetSignalStrengths(cycle, rx, signalStrengths);
                            break;
                        case "addx":
                            for (int i = 0; i < 2; i++)
                            {
                                cycle++;
                                signalStrengths = SetSignalStrengths(cycle, rx, signalStrengths);
                                if (i == 1)
                                    rx += int.Parse(instructions[1]);
                                crtLine = SetLine(cycle, crtLine);
                                crtScreen[crtLine][cycle % 40] = ShouldDrawPixel(cycle, rx);
                            }
                            break;
                    }

                    instruction = sr.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.StackTrace}");
            }
            finally
            {
                Console.WriteLine($"Sum of signal strengths: {signalStrengths.Sum(x => x.Value)}");
                Console.WriteLine("------------------------------------------");
                crtScreen.ForEach(l =>
                {
                    Console.Write("|");
                    l.ForEach(p => Console.Write(p ? "#" : "."));
                    Console.Write("|");
                    Console.WriteLine();
                });
                Console.WriteLine("------------------------------------------");
                Console.ReadLine();
            }
        }

        private static List<List<bool>> InitScreen(int width, int height)
        {
            var crtScreen = new List<List<bool>>();

            for (int i = 0; i < height; i++)
            {
                crtScreen.Add(new List<bool>());

                for (int j = 0; j < width; j++)
                {
                    crtScreen[i].Add(false);
                }
            }

            return crtScreen;
        }

        private static Dictionary<int, int> SetSignalStrengths(int cycle, int x, Dictionary<int, int> signals)
        {
            switch (cycle)
            {
                case 20: signals.Add(20, x * cycle); break;
                case 60: signals.Add(60, x * cycle); break;
                case 100: signals.Add(100, x * cycle); break;
                case 140: signals.Add(140, x * cycle); break;
                case 180: signals.Add(180, x * cycle); break;
                case 220: signals.Add(220, x * cycle); break;
            }

            return signals;
        }

        private static int SetLine(int cycles, int line)
        {
            return cycles switch
            {
                41 => ++line,
                81 => ++line,
                121 => ++line,
                161 => ++line,
                201 => ++line,
                _ => line
            };
        }

        private static bool ShouldDrawPixel(int cycles, int registerX)
        {
            var pixel = cycles % 40;
            return (pixel == registerX - 1 || pixel == registerX || pixel == registerX + 1);
        }
    }
}