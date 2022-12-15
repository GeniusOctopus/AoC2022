using System.Drawing;

namespace Day9
{
    internal class Program
    {
        static Point head1 = new(0, 0);
        static Point tail1 = new(0, 0);
        static List<Point> tailPositions1 = new() { tail1 };
        static Point head2 = new(0, 0);
        static List<Point> tail2 = new();
        static List<Point> tailPositions2 = new();

        static void Main(string[] args)
        {
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
                    Move1(move.First(), int.Parse(move[2..]));
                    Move2(move.First(), int.Parse(move[2..]));

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

        static void Move1(char direction, int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                switch (direction)
                {
                    case 'U': head1 = new Point(head1.X, head1.Y + 1); break;
                    case 'L': head1 = new Point(head1.X - 1, head1.Y); break;
                    case 'D': head1 = new Point(head1.X, head1.Y - 1); break;
                    case 'R': head1 = new Point(head1.X + 1, head1.Y); break;
                    default: break;
                }

                if (Delta1())
                {
                    tail1 = GetPoint1(direction);
                    if (!tailPositions1.Contains(tail1))
                        tailPositions1.Add(tail1);
                }
            }
        }

        static void Move2(char direction, int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                switch (direction)
                {
                    case 'U': head2 = new Point(head2.X, head2.Y + 1); break;
                    case 'L': head2 = new Point(head2.X - 1, head2.Y); break;
                    case 'D': head2 = new Point(head2.X, head2.Y - 1); break;
                    case 'R': head2 = new Point(head2.X + 1, head2.Y); break;
                    default: break;
                }

                var headAux = head2;

                for (int j = 0; j < tail2.Count; j++)
                {
                    if (Delta2(j, headAux))
                    {
                        tail2[j] = GetPoint2(headAux, j);
                        if (!tailPositions2.Contains(tail2[^1]) && j == tail2.Count - 1)
                            tailPositions2.Add(tail2[^1]);
                    }
                    headAux = tail2[j];
                }
            }
        }

        static bool Delta1()
        {
            return (Math.Abs(head1.X - tail1.X) > 1 || Math.Abs(head1.Y - tail1.Y) > 1);
        }

        static bool Delta2(int i, Point head)
        {
            return (Math.Abs(head.X - tail2[i].X) > 1 || Math.Abs(head.Y - tail2[i].Y) > 1);
        }

        static Point GetPoint1(char direcion)
        {
            return direcion switch
            {
                'U' => new Point(head1.X, head1.Y - 1),
                'L' => new Point(head1.X + 1, head1.Y),
                'D' => new Point(head1.X, head1.Y + 1),
                'R' => new Point(head1.X - 1, head1.Y),
                _ => new Point(head1.X, head1.Y),
            };
        }

        static Point GetPoint2(Point head, int j)
        {
            // rechts
            if (head.X - tail2[j].X >= 1 && head.Y - tail2[j].Y == 0)
                return new Point(tail2[j].X + 1, tail2[j].Y);
            // links
            if (head.X - tail2[j].X <= -1 && head.Y - tail2[j].Y == 0)
                return new Point(tail2[j].X - 1, tail2[j].Y);
            // oben
            if (head.X - tail2[j].X == 0 && head.Y - tail2[j].Y >= 1)
                return new Point(tail2[j].X, tail2[j].Y + 1);
            // unten
            if (head.X - tail2[j].X == 0 && head.Y - tail2[j].Y <= -1)
                return new Point(tail2[j].X, tail2[j].Y - 1);
            // oben rechts
            if (head.X - tail2[j].X >= 1 && head.Y - tail2[j].Y >= 1)
                return new Point(tail2[j].X + 1, tail2[j].Y + 1);
            // oben links
            if (head.X - tail2[j].X <= -1 && head.Y - tail2[j].Y >= 1)
                return new Point(tail2[j].X - 1, tail2[j].Y + 1);
            // unten rechts
            if (head.X - tail2[j].X >= 1 && head.Y - tail2[j].Y <= -1)
                return new Point(tail2[j].X + 1, tail2[j].Y - 1);
            // unten links
            if (head.X - tail2[j].X <= -1 && head.Y - tail2[j].Y <= -1)
                return new Point(tail2[j].X - 1, tail2[j].Y - 1);


            return tail2[j];
            //return direcion switch
            //{
            //    'U' => new Point(head2.X, head2.Y - 1),
            //    'L' => new Point(head2.X + 1, head2.Y),
            //    'D' => new Point(head2.X, head2.Y + 1),
            //    'R' => new Point(head2.X - 1, head2.Y),
            //    _ => new Point(head2.X, head2.Y),
            //};
        }
    }
}