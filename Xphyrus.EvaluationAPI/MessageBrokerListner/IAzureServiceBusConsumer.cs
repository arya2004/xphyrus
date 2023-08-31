namespace Xphyrus.EvaluationAPI.MessageBrokerListner
{
    public interface IAzureServiceBusConsumer
    {
        Task Start();
        Task Stop();
    }
}
