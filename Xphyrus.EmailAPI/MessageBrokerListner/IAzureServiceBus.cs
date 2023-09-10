namespace Xphyrus.EmailAPI.MessageBrokerListner
{
    public interface IAzureServiceBus
    {
        Task Start();
        Task Stop();
    }
}
