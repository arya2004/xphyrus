using EmailService.MessageBrokerListner;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EmailService.Extension
{
    public static class ApplicaioBuilderExtensions
    {
        private static IAzureServiceBus azureServiceBusConsumer { get; set; }
        public static IApplicationBuilder USeAzureServiceBusConsumer(this IApplicationBuilder app)
        {
            azureServiceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBus>();
            var hostAppLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostAppLife.ApplicationStarted.Register(OnStart);
            hostAppLife.ApplicationStopping.Register(OnStop);

            //return app, so doesnt hold pipeline
            return app;
        }

        private static void OnStop()
        {
            azureServiceBusConsumer.Stop();
        }

        private static void OnStart()
        {
            azureServiceBusConsumer.Start();
        }
    }
}
