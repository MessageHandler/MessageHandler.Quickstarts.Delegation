using MessageHandler.Quickstart.Contract;
using MessageHandler.Runtime;
using MessageHandler.Runtime.AtomicProcessing;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
                 .AddEnvironmentVariables()
                 .AddUserSecrets<Program>()
                 .Build();

// Add services to the container.

var serviceBusConnectionString = builder.Configuration.GetValue<string>("servicebusnamespace")
                               ?? throw new Exception("No 'servicebusnamespace' was provided. Use User Secrets or specify via environment variable.");


builder.Services.AddMessageHandler("orderbooking.worker", runtimeConfiguration =>
{
    runtimeConfiguration.ImmediateDispatchingPipeline(dispatching =>
    {
        dispatching.RouteMessagesOfType<NotifyBuyer>(to => to.Topic("emails", serviceBusConnectionString));
    });
});

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