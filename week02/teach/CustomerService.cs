/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService
{
    public static void Run()
    {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: Can I add one customer and then serve the customer?
        // Expected Result: This should display the customer that was added
        Console.WriteLine("Test 1");
        var cs = new CustomerService(4);
        cs.AddNewCustomer();
        cs.ServeCustomer();
        // Defect(s) Found: ServeCustomer was removing the customer before reading it; fixed order.

        Console.WriteLine("=================");

        // Test 2
        // Scenario: Can I add two customers and serve them in the correct order?
        // Expected Result: Customers should be displayed in the same order they were added (FIFO)
        Console.WriteLine("Test 2");
        cs = new CustomerService(4);
        cs.AddNewCustomer();
        cs.AddNewCustomer();
        Console.WriteLine($"Before serving customers: {cs}");
        cs.ServeCustomer();
        cs.ServeCustomer();
        Console.WriteLine($"After serving customers: {cs}");
        // Defect(s) Found: None after fixing Test 1 defect :)

        Console.WriteLine("=================");

        // Test 3
        // Scenario: Can I serve a customer when the queue is empty?
        // Expected Result: An error message should be displayed
        Console.WriteLine("Test 3");
        cs = new CustomerService(4);
        cs.ServeCustomer();
        // Defect(s) Found: No empty-queue check existed; added check and error message.

        Console.WriteLine("=================");

        // Test 4
        // Scenario: Does the max queue size get enforced?
        // Expected Result: An error message should appear when trying to add the 5th customer
        Console.WriteLine("Test 4");
        cs = new CustomerService(4);
        cs.AddNewCustomer();
        cs.AddNewCustomer();
        cs.AddNewCustomer();
        cs.AddNewCustomer();
        cs.AddNewCustomer(); // Should print: Maximum Number of Customers in Queue.
        Console.WriteLine($"Service Queue: {cs}");
        // Defect(s) Found: Used > instead of >= in the size check; allowed one too many customers.

        Console.WriteLine("=================");

        // Test 5
        // Scenario: Does the max size default to 10 when an invalid value is provided?
        // Expected Result: max_size should display as 10
        Console.WriteLine("Test 5");
        cs = new CustomerService(0);
        Console.WriteLine($"Size should be 10: {cs}");
        // Defect(s) Found: None :)
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize)
    {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer
    {
        public Customer(string name, string accountId, string problem)
        {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString()
        {
            return $"{Name} ({AccountId}): {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer()
    {
        // Verify there is room in the service queue
        // if (_queue.Count > _maxSize) // Defect - should use >=
        if (_queue.Count >= _maxSize)
        {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer()
    {
        // Need to check if there are customers in the queue
        if (_queue.Count <= 0) // Defect - need to check queue length
        {
            Console.WriteLine("No Customers in the queue");
        }
        else
        {
            // Read and save the customer before removing from the queue
            var customer = _queue[0];
            _queue.RemoveAt(0); // Defect - delete should happen after reading
            Console.WriteLine(customer);
        }
    }

    /// <summary>
    /// /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString()
    {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}