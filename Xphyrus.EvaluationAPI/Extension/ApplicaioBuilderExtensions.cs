using Microsoft.CodeAnalysis.CSharp.Syntax;
using Xphyrus.EvaluationAPI.MessageBrokerListner;

namespace Xphyrus.EvaluationAPI.Extension
{
    public static class ApplicaioBuilderExtensions
    {
        private static IAzureServiceBusConsumer azureServiceBusConsumer { get; set; }
        public static IApplicationBuilder UseAzureServiceBusConsumer(this IApplicationBuilder app)
        {
            azureServiceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();
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
