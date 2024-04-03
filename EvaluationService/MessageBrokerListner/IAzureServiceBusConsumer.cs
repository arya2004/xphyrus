namespace EvaluationService.MessageBrokerListner
{
    public interface IAzureServiceBusConsumer
    {
        Task Start();
        Task Stop();
    }
}
