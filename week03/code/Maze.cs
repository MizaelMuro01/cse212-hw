using System;
using System.Collections.Generic;

class Maze
{
    public static (int x, int y) MoveLeft(int x, int y, Dictionary<(int x, int y), (bool left, bool right, bool up, bool down)> maze)
    {
        if (!maze.ContainsKey((x, y)))
            return (x, y);

        if (maze[(x, y)].left)
            return (x - 1, y);

        return (x, y);
    }

    public static (int x, int y) MoveRight(int x, int y, Dictionary<(int x, int y), (bool left, bool right, bool up, bool down)> maze)
    {
        if (!maze.ContainsKey((x, y)))
            return (x, y);

        if (maze[(x, y)].right)
            return (x + 1, y);

        return (x, y);
    }

    public static (int x, int y) MoveUp(int x, int y, Dictionary<(int x, int y), (bool left, bool right, bool up, bool down)> maze)
    {
        if (!maze.ContainsKey((x, y)))
            return (x, y);

        if (maze[(x, y)].up)
            return (x, y + 1);

        return (x, y);
    }

    public static (int x, int y) MoveDown(int x, int y, Dictionary<(int x, int y), (bool left, bool right, bool up, bool down)> maze)
    {
        if (!maze.ContainsKey((x, y)))
            return (x, y);

        if (maze[(x, y)].down)
            return (x, y - 1);

        return (x, y);
    }
}

// MoveLeft: go to x-1 if left is true
// MoveRight: go to x+1 if right is true
// MoveUp: go to y+1 if up is true
// MoveDown: go to y-1 if down is true
// If position not in maze or move not allowed, stay in same place