using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChatMessageSentimentProcessFunction31
{
    public static class ProcessChatMessage
    {
        private static HttpClient _sentimentClient;
        private static HttpClient _intentClient;
        private static string _chatTopic;
        private static string _textAnalyticsBaseUrl;
        private static string _textAnalyticsAccountKey;

        private static string _luisBaseUrl;
        private static string _luisAppId;
        private static string _luisPredictionKey;


        // Binding the trigger and outputs using the host.  Avoiding the need to create client configuration code.
        [FunctionName("ProcessChatMessage")]
        public static async Task Run(
            [EventHubTrigger("%SourceEventHubName%", Connection = "EventHubConnectionString")]EventData[] messages,
            [EventHub("%DestinationEventHubName%", Connection = "EventHubConnectionString")] EventHubClient archiveEventHubClient,
            [ServiceBus("%ChatTopicPath%", Connection = "ServiceBusConnectionString")] MessageSender topicClient,
            ILogger log)
        {
            
            log.LogInformation("EventHubTrigger was called.");
            InitEnvironmentVars();

            if (topicClient == null || topicClient.IsClosedOrClosing)
            {
                log.LogInformation("topicClient == null || topicClient.IsClosedOrClosing");
                topicClient = new MessageSender("ServiceBusConnectionString", _chatTopic);
            }

            // Reuse the HttpClient across calls as much as possible so as not to exhaust all available sockets on the server on which it runs.
            _sentimentClient = _sentimentClient ?? new HttpClient();
            _intentClient = _intentClient ?? new HttpClient();

            //TODO: 7.Configure the HTTPClient base URL and request headers
            _sentimentClient.DefaultRequestHeaders.Clear();
            _sentimentClient.DefaultRequestHeaders.Accept.Clear();
            _sentimentClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _textAnalyticsAccountKey);
            _sentimentClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            foreach (var eventData in messages)
            {
                try
                {
                    //TODO: 1.Extract the JSON payload from the binary message
                    string sourceEventHubEventBody = Encoding.UTF8.GetString(eventData.Body);
                    var sentimentMessage = JsonConvert.DeserializeObject<MessageType>(sourceEventHubEventBody);

                    //TODO: 7 Append sentiment score to chat message object
                    if (sentimentMessage.messageType.Equals("chat", StringComparison.OrdinalIgnoreCase))
                    {
                        sentimentMessage.score = await GetSentimentScore(sentimentMessage);
                        log.LogInformation("SentimentScore: " + sentimentMessage.score);
                    }

                    //TODO: 3.Create a Message (for Service Bus) and EventData instance (for EventHubs) from source message body
                    var updatedMessage = JsonConvert.SerializeObject(sentimentMessage);
                    var chatMessage  = new Message(Encoding.UTF8.GetBytes(updatedMessage));

                    // Write the body of the message to the console.
                    log.LogInformation($"Sending message: {updatedMessage}");

                    //TODO: 4.Copy the message properties from source to the outgoing message instances
                    foreach (var prop in eventData.Properties)
                    {
                        chatMessage.UserProperties.Add(prop.Key, prop.Value);
                    }

                    //TODO: 5.Send chat message to Topic
                    // Send the message to the topic which will be eventually picked up by ChatHub.cs in the web app.
                    await topicClient.SendAsync(chatMessage);

                    //TODO: 6.Send chat message to next EventHub (for archival)
                    using var eventBatch = archiveEventHubClient.CreateBatch();
                    EventData updatedEventData = new EventData(Encoding.UTF8.GetBytes(updatedMessage));

                    eventBatch.TryAdd(updatedEventData);
                    await archiveEventHubClient.SendAsync(eventBatch);
                    log.LogInformation("Forwarded message to event hub.");

                    //TODO: 8.Respond to chat message intent if appropriate
                    var updatedMessageObject = JsonConvert.DeserializeObject<MessageType>(updatedMessage);

                    // Get your most likely intent based on your message.
                    var intent = await GetIntentAndEntities(updatedMessageObject.message);
                    await HandleIntent(intent, updatedMessageObject, topicClient);
                }
                catch (Exception ex)
                {
                    // Need to include the stack trace so you can see the specific details of where the error is occuring.
                    log.LogError(ex.StackTrace);
                    log.LogError("Chat message processor encountered error while processing", ex);

                    // Need to rethrow the exception so the Azure Function logs show a failure.
                    throw ex;
                }
            }

        }



        private static void InitEnvironmentVars()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                // For local development
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            _chatTopic = config["ChatTopicPath"];

            _textAnalyticsAccountKey = config["TextAnalyticsAccountKey"];
            _textAnalyticsBaseUrl = config["TextAnalyticsBaseUrl"];

            _luisBaseUrl = config["LuisBaseUrl"];
            _luisAppId = config["LuisAppId"];
            _luisPredictionKey = config["LuisPredictionKey"];
        }



        private static async Task<double> GetSentimentScore(MessageType chatMessage)
        {
            if (!chatMessage.messageType.Equals("chat")) return -1;
            double sentimentScore = -1;

            //Construct a sentiment request object 
            var req = new SentimentRequest()
            {
                documents = new SentimentDocument[]
                {
                    new SentimentDocument() { id = "1", text = chatMessage.message, language = "en" }
                }
            };

            //Serialize the request object to a JSON encoded in a byte array
            var jsonReq = JsonConvert.SerializeObject(req);
            byte[] byteData = Encoding.UTF8.GetBytes(jsonReq);

            //Post the request to the /sentiment endpoint
            string uri = $"{_textAnalyticsBaseUrl}/text/analytics/v2.1/sentiment";
            string jsonResponse = "";

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var sentimentResponse = await _sentimentClient.PostAsync(uri, content);
                jsonResponse = await sentimentResponse.Content.ReadAsStringAsync();
            }

            var result = JsonConvert.DeserializeObject<SentimentResponse>(jsonResponse);
            sentimentScore = result.documents[0].score;

            return sentimentScore;
        }





        private static async Task HandleIntent(PredictionResponse predictionResponse, MessageType msgObj, MessageSender outputServiceBus)
        {
           if (!msgObj.messageType.Equals("chat")) return;
           var topIntent = predictionResponse.Prediction.TopIntent;
            
           if (topIntent != null && topIntent.Equals("OrderIn"))
            {
                // What service do you need delivered to your room.
                if (predictionResponse.Prediction.Intents["OrderIn"].Score > 0.75)
                {
                   string destination = String.Empty;

                   foreach (var entity in predictionResponse.Prediction.Entities)
                    {
                        destination = entity.Key;
                        break;
                    }

                    //Detected an actionable request with an identified entity.
                    if (!destination.Equals(String.Empty))
                    {
                        var generatedMessage =
                            $"We've sent your message '{msgObj.message}' to {destination}, and they will confirm it shortly.";
                        await SendBotMessage(msgObj, generatedMessage, outputServiceBus);
                    }
                }
            }
        }


        private static async Task SendBotMessage(MessageType msgObj, string generatedMessage, MessageSender outputServiceBus)
        {
            MessageType generatedMsg = new MessageType()
            {
                createDate = DateTime.UtcNow,
                message = generatedMessage,
                messageId = Guid.NewGuid().ToString(),
                score = 0.5,
                sessionId = msgObj.sessionId,
                userName = "ConciergeBot",
                messageType = "bot"

            };

            var generatedMessageBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(generatedMsg));
            var botMessage = new Message(generatedMessageBytes);
            botMessage.UserProperties.Add("SessionId", msgObj.sessionId);
            await outputServiceBus.SendAsync(botMessage);
        }


        private static async Task<PredictionResponse> GetIntentAndEntities(string messageText)
        {
            var credentials = new ApiKeyServiceClientCredentials(_luisPredictionKey);
            var luisClient = new LUISRuntimeClient(credentials, new System.Net.Http.DelegatingHandler[] { })
            {
                Endpoint = _luisBaseUrl
            };

            var requestOptions = new PredictionRequestOptions
            {
                DatetimeReference = DateTime.Parse("2019-01-01"),
                PreferExternalEntities = true
            };

            var predictionRequest = new PredictionRequest
            {
                Query = messageText,
                Options = requestOptions
            };

            // get prediction
            return await luisClient.Prediction.GetSlotPredictionAsync(
                Guid.Parse(_luisAppId),
                slotName: "production",
                predictionRequest,
                verbose: true,
                showAllIntents: true,
                log: true);

        }


        #region Application Data Structures

        // This class has to match the JS client type, ChatHub message type, and function message type.
        class MessageType
        {
            public string message;
            public DateTime createDate;
            public string userName;
            public string sessionId;
            public string messageId;
            public string messageType;
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
            public string language;
        }

        #endregion
    }
}
