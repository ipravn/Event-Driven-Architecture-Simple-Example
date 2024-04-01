using System;

// Define an event args class to hold data associated with the event
public class OrderEventArgs : EventArgs
{
    public int OrderId { get; set; }
    public string ProductName { get; set; }
    public decimal TotalAmount { get; set; }
}

// Define a class that generates events
//
public class OrderProcessor
{
    // Declare an event using the EventHandler<T> delegate type
    public event EventHandler<OrderEventArgs> OrderProcessed;

    // Define a method that generates an event when an order is processed
    public void ProcessOrder(int orderId, string productName, decimal totalAmount)
    {
        // Perform processing logic here...

        // Raise the OrderProcessed event, passing in an instance of the OrderEventArgs class
        OrderProcessed?.Invoke(this, new OrderEventArgs { OrderId = orderId, ProductName = productName, TotalAmount = totalAmount });
    }
}

// Define a class that handles events
public class EmailSender
{
    // Define a method that handles the OrderProcessed event
    public void OnOrderProcessed(object sender, OrderEventArgs e)
    {
        // Send an email confirmation to the customer
        Console.WriteLine($"Email sent to customer for order {e.OrderId} containing {e.ProductName} for a total amount of {e.TotalAmount}");
    }
}

// Example usage
public class Program
{
    public static void Main()
    {
        // Create instances of the OrderProcessor and EmailSender classes
        var orderProcessor = new OrderProcessor();
        var emailSender = new EmailSender();

        // Subscribe to the OrderProcessed event using the OnOrderProcessed method
        orderProcessor.OrderProcessed += emailSender.OnOrderProcessed;

        // Process an order, which will trigger the OrderProcessed event
        orderProcessor.ProcessOrder(1234, "Widget", 99.99m);
    }
}
