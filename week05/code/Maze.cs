// DO NOT MODIFY THIS FILE

public class Maze
{
    public int Width { get; }
    public int Height { get; }

    public readonly int[] Data;

    /// <summary>
    /// - saves the width, height, and maze data
    /// </summary>
    public Maze(int width, int height, int[] data)
    {
        this.Width = width;
        this.Height = height;
        this.Data = data;
    }

    /// <summary>
    /// #############
    /// # Problem 5 #
    /// #############
    /// 
    /// 0 = wall (you can't go through)
    /// 1 = open path (you can go through)
    /// 2 = end (you want to get here)
    /// 
    /// IsEnd and IsValidMove are already done.
    /// currPath is a list of positions (x,y) you already visited.
    /// 
    /// SolveMaze must return ALL paths to the end using recursion.
    /// </summary>
    /// <summary>
    /// - checks if (x,y) is the end
    /// - returns true if it's 2, false if not
    /// </summary>
    public bool IsEnd(int x, int y)
    {
        return Data[y * Height + x] == 2;
    }


    /// <summary>
    /// - checks if you can move to (x,y)
    /// - can't go outside the maze
    /// - can't go through walls (0)
    /// - can't revisit the same spot
    /// </summary>
    public bool IsValidMove(List<ValueTuple<int, int>> currPath, int x, int y)
    {
        // outside the maze?
        if (x > Width - 1 || x < 0)
            return false;
        if (y > Height - 1 || y < 0)
            return false;
        // is it a wall?
        if (Data[y * Height + x] == 0)
            return false;
        // already been here?
        if (currPath.Contains((x, y)))
            return false;
        // passed all checks, you can move
        return true;
    }
}