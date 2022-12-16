using System.Drawing;

namespace Day9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var head1 = new Point(0, 0);
            var tail1 = new List<Point>() { new Point(0, 0) };
            var tailPositions1 = new List<Point>() { new Point(0, 0) };
            var head2 = new Point(0, 0);
            var tail2 = new List<Point>();
            var tailPositions2 = new List<Point>() { new Point(0, 0) };

            for (int i = 0; i < 9; i++)
                tail2.Add(new Point(0, 0));
            tailPositions2.Add(tail2[8]);

            try
            {
                using var fs = new FileStream(Path.GetFullPath("../../../Input/Rope.txt"), FileMode.Open, FileAccess.Read);
                using var sr = new StreamReader(fs);

                var move = sr.ReadLine();

                while (move is not null)
                {
                    var result1 = MoveInDirection(move.First(), int.Parse(move[2..]), head1, tail1, tailPositions1);
                    head1 = result1.Item1;
                    tail1 = result1.Item2;
                    tailPositions1 = result1.Item3;
                    var result2 = MoveInDirection(move.First(), int.Parse(move[2..]), head2, tail2, tailPositions2);
                    head2 = result2.Item1;
                    tail2 = result2.Item2;
                    tailPositions2 = result2.Item3;

                    move = sr.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.StackTrace}");
            }
            finally
            {
                Console.WriteLine($"positions: {tailPositions1.Count}");
                Console.WriteLine($"positions: {tailPositions2.Count}");
            }
        }

        static (Point, List<Point>, List<Point>) MoveInDirection(char direction, int steps, Point head, List<Point> tail, List<Point> tailPositions)
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

                var headAux = head;

                for (int j = 0; j < tail.Count; j++)
                {
                    if (IsHighDelta(headAux, tail[j]))
                    {
                        tail[j] = GetNewTailPosition(headAux, tail[j]);
                        if (!tailPositions.Contains(tail[^1]) && j == tail.Count - 1)
                            tailPositions.Add(tail[^1]);
                    }
                    headAux = tail[j];
                }
            }

            return (head, tail, tailPositions);
        }

        static bool IsHighDelta(Point head, Point tail)
        {
            return (Math.Abs(head.X - tail.X) > 1 || Math.Abs(head.Y - tail.Y) > 1);
        }

        static Point GetNewTailPosition(Point head, Point tail)
        {
            // rechts
            if (head.X - tail.X >= 1 && head.Y - tail.Y == 0)
                return new Point(tail.X + 1, tail.Y);
            // links
            if (head.X - tail.X <= -1 && head.Y - tail.Y == 0)
                return new Point(tail.X - 1, tail.Y);
            // oben
            if (head.X - tail.X == 0 && head.Y - tail.Y >= 1)
                return new Point(tail.X, tail.Y + 1);
            // unten
            if (head.X - tail.X == 0 && head.Y - tail.Y <= -1)
                return new Point(tail.X, tail.Y - 1);
            // oben rechts
            if (head.X - tail.X >= 1 && head.Y - tail.Y >= 1)
                return new Point(tail.X + 1, tail.Y + 1);
            // oben links
            if (head.X - tail.X <= -1 && head.Y - tail.Y >= 1)
                return new Point(tail.X - 1, tail.Y + 1);
            // unten rechts
            if (head.X - tail.X >= 1 && head.Y - tail.Y <= -1)
                return new Point(tail.X + 1, tail.Y - 1);
            // unten links
            if (head.X - tail.X <= -1 && head.Y - tail.Y <= -1)
                return new Point(tail.X - 1, tail.Y - 1);

            return tail;
        }
    }
}