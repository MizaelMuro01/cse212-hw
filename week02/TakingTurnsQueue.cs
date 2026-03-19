/// <summary>
/// This queue is circular - when people are added via AddPerson, they go to the back of the queue (FIFO rules)
/// When GetNextPerson is called, the next person in the queue is saved to be returned and then placed back at the back
/// each person stays in the queue and is given turns
/// When a person is added, a turns parameter shows how many turns they get
/// If turns is 0 or less, they stay in the queue forever
/// If a person has no turns left, they are not added back into the queue
/// </summary>
public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    /// <summary>
    /// Add new people to the queue with a name and number of turns
    /// </summary>
    /// <param name="name">Name of the person</param>
    /// <param name="turns">Number of turns remaining</param>
    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        _people.Enqueue(person);
    }

    /// <summary>
    /// Get the next person in the queue and return them
    /// The person should go to the back of the queue again unless they have no more turns left
    /// A turns value of 0 or less means the person has infinite turns
    /// An error exception is thrown if the queue is empty
    /// </summary>
    public Person GetNextPerson()
    {
        if (_people.IsEmpty())
        {
            throw new InvalidOperationException("No one in the queue.");
        }
        else
        {
            Person person = _people.Dequeue();
            
            // check if person should go back to the queue
            if (person.Turns > 1)
            {
                // they have turns left - remove one turn and back
                person.Turns -= 1;
                _people.Enqueue(person);
            }
            else if (person.HasInfiniteTurns == true)
            {
                // infinite turns - add back without changing turns
                _people.Enqueue(person);
            }
            // if turns =1, after using it they have 0 turns left - do not back

            return person;
        }
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}