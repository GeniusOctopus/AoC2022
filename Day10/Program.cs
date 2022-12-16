namespace Day10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cycles = 0;
            var signalStrenth = 0;
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
                            switch (cycles)
                            {
                                case 20:
                                    measuredSignalStrength.Add(20, registerX * cycles);
                                    break;
                                case 60:
                                    measuredSignalStrength.Add(60, registerX * cycles);
                                    break;
                                case 100:
                                    measuredSignalStrength.Add(100, registerX * cycles);
                                    break;
                                case 140:
                                    measuredSignalStrength.Add(140, registerX * cycles);
                                    break;
                                case 180:
                                    measuredSignalStrength.Add(180, registerX * cycles);
                                    break;
                                case 220:
                                    measuredSignalStrength.Add(220, registerX * cycles);
                                    break;
                            }
                            break;
                        case "addx":
                            for (int i = 0; i < 2; i++)
                            {
                                cycles++;
                                switch (cycles)
                                {
                                    case 20:
                                        measuredSignalStrength.Add(20, registerX * cycles);
                                        break;
                                    case 60:
                                        measuredSignalStrength.Add(60, registerX * cycles);
                                        break;
                                    case 100:
                                        measuredSignalStrength.Add(100, registerX * cycles);
                                        break;
                                    case 140:
                                        measuredSignalStrength.Add(140, registerX * cycles);
                                        break;
                                    case 180:
                                        measuredSignalStrength.Add(180, registerX * cycles);
                                        break;
                                    case 220:
                                        measuredSignalStrength.Add(220, registerX * cycles);
                                        break;
                                }
                                if (i == 1)
                                    registerX += int.Parse(instructions[1]);
                            }
                            break;
                    }
                    //Console.WriteLine($"{instructions[0]} {(instructions.Length > 1 ? instructions[1] : string.Empty)}; X: {registerX} signal strength: {registerX * cycles}; end of cycle: {cycles}");
                    //Console.ReadLine();

                    instruction = sr.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }
            finally
            {
                foreach (var signal in measuredSignalStrength)
                {
                    Console.WriteLine($"cycle: {signal.Key}, signal strength: {signal.Value}");
                }
                Console.WriteLine($"Sum: {measuredSignalStrength.Sum(x => x.Value)}");
                Console.WriteLine($"cycles: {cycles}");
                Console.ReadLine();
            }
        }
    }
}