using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items with different priorities and dequeue them all.
    // Expected Result: Items should come out in priority order: "high"(3), "mid"(2), "low"(1)
    // Defect(s) Found: 
    //   1. Loop used Count-1 so the last item was never checked for highest priority.
    //   2. Item was never removed after dequeue (RemoveAt was missing).
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("low", 1);
        priorityQueue.Enqueue("mid", 2);
        priorityQueue.Enqueue("high", 3);

        Assert.AreEqual("high", priorityQueue.Dequeue());
        Assert.AreEqual("mid", priorityQueue.Dequeue());
        Assert.AreEqual("low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue items where two have the same highest priority.
    // Expected Result: The one added FIRST should be dequeued first (FIFO tiebreaker).
    //                  Enqueue: A(5), B(5), C(1) → dequeue order: A, B, C
    // Defect(s) Found:
    //   1. Used >= in the priority comparison, causing the LAST highest-priority item
    //      to win instead of the FIRST — violating the FIFO tiebreaker requirement.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 5);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 1);

        Assert.AreEqual("A", priorityQueue.Dequeue()); // A added before B, same priority
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Dequeue from an empty queue.
    // Expected Result: InvalidOperationException thrown with message "The queue is empty."
    // Defect(s) Found: None - this part worked correctly.
    public void TestPriorityQueue_3()
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
    }

    [TestMethod]
    // Scenario: Enqueue one item and dequeue it. Queue should then be empty.
    // Expected Result: Dequeue returns the single item; second dequeue throws exception.
    // Defect(s) Found: 
    //   1. RemoveAt was missing so the item stayed in the queue after dequeue,
    //      meaning the queue never became empty.
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("only", 1);

        Assert.AreEqual("only", priorityQueue.Dequeue());

        // Queue should now be empty — next dequeue should throw
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }
}