using Day11.Models;

namespace Day11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var monkeys = new List<Monkey>();
            var monkeyBusiness = 0;

            try
            {
                using var fs = new FileStream(Path.GetFullPath("../../../Input/monkeys.txt"), FileMode.Open, FileAccess.Read);
                using var sr = new StreamReader(fs);

                var line = sr.ReadLine();

                while (line is not null)
                {
                    if (line.Contains("Monkey"))
                    {
                        monkeys.Add(new Monkey() { Id = int.Parse(line.Replace(":", "").Last().ToString()) });

                        Console.WriteLine($"Monkey {monkeys.Last().Id}");
                    }
                    else if (line.Contains("Starting items"))
                    {
                        var items = line[(line.IndexOf(':') + 1)..].Replace(" ", "").Split(',').Select(x => int.Parse(x)).ToList();
                        monkeys.Last().Items = items;

                        Console.Write($"Items: ");
                        monkeys.Last().Items.ForEach(i => Console.Write($"{i}, "));
                        Console.WriteLine();
                    }
                    else if (line.Contains("Operation"))
                    {
                        var term = line.Replace("  Operation: ", "").Split(' ');
                        monkeys.Last().Operant1 = term[2];
                        switch (term[3].First())
                        {
                            case '+':
                                monkeys.Last().Operator = '+';
                                break;
                            case '*':
                                monkeys.Last().Operator = '*';
                                break;
                        }
                        monkeys.Last().Operant2 = term[4];
                        Console.WriteLine($"Arihm. operation: new = {monkeys.Last().Operant1} {monkeys.Last().Operator} {monkeys.Last().Operant2}");
                    }
                    else if (line.Contains("Test"))
                    {
                        monkeys.Last().Divisor = int.Parse(line.Replace("  Test: divisible by ", ""));
                        Console.WriteLine($"Must be dividable by: {monkeys.Last().Divisor}");
                    }
                    else if (line.Contains("If true"))
                    {
                        monkeys.Last().MonkeyIdIfTrue = int.Parse(line.Replace("    If true: throw to monkey ", ""));
                        Console.WriteLine($"If true throw to: {monkeys.Last().MonkeyIdIfTrue}");
                    }
                    else if (line.Contains("If false"))
                    {
                        monkeys.Last().MonkeyIdIfFalse = int.Parse(line.Replace("    If false: throw to monkey ", ""));
                        Console.WriteLine($"If false throw to: {monkeys.Last().MonkeyIdIfFalse}");
                    }

                    line = sr.ReadLine();
                }

                for (int i = 0; i < 20; i++)
                {
                    foreach (var monkey in monkeys)
                    {
                        while (monkey.Items.Count > 0)
                        {
                            var worry = monkey.Items.First();
                            var newWorry = Operation(monkey.Operant1, monkey.Operant2, monkey.Operator, worry);
                            newWorry /= 3;
                            if (monkey.Test(newWorry))
                                monkeys.Where(m => m.Id == monkey.MonkeyIdIfTrue).First().Items.Add(newWorry);
                            else
                                monkeys.Where(m => m.Id == monkey.MonkeyIdIfFalse).First().Items.Add(newWorry);
                            monkey.Items.Remove(worry);
                            monkey.InspectedItemsCount++;
                        }
                    }
                }

                monkeys = monkeys.OrderByDescending(m => m.InspectedItemsCount).ToList();
                monkeyBusiness = monkeys.Take(2).Aggregate(1, (monk1, monk2) => monk1 * monk2.InspectedItemsCount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }
            finally
            {
                monkeys.ForEach(m => Console.WriteLine($"monkey {m.Id}: {m.InspectedItemsCount} items inspected"));
                Console.WriteLine($"Monkey Business: {monkeyBusiness}");
                Console.ReadLine();
            }
        }

        static int Operation(string op1, string op2, char op, int oldVal)
        {
            var res1 = int.TryParse(op1, out int operant1);
            if (!res1)
                operant1 = oldVal;

            var res2 = int.TryParse(op2, out int operant2);
            if (!res2)
                operant2 = oldVal;

            return op switch
            {
                '+' => operant1 + operant2,
                '*' => operant1 * operant2,
                _ => oldVal
            };
        }
    }
}