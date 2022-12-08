namespace Day8
{
    internal class Program
    {
        static List<List<int>> forest = new();

        static void Main(string[] args)
        {
            int visibleTrees = 0;
            int scenicScore = 0;

            try
            {
                using var fs = new FileStream(Path.GetFullPath("../../../Input/Forest.txt"), FileMode.Open, FileAccess.Read);
                using var sr = new StreamReader(fs);

                var line = sr.ReadLine();
                int counter = 0;

                while (line != null)
                {
                    forest.Add(new List<int>());
                    line.ToList().ForEach(c => forest[counter].Add(int.Parse(c.ToString())));

                    counter++;
                    line = sr.ReadLine();
                }

                visibleTrees = GetVisibleTrees();
                scenicScore = GetHighestScenicScore();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
                Console.WriteLine($"An error occured: {ex.StackTrace}");
            }
            finally
            {
                Console.WriteLine($"Number of visible trees: {visibleTrees}");
                Console.WriteLine($"Highest Scenic Score: {scenicScore}");
                Console.ReadLine();
            }
        }

        static int GetVisibleTrees()
        {
            int countVisible = 0;
            int countInvisible = 0;

            for (int i = 0; i < forest.Count; i++)
            {
                for (int j = 0; j < forest[i].Count; j++)
                {
                    if (IsInvisibleOnLeft(i, j) & IsInvisibleOnTop(i, j) & IsInvisibleOnRight(i, j) & IsInvisibleOnBottom(i, j))
                        countInvisible++;
                    else
                        countVisible++;
                }
            }

            Console.WriteLine($"Visible: {countVisible}");
            Console.WriteLine($"Total: {countInvisible + countVisible}");

            return countVisible;
        }

        static bool IsInvisibleOnBottom(int i, int j)
        {
            if (i == forest.Count - 1)
                return false;

            int tree = forest[i][j];

            for (int b = i + 1; b < forest.Count; b++)
            {
                if (tree <= forest[b][j])
                    return true;
            }

            return false;
        }

        static bool IsInvisibleOnRight(int i, int j)
        {
            if (j == forest[i].Count - 1)
                return false;

            int tree = forest[i][j];

            for (int r = j + 1; r < forest[i].Count; r++)
            {
                if (tree <= forest[i][r])
                    return true;
            }

            return false;
        }

        static bool IsInvisibleOnTop(int i, int j)
        {
            if (i == 0)
                return false;

            int tree = forest[i][j];

            for (int t = i - 1; t >= 0; t--)
            {
                if (tree <= forest[t][j])
                    return true;
            }

            return false;
        }

        static bool IsInvisibleOnLeft(int i, int j)
        {
            if (j == 0)
                return false;

            int tree = forest[i][j];

            for (int l = j - 1; l >= 0; l--)
            {
                if (tree <= forest[i][l])
                    return true;
            }

            return false;
        }

        static int GetHighestScenicScore()
        {
            List<int> scores = new();

            for (int i = 0; i < forest.Count; i++)
            {
                for (int j = 0; j < forest[i].Count; j++)
                {
                    scores.Add(LookLeft(i, j) * LookTop(i, j) * LookRight(i, j) * LookBottom(i, j));
                }
            }

            return scores.Max();
        }

        static int LookBottom(int i, int j)
        {
            int treeHeight = forest[j][i];
            var dVec = new List<int>();

            for (int y = i + 1; y < forest.Count; y++)
            {
                dVec.Add(forest[j][y]);
            }

            var d = dVec.TakeWhile(f => f < treeHeight).Count();
            if (d != dVec.Count) d++;

            return d;
        }

        static int LookRight(int i, int j)
        {
            int treeHeight = forest[j][i];
            var right = new List<int>();

            for (int x = j + 1; x < forest[i].Count; x++)
            {
                right.Add(forest[x][i]);
            }

            var r = right.TakeWhile(f => f < treeHeight).Count();
            if (r != right.Count) r++;

            return r;
        }

        static int LookTop(int i, int j)
        {
            int treeHeight = forest[j][i];
            var up = new List<int>();

            for (int y = i - 1; y >= 0; y--)
            {
                up.Add(forest[j][y]);
            }

            var u = up.TakeWhile(f => f < treeHeight).Count();
            if (u != up.Count) u++;

            return u;
        }

        static int LookLeft(int i, int j)
        {
            int treeHeight = forest[j][i];
            var left = new List<int>();

            for (int x = j - 1; x >= 0; x--)
            {
                left.Add(forest[x][i]);
            }

            var l = left.TakeWhile(f => f < treeHeight).Count();
            if (l != left.Count) l++;

            return l;
        }

        private static bool TreeIsNotHidden(int currentTree, int oi, int oj, int ni, int nj)
        {
            for (int i = oi; i <= ni; i++)
            {
                for (int j = oj; j <= nj; j++)
                {
                    if (forest[i][j] >= currentTree)
                        return true;
                }
            }

            return false;
        }
    }
}