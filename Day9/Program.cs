using System.Drawing;

namespace Day9
{
    internal class Program
    {
        static Point head = new(0, 0);
        static Point tail = new(0, 0);
        static List<Point> tailPositions = new() { tail };

        static void Main(string[] args)
        {
            try
            {
                using var fs = new FileStream(Path.GetFullPath("../../../Input/Rope.txt"), FileMode.Open, FileAccess.Read);
                using var sr = new StreamReader(fs);

                var move = sr.ReadLine();

                while (move is not null)
                {
                    MoveDirection(move.First(), int.Parse(move[2..]));

                    move = sr.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }
            finally
            {
                Console.WriteLine($"positions: {tailPositions.Count}");
            }
        }

        static void MoveDirection(char direction, int steps)
        {
            switch (direction)
            {
                case 'U': MoveHead(direction, steps); break;
                case 'L': MoveHead(direction, steps); break;
                case 'D': MoveHead(direction, steps); break;
                case 'R': MoveHead(direction, steps); break;
                default: break;
            }
        }

        static void MoveHead(char direction, int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                switch (direction)
                {
                    case 'U': head = new Point(head.X, head.Y + 1); break;
                    case 'L': head = new Point(head.X - 1, head.Y); break;
                    case 'D': head = new Point(head.X, head.Y - 1); break;
                    case 'R': head = new Point(head.X + 1, head.Y); break;
                    default: break;
                }

                if (Delta())
                {
                    tail = GetPoint(direction);
                    if (!tailPositions.Contains(tail))
                        tailPositions.Add(tail);
                }
            }
        }

        static bool Delta()
        {
            return (Math.Abs(head.X - tail.X) > 1 || Math.Abs(head.Y - tail.Y) > 1);
        }

        static Point GetPoint(char direcion)
        {
            return direcion switch
            {
                'U' => new Point(head.X, head.Y - 1),
                'L' => new Point(head.X + 1, head.Y),
                'D' => new Point(head.X, head.Y + 1),
                'R' => new Point(head.X - 1, head.Y),
                _ => new Point(head.X, head.Y),
            };
        }
    }
}