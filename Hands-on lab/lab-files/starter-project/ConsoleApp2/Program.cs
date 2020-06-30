using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Endpoint=sb://awhotel-events-namespaceth.servicebus.windows.net/;SharedAccessKeyName=ChatConsole;SharedAccessKey=nm73oeEUzWh80cKBDUY/aNDwODXs1zaTHmcztQd/n9g=";
            string eventHubName = "awchathub";

            EventHubClient eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, eventHubName);

            //var message = new
            //{
            //    message = "test",
            //    createDate = DateTime.UtcNow,
            //    username = "thenning",
            //    sessionId = "join",
            //    messageId = Guid.NewGuid().ToString()
            //};

            try
            {
                for (int x = 0; x < 6;x++) {
                    // Use an Event Hub sender, message includes session ID as a Property
                    var message = new
                    {
                        message = "test",
                        createDate = DateTime.UtcNow,
                        username = "thenning",
                        sessionId = "join",
                        messageId = Guid.NewGuid().ToString()
                    };

                    string jsonMessage = JsonConvert.SerializeObject(message);
                    EventData eventData = new EventData(Encoding.UTF8.GetBytes(jsonMessage));
                    eventData.Properties.Add("SessionId", "join");
                    // Optional: Provide a partitionKey value unique to each hotel. This will
                    // help ensure in-order processing of chat data. If you end up having more
                    // hotels than Event Hub partitions, that's ok. The data will be spread
                    // throughout all partitions, and the Azure Functions that are processing
                    // the data with EventProcessorHost will only process all of the events
                    // within the partition.
                    eventData.Properties.Add("partitionKey", "hotel-parkey");
                    eventHubClient.Send(eventData);
                    Console.WriteLine("Done");
                }
            }
            catch (Exception ex)
            {
                //TODO: Enable logging
                Console.WriteLine(ex.Message);
            }

        }
    }
}
