using System.Reflection;
using Gorold.Billing.Extensions;
using GreenPipes;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add RabbitMQ services to the container
builder.Services.AddMassTransit(configure =>
            {
                configure.AddConsumers(Assembly.GetEntryAssembly());
                configure.UsingGoroldRabbitMq(retry =>
                {
                    retry.Interval(3, TimeSpan.FromSeconds(5));
                });
            });
builder.Services.AddMassTransitHostedService();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
