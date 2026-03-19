using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 1 - Run test cases and record any defects the test code finds in the comment above the test method.
// DO NOT MODIFY THE CODE IN THE TESTS in this file, just the comments above the tests. 
// Fix the code being tested to match requirements and make all tests pass. 

[TestClass]
public class TakingTurnsQueueTests
{
    [TestMethod]
    // Scenario: Create a queue with Bob (2 turns), Tim (5 turns), Sue (3 turns) and run until the queue is empty
    // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, Sue, Tim, Tim
    // Defect(s) Found: 1
    //
    // - The Enqueue method was always adding people to index 0, shifting the queue out of order
    // - The problem: public void Enqueue(Person person) => _queue.Insert(0, person);
    // - Solution: public void Enqueue(Person person) => _queue.Add(person);
    // 

    public void TestTakingTurnsQueue_FiniteRepetition()
    {
        var bob = new Person("Bob", 2);
        var tim = new Person("Tim", 5);
        var sue = new Person("Sue", 3);

        Person[] expectedResult = [bob, tim, sue, bob, tim, sue, tim, sue, tim, tim];

        var players = new TakingTurnsQueue();
        players.AddPerson(bob.Name, bob.Turns);
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        int i = 0;
        while (players.Length > 0)
        {
            if (i >= expectedResult.Length)
            {
                Assert.Fail("Queue should have ran out of items by now.");
            }

            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
            i++;
        }
    }

    [TestMethod]
    // Scenario: Create a queue with Bob (2 turns), Tim (5 turns), Sue (3 turns)
    // After running 5 times, add George with 3 turns - run until the queue is empty
    // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, George, Sue, Tim, George, Tim, George
    // Defect(s) Found: 0
    public void TestTakingTurnsQueue_AddPlayerMidway()
    {
        var bob = new Person("Bob", 2);
        var tim = new Person("Tim", 5);
        var sue = new Person("Sue", 3);
        var george = new Person("George", 3);

        Person[] expectedResult = [bob, tim, sue, bob, tim, sue, tim, george, sue, tim, george, tim, george];

        var players = new TakingTurnsQueue();
        players.AddPerson(bob.Name, bob.Turns);
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        int i = 0;
        for (; i < 5; i++)
        {
            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
        }

        players.AddPerson("George", 3);

        while (players.Length > 0)
        {
            if (i >= expectedResult.Length)
            {
                Assert.Fail("Queue should have ran out of items by now.");
            }

            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);

            i++;
        }
    }

    [TestMethod]
    // Scenario: Create a queue with Bob (2 turns), Tim (Forever - 0 turns), Sue (3 turns) - run 10 times
    // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, Sue, Tim, Tim
    // Defect(s) Found: 1
    //
    // - Having zero turns did not mean the player had infinite turns - they were treated as having no turns and removed
    // - The problem: The Person and TakingTurnsQueue classes had no way to tell finite turns from infinite turns
    // - Solution: Added a boolean attribute to Person that checks if the player starts with zero or negative turns
    // - Implementation in code:
    //   internal Person(string name, int turns)
    //   {
    //       Name = name;
    //       Turns = turns;
    //       // If turns is 0 or negative, they have infinite turns
    //       if (turns == 0 || turns < 0)
    //       {
    //           HasInfiniteTurns = true;
    //       }
    //       else
    //       {
    //           HasInfiniteTurns = false;
    //       }
    //   }
    //   
    //   TakingTurnsQueue => 
    //       else if (person.HasInfiniteTurns == true)
    //       {
    //           _people.Enqueue(person);
    //       }
    //
    public void TestTakingTurnsQueue_ForeverZero()
    {
        var timTurns = 0;

        var bob = new Person("Bob", 2);
        var tim = new Person("Tim", timTurns);
        var sue = new Person("Sue", 3);

        Person[] expectedResult = [bob, tim, sue, bob, tim, sue, tim, sue, tim, tim];

        var players = new TakingTurnsQueue();
        players.AddPerson(bob.Name, bob.Turns);
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        for (int i = 0; i < 10; i++)
        {
            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
        }

        // Verify that people with infinite turns really have infinite turns
        var infinitePerson = players.GetNextPerson();
        Assert.AreEqual(timTurns, infinitePerson.Turns, "People with infinite turns should not have their turns changed to a very big number - a very big number is not infinite");
    }

    [TestMethod]
    // Scenario: Create a queue with Tim (Forever - negative turns), Sue (3 turns) - run 10 times
    // Expected Result: Tim, Sue, Tim, Sue, Tim, Sue, Tim, Tim, Tim, Tim
    // Defect(s) Found: 0
    public void TestTakingTurnsQueue_ForeverNegative()
    {
        var timTurns = -3;
        var tim = new Person("Tim", timTurns);
        var sue = new Person("Sue", 3);

        Person[] expectedResult = [tim, sue, tim, sue, tim, sue, tim, tim, tim, tim];

        var players = new TakingTurnsQueue();
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        for (int i = 0; i < 10; i++)
        {
            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
        }

        // Verify that people with infinite turns really have infinite turns
        var infinitePerson = players.GetNextPerson();
        Assert.AreEqual(timTurns, infinitePerson.Turns, "People with infinite turns should not have their turns changed to a very big number - a very big number is not infinite");
    }

    [TestMethod]
    // Scenario: Try to get the next person from an empty queue
    // Expected Result: Exception should be thrown with appropriate error message
    // Defect(s) Found: 0
    public void TestTakingTurnsQueue_Empty()
    {
        var players = new TakingTurnsQueue();

        try
        {
            players.GetNextPerson();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("No one in the queue.", e.Message);
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
}