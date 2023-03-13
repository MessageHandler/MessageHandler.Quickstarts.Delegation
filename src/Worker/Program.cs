using MessageHandler.Quickstart.Contract;
using MessageHandler.Runtime;
using MessageHandler.Runtime.AtomicProcessing;
using Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddLogging();

        services.AddPostmark();

        var serviceBusConnectionString = hostContext.Configuration.GetValue<string>("servicebusnamespace")
                                                ?? throw new Exception("No 'servicebusnamespace' was provided. Use User Secrets or specify via environment variable.");

        services.AddMessageHandler("emailsender", runtimeConfiguration =>
        {
            runtimeConfiguration.AtomicProcessingPipeline(pipeline =>
            {
                pipeline.PullMessagesFrom(p => p.Queue(name: "emails", serviceBusConnectionString));
                pipeline.DetectTypesInAssembly(typeof(NotifyBuyer).Assembly);
                pipeline.HandleMessagesWith<SendNotificationMail>();
            });
        });

    })
    .Build();

await host.RunAsync();