using MassTransit;
using MassTransitSample.Messages; // Ensure this matches the namespace where Order is defined
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MassTransitSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public OrdersController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            // Create a new order instance
            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                ProductName = request.ProductName,
                Quantity = request.Quantity
            };

            // Publish the order message to RabbitMQ
            await _publishEndpoint.Publish(order);

            // Return a response indicating success
            return Ok();
        }
    }

    // Data transfer object for incoming order requests
    public class CreateOrderRequest
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
    