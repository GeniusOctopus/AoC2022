using Day11.Models;

namespace Day11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var monkeys = new List<Monkey>();
            var monkeyBusiness = (ulong)0L;

            try
            {
                using var fs = new FileStream(Path.GetFullPath("../../../Input/monkeys.txt"), FileMode.Open, FileAccess.Read);
                using var sr = new StreamReader(fs);

                var line = sr.ReadLine();

                while (line is not null)
                {
                    if (line.Contains("Monkey"))
                    {
                        monkeys.Add(new Monkey() { Id = ulong.Parse(line.Replace(":", "").Last().ToString()) });
                    }
                    else if (line.Contains("Starting items"))
                    {
                        var items = line[(line.IndexOf(':') + 1)..].Replace(" ", "").Split(',').Select(x => ulong.Parse(x)).ToList();
                        monkeys.Last().Items = items;
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
                    }
                    else if (line.Contains("Test"))
                    {
                        monkeys.Last().Divisor = ulong.Parse(line.Replace("  Test: divisible by ", ""));
                    }
                    else if (line.Contains("If true"))
                    {
                        monkeys.Last().MonkeyIdIfTrue = ulong.Parse(line.Replace("    If true: throw to monkey ", ""));
                    }
                    else if (line.Contains("If false"))
                    {
                        monkeys.Last().MonkeyIdIfFalse = ulong.Parse(line.Replace("    If false: throw to monkey ", ""));
                    }

                    line = sr.ReadLine();
                }

                for (int i = 0; i < 10000; i++)
                {
                    foreach (var monkey in monkeys)
                    {
                        while (monkey.Items.Count > 0)
                        {
                            var worry = monkey.Items.First();
                            var newWorry = Operation(monkey.Operant1, monkey.Operant2, monkey.Operator, worry);
                            ulong mod = monkeys.Aggregate((ulong)1, (acc, m) => acc * m.Divisor);
                            // newWorry /= 3;
                            newWorry %= mod;
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
                monkeyBusiness = monkeys.Take(2).Aggregate((ulong)1L, (monk1, monk2) => monk1 * monk2.InspectedItemsCount);
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

        static ulong Operation(string op1, string op2, char op, ulong oldVal)
        {
            var res1 = ulong.TryParse(op1, out ulong operant1);
            if (!res1)
                operant1 = oldVal;

            var res2 = ulong.TryParse(op2, out ulong operant2);
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