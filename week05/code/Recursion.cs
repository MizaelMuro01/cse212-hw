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
        // Base case: if n <= 0, return 0
        if (n <= 0)
            return 0;
        
        // Recursive case: n^2 + sum of squares for (n-1)
        return (n * n) + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// - creates all permutations of length 'size' from 'letters'
    /// - each letter is unique (no repeats to check)
    /// - saves results in the 'results' list
    /// - example: letters="ABC", size=2 gives AB, AC, BA, BC, CA, CB
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // Base case: if word length equals size, add to results
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }
        
        // Recursive case: try each unused letter
        for (int i = 0; i < letters.Length; i++)
        {
            // Take the current letter and remove it from remaining letters
            char currentLetter = letters[i];
            string remainingLetters = letters.Substring(0, i) + letters.Substring(i + 1);
            
            // Recursive call with the new word and remaining letters
            PermutationsChoose(results, remainingLetters, size, word + currentLetter);
        }
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
        // Initialize memoization dictionary if this is the first call
        if (remember == null)
        {
            remember = new Dictionary<int, decimal>();
        }
        
        // Check if we already calculated this value
        if (remember.ContainsKey(s))
        {
            return remember[s];
        }
        
        // Base Cases
        decimal result;
        if (s == 0)
            result = 0;
        else if (s == 1)
            result = 1;
        else if (s == 2)
            result = 2;
        else if (s == 3)
            result = 4;
        else
        {
            // Solve using recursion with memoization
            result = CountWaysToClimb(s - 1, remember) + 
                     CountWaysToClimb(s - 2, remember) + 
                     CountWaysToClimb(s - 3, remember);
        }
        
        // Store result in memoization dictionary
        remember[s] = result;
        return result;
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
        // Base case: if pattern has no wildcards, add it to results
        int wildcardIndex = pattern.IndexOf('*');
        if (wildcardIndex == -1)
        {
            results.Add(pattern);
            return;
        }
        
        // Recursive case: replace first * with 0 and with 1
        string patternWith0 = pattern.Substring(0, wildcardIndex) + "0" + pattern.Substring(wildcardIndex + 1);
        string patternWith1 = pattern.Substring(0, wildcardIndex) + "1" + pattern.Substring(wildcardIndex + 1);
        
        WildcardBinary(patternWith0, results);
        WildcardBinary(patternWith1, results);
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
        if (currPath == null)
        {
            currPath = new List<ValueTuple<int, int>>();
        }
        
        // Add current position to path
        currPath.Add((x, y));
        
        // Check if we found the end
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
            currPath.RemoveAt(currPath.Count - 1); // Backtrack
            return;
        }
        
        // Try moving in all four directions: up, down, left, right
        // Order: right (x+1), down (y+1), left (x-1), up (y-1)
        int[] dx = { 1, 0, -1, 0 };
        int[] dy = { 0, 1, 0, -1 };
        
        for (int i = 0; i < 4; i++)
        {
            int newX = x + dx[i];
            int newY = y + dy[i];
            
            if (maze.IsValidMove(currPath, newX, newY))
            {
                SolveMaze(results, maze, newX, newY, currPath);
            }
        }
        
        // Backtrack: remove current position before returning
        currPath.RemoveAt(currPath.Count - 1);
    }
}