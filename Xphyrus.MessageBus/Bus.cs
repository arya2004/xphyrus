using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xphyrus.MessageBus
{
    public class Bus : IBus
    {
        private string connectionString = "Endpoint=sb://xphyrus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=OQPAGWVB6OVT6JDdnVeKc05wgNbCkQ+pJ+ASbKarY7A=";
        public async Task PublishMessage(object message, string TopicQueueName)
        {
            await using var client = new ServiceBusClient(connectionString);

            ServiceBusSender sender = client.CreateSender(TopicQueueName);
            var jsonMessage = JsonConvert.SerializeObject(message);
            //give unique id
            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };

            //sned message
            await sender.SendMessageAsync(finalMessage);
            await client.DisposeAsync();
        }
    }
}
