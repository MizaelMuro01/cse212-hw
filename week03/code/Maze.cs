public static (int x, int y) MoveLeft(int x, int y, Dictionary<(int x, int y), (bool left, bool right, bool up, bool down)> maze)
{
    // Check if current position exists in maze
    if (!maze.ContainsKey((x, y)))
        return (x, y);
    
    // Check if can move left
    if (maze[(x, y)].left)
        return (x - 1, y);
    
    return (x, y);
}
