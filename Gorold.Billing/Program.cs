using Azure.Identity;
using Gorold.Common.MassTransit;
using Gorold.Common.MongoDb;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        //configure another configuration source
        builder.Host.ConfigureAppConfiguration((context, configurationBuilder) =>
        {
            //we only want to use Azure Key Vault when we are running in production
            if (context.HostingEnvironment.IsProduction())
            {
                configurationBuilder.AddAzureKeyVault(
                    new Uri("https://gorold.vault.azure.net/"),
                    //Azure.Identity will fill the best way to fill the credentials based on the environment it's based
                    //in production, it will use the kubernetes context to fill in those credentials
                    new DefaultAzureCredential()
                );
            }
        });

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

