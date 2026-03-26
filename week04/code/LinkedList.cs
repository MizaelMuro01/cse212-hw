using System.Collections;

public class LinkedList : IEnumerable<int>
{
    private Node? _head;
    private Node? _tail;

    /// <summary>
    /// - insert a new node at the front (the head)
    /// - if list is empty - head and tail point to the new node
    /// - if not empty - connect new node to old head and update head
    /// </summary>
    public void InsertHead(int value)
    {
        Node newNode = new Node(value);

        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Next = _head;
            _head.Prev = newNode;
            _head = newNode;
        }
    }

    /// <summary>
    /// - insert a new node at the back (the tail)
    /// - if list is empty - head and tail point to the new node
    /// - if not empty - connect new node to old tail and update tail
    /// </summary>
    public void InsertTail(int value)
    {
        Node newNode = new Node(value);

        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Prev = _tail;
            _tail!.Next = newNode;
            _tail = newNode;
        }
    }

    /// <summary>
    /// - remove the first node (the head)
    /// - if only one node - set head and tail to null
    /// - if more than one - update head to the second node
    /// </summary>
    public void RemoveHead()
    {
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        else if (_head is not null)
        {
            _head.Next!.Prev = null;
            _head = _head.Next;
        }
    }

    /// <summary>
    /// - remove the last node (the tail)
    /// - if only one node - set head and tail to null
    /// - if more than one - update tail to the second last node
    /// </summary>
    public void RemoveTail()
    {
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        else if (_tail is not null)
        {
            _tail = _tail.Prev;
            _tail!.Next = null;
        }
    }

    /// <summary>
    /// - insert newValue after the first node that contains value
    /// - if value is at the tail - call InsertTail
    /// - otherwise - create new node and reconnect links
    /// </summary>
    public void InsertAfter(int value, int newValue)
    {
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                if (curr == _tail)
                {
                    InsertTail(newValue);
                }
                else
                {
                    Node newNode = new(newValue);
                    newNode.Prev = curr;
                    newNode.Next = curr.Next;
                    curr.Next!.Prev = newNode;
                    curr.Next = newNode;
                }
                return;
            }
            curr = curr.Next;
        }
    }

    /// <summary>
    /// - remove the first node that contains value
    /// - if node is head - call RemoveHead
    /// - if node is tail - call RemoveTail
    /// - if node is in the middle - connect neighbors and remove node
    /// - stop after removing one node
    /// </summary>
    public void Remove(int value)
    {
        Node? curr = _head;

        while (curr is not null)
        {
            if (curr.Data == value)
            {
                if (curr == _head)
                {
                    RemoveHead();
                }
                else if (curr == _tail)
                {
                    RemoveTail();
                }
                else
                {
                    curr.Prev!.Next = curr.Next;
                    curr.Next!.Prev = curr.Prev;
                }
                return;
            }
            curr = curr.Next;
        }
    }

    /// <summary>
    /// - replace all nodes that have oldValue with newValue
    /// - keep searching through the whole list
    /// - do not stop after first match
    /// </summary>
    public void Replace(int oldValue, int newValue)
    {
        Node? curr = _head;

        while (curr is not null)
        {
            if (curr.Data == oldValue)
            {
                curr.Data = newValue;
            }
            curr = curr.Next;
        }
    }

    /// <summary>
    /// - standard enumerator for foreach
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    /// <summary>
    /// - go forward from head to tail
    /// - yield each value one by one
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        var curr = _head;
        while (curr is not null)
        {
            yield return curr.Data;
            curr = curr.Next;
        }
    }

    /// <summary>
    /// - go backward from tail to head
    /// - each value one by one like the scripture
    /// </summary>
    public IEnumerable Reverse()
    {
        var curr = _tail;
        while (curr is not null)
        {
            yield return curr.Data;
            curr = curr.Prev;
        }
    }

    public override string ToString()
    {
        return "<LinkedList>{" + string.Join(", ", this) + "}";
    }

    // testing
    public Boolean HeadAndTailAreNull()
    {
        return _head is null && _tail is null;
    }

    // testing
    public Boolean HeadAndTailAreNotNull()
    {
        return _head is not null && _tail is not null;
    }
}

public static class IntArrayExtensionMethods
{
    public static string AsString(this IEnumerable array)
    {
        return "<IEnumerable>{" + string.Join(", ", array.Cast<int>()) + "}";
    }
}