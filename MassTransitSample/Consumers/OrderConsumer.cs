using MassTransit;
using MassTransitSample.Messages;
using System;
using System.Threading.Tasks;

namespace MassTransitSample.Consumers
{
    public class OrderConsumer : IConsumer<Order>
    {
        public Task Consume(ConsumeContext<Order> context)
        {
            // Handle the order message
            var order = context.Message;

            Console.WriteLine($"Received order: {order.OrderId} for {order.ProductName}, quantity: {order.Quantity}");

            // Add your processing logic here

            return Task.CompletedTask;
        }
    }
}
