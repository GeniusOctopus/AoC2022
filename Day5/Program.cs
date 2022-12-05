namespace Day5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cargoStacksPart1 = new List<Stack<char>>();
            var cargoStacksPart2 = new List<Stack<char>>();

            try
            {
                using var fs = new FileStream(Path.GetFullPath("../../../Input/Cargo.txt"), FileMode.Open, FileAccess.Read);
                using var sr = new StreamReader(fs);

                var line = sr.ReadLine();

                cargoStacksPart1 = InitializeStacks(line);
                cargoStacksPart2 = InitializeStacks(line);

                while (line != null)
                {
                    var crateCounter = 0;
                    var stackCoutner = 0;

                    if (line.Any())
                        if (line[0] == 'm')
                        {
                            var actions = line.Split(' ');
                            cargoStacksPart1 = CrateMover9000(int.Parse(actions[1]), int.Parse(actions[3]) - 1, int.Parse(actions[5]) - 1, cargoStacksPart1);
                            cargoStacksPart2 = CrateMover9001(int.Parse(actions[1]), int.Parse(actions[3]) - 1, int.Parse(actions[5]) - 1, cargoStacksPart2);
                        }
                        else if (line[0] == '[')
                        {
                            foreach (var c in line)
                            {
                                if (crateCounter < 3)
                                {
                                    if (crateCounter == 1 && c != ' ')
                                    {
                                        cargoStacksPart1[stackCoutner].Push(c);
                                        cargoStacksPart2[stackCoutner].Push(c);
                                    }
                                    crateCounter++;
                                }
                                else
                                {
                                    crateCounter = 0;
                                    stackCoutner++;
                                }
                            }
                        }
                        else if (line[0] == ' ')
                        {
                            cargoStacksPart1 = ReverseStacks(cargoStacksPart1);
                            cargoStacksPart2 = ReverseStacks(cargoStacksPart2);
                        }

                    line = sr.ReadLine();
                    stackCoutner = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("CrateMover9000:");
                cargoStacksPart1.ForEach(x => Console.Write(x.Peek()));
                Console.WriteLine();
                Console.WriteLine("CrateMover9000:");
                cargoStacksPart2.ForEach(x => Console.Write(x.Peek()));
                Console.WriteLine();
                Console.ReadLine();
            }
        }

        private static List<Stack<char>> CrateMover9001(int count, int source, int destination, List<Stack<char>> cargoStacks)
        {
            var auxilary = new Stack<char>();

            for (int i = 0; i < count; i++)
            {
                auxilary.Push(cargoStacks[source].Pop());
            }

            for (int i = 0; i < count; i++)
            {
                cargoStacks[destination].Push(auxilary.Pop());
            }

            return cargoStacks;
        }

        private static List<Stack<char>> CrateMover9000(int count, int source, int destination, List<Stack<char>> cargoStacks)
        {
            for (int i = 0; i < count; i++)
            {
                cargoStacks[destination].Push(cargoStacks[source].Pop());
            }

            return cargoStacks;
        }

        private static List<Stack<char>> ReverseStacks(List<Stack<char>> cargoStacks)
        {
            var auxilary = new List<Stack<char>>();

            for (int i = 0; i < cargoStacks.Count; i++)
            {
                auxilary.Add(new Stack<char>());
            }

            for (int i = 0; i < cargoStacks.Count; i++)
                while (cargoStacks[i].Count > 0)
                    auxilary[i].Push(cargoStacks[i].Pop());

            return auxilary;
        }

        private static List<Stack<char>> InitializeStacks(string line)
        {
            var cargoStacks = new List<Stack<char>>();
            var crateCounter = 0;
            var stackCoutner = 0;

            foreach (var c in line)
            {
                if (crateCounter < 3)
                {
                    if (crateCounter == 1)
                        cargoStacks.Add(new Stack<char>());
                    crateCounter++;
                }
                else
                {
                    crateCounter = 0;
                    stackCoutner++;
                }
            }

            return cargoStacks;
        }
    }
}
