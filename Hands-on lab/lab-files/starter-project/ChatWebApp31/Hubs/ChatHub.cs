using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ChatWebApp31.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IConfiguration configuration;
        private readonly ILogger log;
        private readonly IHubContext<ChatHub> hubContext;
        static ISubscriptionClient subscriptionClient;
        static EventHubProducerClient producerClient;
        private readonly string serviceBusConnectionString;
        private readonly string topicName;
        private readonly string subscriptionName;
        private readonly string connectionString;
        private readonly string eventHubName;

        public ChatHub(IConfiguration configuration, ILogger<ChatHub> log, IHubContext<ChatHub> hubContext) {
            this.configuration = configuration;
            this.log = log;

            // Hubs are short lived and will be disposed by the time you try to make a call. It is better to inject into the class.
            this.hubContext = hubContext;

            serviceBusConnectionString = configuration["ServiceBusConnectionString"];
            topicName = configuration["ChatTopic"];
            subscriptionName = configuration["ChatMessageSubscriptionName"];

            connectionString = configuration["EventHubConnectionString"];
            eventHubName = configuration["SourceEventHubName"];
        }



        public async Task SendMessage(string serializedMessage)
        {

            if (producerClient == null || producerClient.IsClosed)
            {
                // Create and check the Event Hub client.
                producerClient = new EventHubProducerClient(connectionString, eventHubName);
            }

            try
            {
                var message = JsonConvert.DeserializeObject<Message>(
                    serializedMessage,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
                    );

                message.createDate = DateTime.Now;
                if (String.IsNullOrEmpty(message.message)) throw new ArgumentNullException();
                var updatedMessage = JsonConvert.SerializeObject(message );
                var eventData = new EventData(Encoding.UTF8.GetBytes(updatedMessage));
                using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();
                eventBatch.TryAdd(eventData);
                await producerClient.SendAsync(eventBatch);
                Console.WriteLine("Sent message");
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                await producerClient.CloseAsync();
            }

        }


        public void RecieveMessage() {

            log.LogInformation("RecieveMessage called.");

            try
            {
                // ServiceBus Subscription
                if (subscriptionClient == null || subscriptionClient.IsClosedOrClosing)
                {
                    subscriptionClient = new SubscriptionClient(serviceBusConnectionString, topicName, subscriptionName, ReceiveMode.PeekLock);
                }
             
                subscriptionClient.RegisterMessageHandler(
                    async (message, token) =>
                    {
                        var messageJson = Encoding.UTF8.GetString(message.Body);
                        log.LogInformation($"Message was picked up by ChatHub.ReceiveMessage method. Body: {messageJson}");

                        // Send the ReceiveMessage event back to the browser client.
                        await hubContext.Clients.All.SendAsync("ReceiveMessage", messageJson);
                        await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                    },
                    new MessageHandlerOptions(async args => Console.WriteLine(args.Exception))
                    { MaxConcurrentCalls = 1, AutoComplete = false });


            }
            catch (Exception e)
            {
                log.LogError("Exception: " + e.Message);
            }

        }


        public override async Task OnConnectedAsync() 
        {
            // Send the HubConnected event back to the browser client.
            await Clients.All.SendAsync("HubConnected", "You are connected to the chat hub.");
        }


        // This class has to match the JS client type, ChatHub message type, and function message type.
        private class Message
        {

            public string message;
            public DateTime createDate = new DateTime();
            public string userName = String.Empty;
            public string sessionId = String.Empty;
            public string messageId = Guid.NewGuid().ToString();
            public string messageType = String.Empty;
            public double score = -1.0;
        }
    }
}
