using MassTransit;
using MassTransitSample.Consumers;
using MassTransitSample.Messages; // Ensure this namespace is included if Order is in this namespace
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure MassTransit with RabbitMQ
builder.Services.AddMassTransit(x =>
{
    // Register the consumer
    x.AddConsumer<OrderConsumer>();

    // Configure MassTransit to use RabbitMQ
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        // Configure the receive endpoint
        cfg.ReceiveEndpoint("order-queue", e =>
        {
            e.ConfigureConsumer<OrderConsumer>(context);
        });
    });
});

// Register MassTransit hosted service
builder.Services.AddMassTransitHostedService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable developer exception page
    app.UseDeveloperExceptionPage();

    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();

    // Enable middleware to serve Swagger UI (HTML, JS, CSS, etc.),
    // specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MassTransitSample API v1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

app.UseAuthorization();
app.MapControllers();

app.Run();
