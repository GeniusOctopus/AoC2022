namespace Day6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var signal = File.ReadAllText(Path.GetFullPath("../../../Input/Signal.txt")).ToList();

                for (int i = 0; i < signal.Count; i++)
                {
                    if (signal.GetRange(i, 4).Distinct().ToList().Count == 4)
                    {
                        Console.WriteLine($"Packet received after {i + 4} characters.");
                        break;
                    }
                }

                for (int i = 0; i < signal.Count; i++)
                {
                    if (signal.GetRange(i, 14).Distinct().ToList().Count == 14)
                    {
                        Console.WriteLine($"Message received after {i + 14} characters.");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
