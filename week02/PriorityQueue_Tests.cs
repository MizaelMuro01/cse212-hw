using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Create a queue with items that have different priorities - run until empty
    // Items: ("ItemA", 7), ("ItemB", 3), ("ItemC", 10), ("ItemD", 1), ("ItemE", 5)
    //        ("ItemF", 8), ("ItemG", 2), ("ItemH", 9), ("ItemI", 4), ("ItemJ", 6)
    // Expected Result: [ItemC, ItemH, ItemF, ItemA, ItemJ, ItemE, ItemI, ItemB, ItemG, ItemD]
    // Defect(s) Found: 2
    //
    // - The Dequeue() method did not remove items from the queue
    //   - The problem: The highest priority item was selected and returned, but never removed
    //   - Solution: Added a Remove method that is called inside Dequeue() after finding the highest priority item
    //
    // - The loop that finds the highest priority item skipped some indexes
    //   - The problem: Loop started at index 1 and stopped at Count-1, missing index 0 and the last item
    //   - Solution: Changed "for (int index = 1; index < _queue.Count - 1; index++)" 
    //               to "for (int index = 0; index < _queue.Count; index++)"
    //
    public void TestPriorityQueue_DifferentPriorities()
    {
        var priorityQueue = new PriorityQueue();

        // add items to the queue - using values directly, not PriorityItem objects
        priorityQueue.Enqueue("ItemA", 7);
        priorityQueue.Enqueue("ItemB", 3);
        priorityQueue.Enqueue("ItemC", 10);
        priorityQueue.Enqueue("ItemD", 1);
        priorityQueue.Enqueue("ItemE", 5);
        priorityQueue.Enqueue("ItemF", 8);
        priorityQueue.Enqueue("ItemG", 2);
        priorityQueue.Enqueue("ItemH", 9);
        priorityQueue.Enqueue("ItemI", 4);
        priorityQueue.Enqueue("ItemJ", 6);

        string[] expectedResult = ["ItemC", "ItemH", "ItemF", "ItemA", "ItemJ", "ItemE", "ItemI", "ItemB", "ItemG", "ItemD"];

        int i = 0;

        while (priorityQueue.GetLength() > 0)
        {
            if (i >= expectedResult.Length)
            {
                Assert.Fail("Queue should have run out of items by now.");
            }

            // get the highest priority item's value from the queue
            var value = priorityQueue.Dequeue();
            Assert.AreEqual(expectedResult[i], value);
            i++;
        }
    }

    [TestMethod]
    // Scenario: Create a queue with items that share the same priority values
    // Items: ("ItemA", 10), ("ItemB", 5), ("ItemC", 10), ("ItemD", 10), ("ItemE", 9)
    //        ("ItemF", 10), ("ItemG", 3), ("ItemH", 2), ("ItemI", 9), ("ItemJ", 1)
    // Expected Result: [ItemA, ItemC, ItemD, ItemF, ItemI, ItemE, ItemB, ItemG, ItemH, ItemJ]
    // Defect(s) Found: 1
    //
    // - When multiple items had the same priority, the queue picked the last one checked instead of the first one
    //   - The problem: Dequeue() had no code to handle ties - it just updated when priority was greater
    //   - Solution: Added code to handle equal priorities - keep the one closer to the front (smaller index)
    //   - Code added:
    //     else if (_queue[index].Priority == _queue[highPriorityIndex].Priority)
    //     {
    //         if (index < highPriorityIndex)
    //             highPriorityIndex = index;
    //     }
    //                            
    public void TestPriorityQueue_SamePrioritiesFIFO()
    {
        var priorityQueue = new PriorityQueue();

        // add items to the queue - using values directly, not PriorityItem objects
        priorityQueue.Enqueue("ItemA", 10);
        priorityQueue.Enqueue("ItemB", 5);
        priorityQueue.Enqueue("ItemC", 10);
        priorityQueue.Enqueue("ItemD", 10);
        priorityQueue.Enqueue("ItemE", 9);
        priorityQueue.Enqueue("ItemF", 10);
        priorityQueue.Enqueue("ItemG", 3);
        priorityQueue.Enqueue("ItemH", 2);
        priorityQueue.Enqueue("ItemI", 9);
        priorityQueue.Enqueue("ItemJ", 1);

        string[] expectedResult = ["ItemA", "ItemC", "ItemD", "ItemF", "ItemI", "ItemE", "ItemB", "ItemG", "ItemH", "ItemJ"];

        int i = 0;

        while (priorityQueue.GetLength() > 0)
        {
            if (i >= expectedResult.Length)
            {
                Assert.Fail("Queue should have run out of items by now.");
            }

            // get the highest priority item's value from the queue
            var value = priorityQueue.Dequeue();
            Assert.AreEqual(expectedResult[i], value);
            i++;
        }
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue
    // Expected Result: InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: 0
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                string.Format("Unexpected exception of type {0} caught: {1}",
                e.GetType(), e.Message)
            );
        }
    }

    [TestMethod]
    // Scenario: Add items and verify that items with same priority come out in FIFO order
    // Expected Result: "First", then "Second", then "Third"
    // Defect(s) Found: 0 (already fixed in previous test)
    public void TestPriorityQueue_SimpleFIFO()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("First", 5);
        priorityQueue.Enqueue("Second", 5);
        priorityQueue.Enqueue("Third", 3);

        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("Third", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Mix of different priorities and same priorities
    // Expected Result: Highest priority first, then FIFO for ties
    // Defect(s) Found: 0 (all fixed)
    public void TestPriorityQueue_MixedPriorities()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High", 10);
        priorityQueue.Enqueue("Medium", 5);
        priorityQueue.Enqueue("AnotherHigh", 10);
        priorityQueue.Enqueue("AnotherLow", 1);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("AnotherHigh", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
        Assert.AreEqual("AnotherLow", priorityQueue.Dequeue());
    }
}