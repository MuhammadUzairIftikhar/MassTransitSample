using MassTransit;
using MassTransitSample.Messages;
using System;
using System.Threading.Tasks;

public class OrderConsumer : IConsumer<Order>
{
    public Task Consume(ConsumeContext<Order> context)
    {
        // Log the received message
        Console.WriteLine($"Received order: {context.Message.OrderId} for {context.Message.ProductName}, quantity: {context.Message.Quantity}");

        return Task.CompletedTask;
    }
}
