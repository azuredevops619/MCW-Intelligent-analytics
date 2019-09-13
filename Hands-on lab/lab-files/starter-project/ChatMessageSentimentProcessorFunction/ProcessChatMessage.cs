using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace ChatMessageSentimentProcessorFunction
{
    public static class ProcessChatMessage
    {
        private static HttpClient _sentimentClient;
        private static HttpClient _intentClient;

        private static readonly string _textAnalyticsBaseUrl = ConfigurationManager.AppSettings["textAnalyticsBaseUrl"];
        private static readonly string _textAnalyticsAccountKey = ConfigurationManager.AppSettings["textAnalyticsAccountKey"];

        private static readonly string _luisBaseUrl = ConfigurationManager.AppSettings["luisBaseUrl"];
        private static readonly string _luisQueryParams = "luis/v2.0/apps/{0}?subscription-key={1}&q={2}";
        private static readonly string _luisAppId = ConfigurationManager.AppSettings["luisAppId"];
        private static readonly string _luisKey = ConfigurationManager.AppSettings["luisKey"];

        [FunctionName("ProcessChatMessage")]
        public static async Task Run([EventHubTrigger("%sourceEventHubName%",
                Connection = "eventHubConnectionString")]EventData[] messages,
            [EventHub("%destinationEventHubName%",
                Connection="eventHubConnectionString")]IAsyncCollector<EventData> outputEventHub,
            [ServiceBus("%chatTopicPath%",
                Connection = "serviceBusConnectionString")]IAsyncCollector<BrokeredMessage> outputServiceBus,
            TraceWriter log)
        {
            // Reuse the HttpClient across calls as much as possible so as not to exhaust all available sockets on the server on which it runs.
            _sentimentClient = _sentimentClient ?? new HttpClient();
            _intentClient = _intentClient ?? new HttpClient();
            //TODO: 7.Configure the HTTPClient base URL and request headers
            //_sentimentClient.DefaultRequestHeaders.Clear();
            //_sentimentClient.DefaultRequestHeaders.Accept.Clear();
            //_sentimentClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", /* Complete this with Key to Text API  */);
            //_sentimentClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            foreach (var eventData in messages)
            {
                try
                {
                    //TODO: 1.Extract the JSON payload from the binary message
                    //var eventBytes = //Complete this...
                    //var jsonMessage = Encoding.UTF8.GetString(//Complete this...);

                    //TODO: 2.Deserialize the JSON message payload into an instance of MessageType
                    //var msgObj = JsonConvert.DeserializeObject<MessageType>(//Complete this...);

                    //TODO: 12 Append sentiment score to chat message object
                    //Add a line here that invokes GetSentimentScore and sets the score on the msg object

                    //TODO: 3.Create a BrokeredMessage (for Service Bus) and EventData instance (for EventHubs) from source message body
                    //var updatedEventBytes = //Complete this...
                    //BrokeredMessage chatMessage = //Complete this...
                    //EventData updatedEventData = new EventData(//Complete this...);

                    //TODO: 4.Copy the message properties from source to the outgoing message instances
                    //foreach (var prop in eventData.Properties)
                    //{
                    //    chatMessage.Properties.Add(//Complete this...);
                    //    updatedEventData.Properties.Add(//Complete this...);
                    //}

                    //TODO: 5.Send chat message to Topic
                    //await outputServiceBus.AddAsync(//Complete this...);
                    //Console.WriteLine("Forwarded message to topic.");

                    //TODO: 6.Send chat message to next EventHub (for archival)
                    //await outputEventHub.AddAsync(//Complete this...);
                    //Console.WriteLine("Forwarded message to event hub.");

                    //TODO: 14.Respond to chat message intent if appropriate
                    //var intent = await GetIntentAndEntities(//Complete with the message object message property);
                    //await HandleIntent(//Complete with passing the intent, message object, and outputServiceBus collection);
                }
                catch (Exception ex)
                {
                    log.Error("Chat message processor encountered error while processing", ex);
                }
            }

            //TODO: 13. Perform a final flush to send all remaining events and messages in a batch.
            //await outputEventHub.FlushAsync();
            //await outputServiceBus.FlushAsync();
        }

        private static async Task<double> GetSentimentScore(string messageText)
        {
            double sentimentScore = -1;

            //TODO: 8.Construct a sentiment request object 
            //var req = new SentimentRequest()
            //{
            //    /* Complete this with a single document having id of 1 and text of the message */
            //};

            //TODO: 9.Serialize the request object to a JSON encoded in a byte array
            //var jsonReq = //Complete this...  
            //byte[] byteData = //Complete this...get byte array from jsonReq 

            //TODO: 10.Post the request to the /sentiment endpoint
            //string uri = $"{_textAnalyticsBaseUrl}/sentiment";
            //string jsonResponse = "";
            //using (var content = new ByteArrayContent(byteData))
            //{
            //    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //    var sentimentResponse = //Complete this... do a Post using the uri and content 
            //    jsonResponse = //Complete this...extract the response content as a string 
            //}
            //Console.WriteLine("\nDetect sentiment response:\n" + jsonResponse);

            //TODO: 11.Deserialize sentiment response and extract the score
            //var result = JsonConvert.DeserializeObject<SentimentResponse>(jsonResponse);
            //sentimentScore = //Complete this...retrieve the score for the first document in the result

            return sentimentScore;
        }

        private static async Task HandleIntent(LuisResponse intent, MessageType msgObj, IAsyncCollector<BrokeredMessage> outputServiceBus)
        {
            var primaryIntent = intent.topScoringIntent;
            var primaryEntity = intent.entities.FirstOrDefault();
            if (primaryIntent != null && primaryEntity != null)
            {
                if (primaryIntent.intent.Equals("OrderIn") && primaryIntent.score > 0.75)
                {
                    //Detected an actionable request with an identified entity
                    if (primaryEntity != null && primaryEntity.score > 0.5)
                    {
                        var destination = primaryEntity.type.Equals("RoomService::FoodItem") ? "Room Service" : "Housekeeping";
                        var generatedMessage =
                            $"We've sent your request for {primaryEntity.entity} to {destination}, we will confirm it shortly.";
                        await SendBotMessage(msgObj, generatedMessage, outputServiceBus);
                    }
                    else
                    {
                        //Detected only an actionable request, but no entity
                        var generatedMessage = "We've received your request for service, our staff will followup momentarily.";
                        await SendBotMessage(msgObj, generatedMessage, outputServiceBus);
                    }
                }
            }
        }

        private static async Task SendBotMessage(MessageType msgObj, string generatedMessage, IAsyncCollector<BrokeredMessage> outputServiceBus)
        {
            MessageType generatedMsg = new MessageType()
            {
                createDate = DateTime.UtcNow,
                message = generatedMessage,
                messageId = Guid.NewGuid().ToString(),
                score = 0.5,
                sessionId = msgObj.sessionId,
                username = "ConciergeBot"
            };
            var generatedMessageBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(generatedMsg));
            BrokeredMessage botMessage = new BrokeredMessage(generatedMessageBytes);
            botMessage.Properties.Add("SessionId", msgObj.sessionId);
            await outputServiceBus.AddAsync(botMessage);
            Console.WriteLine("Sent bot message to topic.");
        }


        private static async Task<LuisResponse> GetIntentAndEntities(string messageText)
        {
            LuisResponse result = null;

            string queryUri = string.Format(_luisQueryParams, _luisAppId, _luisKey, Uri.EscapeDataString(messageText));
            HttpResponseMessage response = await _intentClient.GetAsync($"{_luisBaseUrl}/{queryUri}");
            string res = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<LuisResponse>(res);

            Console.WriteLine("\nLUIS Response:\n" + res);

            return result;
        }

        #region Application Data Structures
        class MessageType
        {
            public string message;
            public DateTime createDate;
            public string username;
            public string sessionId;
            public string messageId;
            public double score;
        }

        //{"documents":[{"score":0.8010351,"id":"1"}],"errors":[]}
        class SentimentResponse
        {
            public SentimentResponseDocument[] documents;
            public string[] errors;
        }
        class SentimentResponseDocument
        {
            public double score;
            public string id;
        }

        class SentimentRequest
        {
            public SentimentDocument[] documents;
        }

        class SentimentDocument
        {
            public string id;
            public string text;
        }

        class LuisResponse
        {
            public string query;
            public Intent topScoringIntent;
            public Intent[] intents;
            public LuisEntity[] entities;
        }

        class Intent
        {
            public string intent;
            public double score;
        }

        class LuisEntity
        {
            public string entity;
            public string type;
            public int startIndex;
            public int endIndex;
            public double score;
        }

        #endregion
    }
}
