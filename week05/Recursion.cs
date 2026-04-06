using System.Collections;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// - adds squares: 1^2 + 2^2 + 3^2 + ... + n^2
    /// - if n <= 0, return 0
    /// - no loops allowed, use recursion
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // TODO Start Problem 1
        return 0;
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// - creates all permutations of length 'size' from 'letters'
    /// - each letter is unique (no repeats to check)
    /// - saves results in the 'results' list
    /// - example:letters="ABC", size=2 gives AB, AC, BA, BC, CA, CB
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // TODO Start Problem 2
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// - counts ways to climb 's' stairs
    /// - can take 1, 2, or 3 steps at a time
    /// - formula: ways(s) = ways(s-1) + ways(s-2) + ways(s-3)
    /// - base cases: s=0→0, s=1→1, s=2→2, s=3→4
    /// - uses memoization (remember dictionary) for bigger numbers
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Base Cases
        if (s == 0)
            return 0;
        if (s == 1)
            return 1;
        if (s == 2)
            return 2;
        if (s == 3)
            return 4;

        // TODO Start Problem 3

        // Solve using recursion
        decimal ways = CountWaysToClimb(s - 1) + CountWaysToClimb(s - 2) + CountWaysToClimb(s - 3);
        return ways;
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// - * is a wildcard that can be 0 or 1
    /// - generates all possible binary strings from a pattern
    /// - example: "1*1" makes "101" and "111"
    /// - example: "1**1" makes 4 strings (1001, 1011, 1101, 1111)
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        // TODO Start Problem 4
    }

    /// <summary>
    /// - finds all paths from (0,0) to the end square in a maze
    /// - saves each complete path to 'results'
    /// - uses currPath to track where you've been
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // If this is the first time running the function, then we need
        // to initialize the currPath list.
        if (currPath == null) {
            currPath = new List<ValueTuple<int, int>>();
        }
        
        // currPath.Add((1,2)); // Use this syntax to add to the current path

        // TODO Start Problem 5
        // ADD CODE HERE

        // results.Add(currPath.AsString()); // Use this to add your path to the results array keeping track of complete maze solutions when you find the solution.
    }
}