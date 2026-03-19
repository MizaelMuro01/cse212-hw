using System.Diagnostics;

public class PriorityQueue
{
    private List<PriorityItem> _queue = new();

    /// <summary>
    /// Add a new value to the queue with an associated priority
    /// The node is always added to the back of the queue regardless of the priority
    /// </summary>
    /// <param name="value">The value</param>
    /// <param name="priority">The priority</param>
    public void Enqueue(string value, int priority)
    {
        var newNode = new PriorityItem(value, priority);
        _queue.Add(newNode);
    }

    private void Remove(int index)
    {
        _queue.RemoveAt(index);
    }

    public string Dequeue()
    {
        if (_queue.Count == 0) // Verify the queue is not empty
        {
            throw new InvalidOperationException("The queue is empty.");
        }

        // Find the index of the item with the highest priority to remove
        var highPriorityIndex = 0;
        for (int index = 0; index < _queue.Count; index++)
        {
            // if current item has higher priority, update highPriorityIndex
            if (_queue[index].Priority > _queue[highPriorityIndex].Priority)
            {
                highPriorityIndex = index;
            }
            // if priorities are equal, keep the one closer to the front (smaller index)
            else if (_queue[index].Priority == _queue[highPriorityIndex].Priority)
            {
                if (index < highPriorityIndex)
                {
                    highPriorityIndex = index;
                }
            }
        }

        // Save the value and remove the item from the queue
        var value = _queue[highPriorityIndex].Value;
        Remove(highPriorityIndex);
        return value;
    }

    public int GetLength()
    {
        return _queue.Count();
    }

    // DO NOT MODIFY THE CODE IN THIS METHOD
    // The graders rely on this method to check if you fixed all the bugs, so changes to it will cause you to lose points.
    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}

internal class PriorityItem
{
    internal string Value { get; set; }
    internal int Priority { get; set; }

    internal PriorityItem(string value, int priority)
    {
        Value = value;
        Priority = priority;
    }

    // DO NOT MODIFY THE CODE IN THIS METHOD
    // The graders rely on this method to check if you fixed all the bugs, so changes to it will cause you to lose points.
    public override string ToString()
    {
        return $"{Value} (Pri:{Priority})";
    }
}