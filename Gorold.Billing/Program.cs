using Azure.Identity;
using Gorold.Common.Configurations;
using Gorold.Common.MassTransit;
using Gorold.Common.MongoDb;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        //configure another configuration source
        builder.Host.ConfigureAzureKeyVault();

        // Add RabbitMQ services to the container
        builder.Services.AddMassTransitWithMessageBroker(builder.Configuration);

        builder.Services.AddMongo();

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
    }
}

