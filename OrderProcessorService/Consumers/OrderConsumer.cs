using MassTransit;
using MassTransitSample.Messages;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace OrderProcessorService.Consumers
{
    public class OrderConsumer : IConsumer<Order>
    {
        private readonly ILogger<OrderConsumer> _logger;

        public OrderConsumer(ILogger<OrderConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<Order> context)
        {
            var order = context.Message;
            // Display a message or log the received order
            _logger.LogInformation($"Received order: {order.OrderId} for {order.ProductName}, quantity: {order.Quantity}");

            // You can also perform additional processing here if needed
            await Task.CompletedTask;
        }
    }
}
