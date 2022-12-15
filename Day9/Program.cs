using System.Drawing;

namespace Day9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var head = new Point(0, 0);
            var tail = new Point(0, 0);
            var headPositions = new List<Point>() { head };
            var tailPositions = new List<Point>() { head };
            var up = 0;
            var right = 0;
            var down = 0;
            var left = 0;

            try
            {
                using var fs = new FileStream(Path.GetFullPath("../../../Input/Rope.txt"), FileMode.Open, FileAccess.Read);
                using var sr = new StreamReader(fs);

                var move = sr.ReadLine();

                while (move is not null)
                {
                    var mulm = move.Split(' ');
                    var direction = mulm[0];
                    var steps = int.Parse(mulm[1]);

                    switch (direction)
                    {
                        case "U":
                            up += steps;
                            for (int i = 0; i < steps; i++)
                            {
                                head = new Point(head.X, head.Y + 1);
                                if (!headPositions.Contains(head))
                                    headPositions.Add(head);

                                if (Math.Abs(head.X - tail.X) > 1 || Math.Abs(head.Y - tail.Y) > 1)
                                {
                                    tail = new Point(head.X, head.Y - 1);
                                    if (!tailPositions.Contains(tail))
                                        tailPositions.Add(tail);
                                }
                            }
                            break;
                        case "R":
                            right += steps;
                            for (int i = 0; i < steps; i++)
                            {
                                head = new Point(head.X + 1, head.Y);
                                if (!headPositions.Contains(head))
                                    headPositions.Add(head);

                                if (Math.Abs(head.X - tail.X) > 1 || Math.Abs(head.Y - tail.Y) > 1)
                                {
                                    tail = new Point(head.X - 1, head.Y);
                                    if (!tailPositions.Contains(tail))
                                        tailPositions.Add(tail);
                                }
                            }
                            break;
                        case "D":
                            down += steps;
                            for (int i = 0; i < steps; i++)
                            {
                                head = new Point(head.X, head.Y - 1);
                                if (!headPositions.Contains(head))
                                    headPositions.Add(head);

                                if (Math.Abs(head.X - tail.X) > 1 || Math.Abs(head.Y - tail.Y) > 1)
                                {
                                    tail = new Point(head.X, head.Y + 1);
                                    if (!tailPositions.Contains(tail))
                                        tailPositions.Add(tail);
                                }
                            }
                            break;
                        case "L":
                            left += steps;
                            for (int i = 0; i < steps; i++)
                            {
                                head = new Point(head.X - 1, head.Y);
                                if (!headPositions.Contains(head))
                                    headPositions.Add(head);

                                if (Math.Abs(head.X - tail.X) > 1 || Math.Abs(head.Y - tail.Y) > 1)
                                {
                                    tail = new Point(head.X + 1, head.Y);
                                    if (!tailPositions.Contains(tail))
                                        tailPositions.Add(tail);
                                }
                            }
                            break;
                    }

                    move = sr.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }
            finally
            {
                Console.WriteLine($"up: {up}");
                Console.WriteLine($"right: {right}");
                Console.WriteLine($"down: {down}");
                Console.WriteLine($"left: {left}");
                Console.WriteLine($"psoitions: {headPositions.Count}");
                Console.WriteLine($"psoitions: {tailPositions.Count}");
            }
        }
    }
}