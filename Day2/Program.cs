using System.ComponentModel;

namespace Day2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var scoreStretegyOne = 0;
            var scoreStretegyTwo = 0;

            try
            {
                using var fs = new FileStream(Path.GetFullPath("../../../Input/StrategyGuide.txt"), FileMode.Open, FileAccess.Read);
                using var reader = new StreamReader(fs);

                var round = reader.ReadLine();

                while (round != null)
                {
                    int opponent = round[0] - 65;
                    int me = round[2] - 88;

                    // Part 1
                    scoreStretegyOne += me + 1;
                    scoreStretegyOne += GetRoundPoints(opponent, me) * 3;

                    // Part2
                    var chosenAction = GetChosenAction(opponent, me);
                    scoreStretegyTwo += chosenAction + 1;
                    scoreStretegyTwo += GetRoundPoints(opponent, chosenAction) * 3;

                    round = reader.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occured: {ex.Message}");
            }
            finally
            {
                Console.WriteLine($"Your score using strategy one is: {scoreStretegyOne}");
                Console.WriteLine($"Your score using strategy two is: {scoreStretegyTwo}");
                Console.ReadLine();
            }
        }

        static int GetRoundPoints(int opponent, int me)
        {
            if (opponent == me)
                return 1;
            if (Math.Abs(opponent - me) == 1 && opponent < me)
                return 2;
            if (Math.Abs(opponent - me) == 2 && opponent > me)
                return 2;
            return 0;
        }

        static int GetChosenAction(int opponent, int me)
        {
            if (opponent == 0)
                return (me + 2) % 3;
            if (opponent == 1)
                return me;
            if (opponent == 2)
                return (me + 4) % 3;
            return 0;
        }
    }
}
