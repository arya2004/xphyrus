namespace EmailService.MessageBrokerListner
{
    public interface IAzureServiceBus
    {
        Task Start();
        Task Stop();
    }
}
