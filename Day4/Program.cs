namespace Day4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var countFullyContaining = 0;
            var countOverlapping = 0;

            try
            {
                using var fs = new FileStream(Path.GetFullPath("../../../Input/SectionAssignments.txt"), FileMode.Open, FileAccess.Read);
                using var sr = new StreamReader(fs);

                var pair = sr.ReadLine();

                while (pair != null)
                {
                    var bounds = pair.Split(new[] { '-', ',' });

                    var sections1 = Enumerable.Range(int.Parse(bounds[0]), int.Parse(bounds[1]) - int.Parse(bounds[0]) + 1).ToList();
                    var sections2 = Enumerable.Range(int.Parse(bounds[2]), int.Parse(bounds[3]) - int.Parse(bounds[2]) + 1).ToList();

                    if (!sections1.Except(sections2).Any() || !sections2.Except(sections1).Any())
                    {
                        countFullyContaining++;
                    }

                    if (sections1.Contains(sections2.First()) || sections1.Contains(sections2.Last()) || sections2.Contains(sections1.First()) || sections2.Contains(sections1.Last()))
                    {
                        countOverlapping++;
                    }

                    pair = sr.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }
            finally
            {
                Console.WriteLine($"Count of fully containing pairs: {countFullyContaining}");
                Console.WriteLine($"Count of overlapping pairs: {countOverlapping}");
                Console.ReadLine();
            }
        }
    }
}
