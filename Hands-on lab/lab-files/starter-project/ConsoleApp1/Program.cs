using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Newtonsoft.Json;
using Azure.Storage.Blobs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Microsoft.Extensions.Logging;

namespace ConsoleApp1
{
    class Program
    {
        private const string connectionString = "Endpoint=sb://awhotel-events-namespaceth.servicebus.windows.net/;SharedAccessKeyName=ChatConsole;SharedAccessKey=nm73oeEUzWh80cKBDUY/aNDwODXs1zaTHmcztQd/n9g=";
        private const string eventHubName = "awchathub";
        private const string blobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=thawhotelchatstore;AccountKey=JP4iGyUZUfHdwzqPsyyPHUGBmSlmALJOHWhLTsstbdvceSzpBr/g9F8O+XicDOMpTvN6wxfIg5xgsvcsOJcEwg==;EndpointSuffix=core.windows.net";
        private const string blobContainerName = "checkpoints";


        static async Task Main()
        {
            SendEventHubMessage();
            //ReceiveEventHubMessages();

        }

        private static async Task SendEventHubMessage() {

            var producerClient = new EventHubProducerClient(connectionString, eventHubName);

            try
            {
                Message message = new Message();
                message.message = "Test";
                string testMessage = JsonConvert.SerializeObject(message);
                
                
                
                var eventData = new EventData(Encoding.UTF8.GetBytes(testMessage));
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


        private static async Task ReceiveEventHubMessages() {

            string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

            // Create a blob container client that the event processor will use 
            BlobContainerClient storageClient = new BlobContainerClient(blobStorageConnectionString, blobContainerName);

            // Create an event processor client to process events in the event hub
            EventProcessorClient processor = new EventProcessorClient(storageClient, consumerGroup, connectionString, eventHubName);

            // Register handlers for processing events and handling errors
            processor.ProcessEventAsync += ProcessEventHandler;
            processor.ProcessErrorAsync += ProcessErrorHandler;

            // Start the processing
            await processor.StartProcessingAsync();

            // Wait for 10 seconds for the events to be processed
            await Task.Delay(TimeSpan.FromSeconds(10));

            // Stop the processing
            await processor.StopProcessingAsync();

        }

        static async Task ProcessEventHandler(ProcessEventArgs eventArgs)
        {
            // Write the body of the event to the console window
            Console.WriteLine("\tRecevied event: {0}", Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray()));
            System.Diagnostics.Debug.WriteLine("Test");

            // Update checkpoint in the blob storage so that the app receives only new events the next time it's run
            await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
        }

        static Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
        {
            // Write details about the error to the console window
            Console.WriteLine($"\tPartition '{ eventArgs.PartitionId}': an unhandled exception was encountered. This was not expected to happen.");
            Console.WriteLine(eventArgs.Exception.Message);
            return Task.CompletedTask;
        }

        private class Message {

            public string message;
            public DateTime createDate = new DateTime();
            public string userName = "thenning";
            public string sessionId = Guid.NewGuid().ToString();
            public string messageId = Guid.NewGuid().ToString();
        }
    }
}
