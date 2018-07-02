![](../media/ms-cloud-workshop.png 'Microsoft Cloud Workshops')


Information in this document, including URL and other Internet Web site references, is subject to change without notice. Unless otherwise noted, the example companies, organizations, products, domain names, e-mail addresses, logos, people, places, and events depicted herein are fictitious, and no association with any real company, organization, product, domain name, e-mail address, logo, person, place or event is intended or should be inferred. Complying with all applicable copyright laws is the responsibility of the user. Without limiting the rights under copyright, no part of this document may be reproduced, stored in or introduced into a retrieval system, or transmitted in any form or by any means (electronic, mechanical, photocopying, recording, or otherwise), or for any purpose, without the express written permission of Microsoft Corporation.

Microsoft may have patents, patent applications, trademarks, copyrights, or other intellectual property rights covering subject matter in this document. Except as expressly provided in any written license agreement from Microsoft, the furnishing of this document does not give you any license to these patents, trademarks, copyrights, or other intellectual property.

The names of manufacturers, products, or URLs are provided for informational purposes only and Microsoft makes no representations and warranties, either expressed, implied, or statutory, regarding these manufacturers or the use of the products with any Microsoft technologies. The inclusion of a manufacturer or product does not imply endorsement of Microsoft of the manufacturer or product. Links may be provided to third party sites. Such sites are not under the control of Microsoft and Microsoft is not responsible for the contents of any linked site or any link contained in a linked site, or any changes or updates to such sites. Microsoft is not responsible for webcasting or any other form of transmission received from any linked site. Microsoft is providing these links to you only as a convenience, and the inclusion of any link does not imply endorsement of Microsoft of the site or the products contained therein.

© 2018 Microsoft Corporation. All rights reserved.

Microsoft and the trademarks listed at https://www.microsoft.com/en-us/legal/intellectualproperty/Trademarks/Usage/General.aspx are trademarks of the Microsoft group of companies. All other trademarks are property of their respective owners.

# Intelligent analytics hands-on lab unguided

Updated May 2018

AdventureWorks Travel specializes in building software solutions for the hospitality industry. Their latest product is an enterprise mobile/social chat product called Concierge+ (aka ConciergePlus). The mobile web app enables guests to easily stay in touch with the concierge and other guests, enabling greater personalization and improving their experience during their stay. Sentiment analysis is performed on top of chat messages as they occur, enabling hotel operators to keep tabs on guest sentiments in real-time.

If you have not yet completed the steps to set up your environment in [Before the hands-on lab](./Setup.md), you will need to do that before proceeding.

**Contents**

<!-- TOC -->

- [Intelligent analytics hands-on lab unguided](#intelligent-analytics-hands-on-lab-unguided)
  - [Exercise 1: Environment setup](#exercise-1-environment-setup)
    - [Task 1: Connect to the lab VM](#task-1-connect-to-the-lab-vm)
    - [Task 2: Download and open the ConciergePlus starter solution](#task-2-download-and-open-the-conciergeplus-starter-solution)
    - [Task 3: Create App Services](#task-3-create-app-services)
    - [Task 4: Provision Function App](#task-4-provision-function-app)
    - [Task 5: Provision Service Bus](#task-5-provision-service-bus)
    - [Task 6: Provision Event Hubs](#task-6-provision-event-hubs)
    - [Task 7: Provision Azure Cosmos DB](#task-7-provision-azure-cosmos-db)
    - [Task 8: Provision Azure Search](#task-8-provision-azure-search)
    - [Task 9: Create Stream Analytics job](#task-9-create-stream-analytics-job)
    - [Task 10: Start the Stream Analytics Job](#task-10-start-the-stream-analytics-job)
    - [Task 11: Provision an Azure Storage account](#task-11-provision-an-azure-storage-account)
    - [Task 12: Provision Cognitive Services](#task-12-provision-cognitive-services)
  - [Exercise 2: Implement message forwarding](#exercise-2-implement-message-forwarding)
    - [Task 1: Implement the event processor](#task-1-implement-the-event-processor)
    - [Task 2: Configure the Chat Message Processor Function App](#task-2-configure-the-chat-message-processor-function-app)
  - [Exercise 3: Configure the Chat Web App settings](#exercise-3-configure-the-chat-web-app-settings)
    - [Task 1: Configure the Chat Web App settings](#task-1-configure-the-chat-web-app-settings)
  - [Exercise 4: Deploying the App Services](#exercise-4-deploying-the-app-services)
    - [Task 1: Publish the ChatMessageSentimentProcessor Function App](#task-1-publish-the-chatmessagesentimentprocessor-function-app)
    - [Task 2: Publish the ChatWebApp](#task-2-publish-the-chatwebapp)
    - [Task 3: Testing hotel lobby chat](#task-3-testing-hotel-lobby-chat)
  - [Exercise 5: Add intelligence](#exercise-5-add-intelligence)
    - [Task 1: Implement sentiment analysis](#task-1-implement-sentiment-analysis)
    - [Task 2: Implement linguistic understanding](#task-2-implement-linguistic-understanding)
    - [Task 3: Implement speech to text](#task-3-implement-speech-to-text)
    - [Task 4: Re-deploy and test](#task-4-re-deploy-and-test)
  - [Exercise 6: Building the Power BI dashboard](#exercise-6-building-the-power-bi-dashboard)
    - [Task 1: Create the static dashboard](#task-1-create-the-static-dashboard)
    - [Task 2: Create the real-time dashboard](#task-2-create-the-real-time-dashboard)
    - [Task 3: Add a trending sentiment chart to the dashboard](#task-3-add-a-trending-sentiment-chart-to-the-dashboard)
  - [Exercise 7: Enabling search indexing](#exercise-7-enabling-search-indexing)
    - [Task 1: Verifying message archival](#task-1-verifying-message-archival)
    - [Task 2: Creating the index and indexer](#task-2-creating-the-index-and-indexer)
    - [Task 3: Update the Web App web.config](#task-3-update-the-web-app-webconfig)
    - [Task 4: Configure the Search API App](#task-4-configure-the-search-api-app)
    - [Task 5: Re-publish apps](#task-5-re-publish-apps)
  - [Exercise 8: Add a bot using Bot service and QnA Maker](#exercise-8-add-a-bot-using-bot-service-and-qna-maker)
    - [Task 1: Create a QnA service instance in Azure](#task-1-create-a-qna-service-instance-in-azure)
    - [Task 2: Create a QnA bot](#task-2-create-a-qna-bot)
  - [After the hands-on lab](#after-the-hands-on-lab)
    - [Task 1: Delete the resource group](#task-1-delete-the-resource-group)

<!-- /TOC -->

## Exercise 1: Environment setup

Duration: 60 minutes

Synopsis: The following section walks you through the manual steps to provision the services required using the Azure Portal. AdventureWorks has provided a starter solution for you. They have asked you to use this as the starting point for creating the Concierge Plus intelligent chat solution in Azure.

### Task 1: Connect to the lab VM

_Tasks to complete_:

1.  Create an RDP connection to your Lab VM

2.  Disable Internet Explorer Enhanced Security Configuration

_Exit criteria_:

- You have an active session to your Lab VM

### Task 2: Download and open the ConciergePlus starter solution

_Tasks to complete_:

1.  From your Lab VM, download the starter project by downloading a .zip copy of the Intelligent analytics GitHub repo

2.  In a web browser, navigate to the [Intelligent analytics MCW repo](https://github.com/Microsoft/MCW-Intelligent-analytics)

3.  On the repo page, select **Clone or download**, then select **Download ZIP**

    ![Download .zip containing the Intelligent analytics repository](media/git-hub-download-repo.png 'Download ZIP')

4.  Unzip the contents of the downloaded ZIP file to the folder **C:\\ConciergePlus**\\

    ![In the Extract Compressed (Zipped) Folders window, files will be extracted to C:\ConciergePlus.](media/image18.png 'Extract Compressed (Zipped) Folders window')

5.  Open **ConciergePlusSentiment.sln** in the C:\\ConciergePlus\\Hands-on-lab\\lab-files\\starter-project\\ folder with Visual Studio 2017

6.  Open the solution in Visual Studio 2017 on your Lab VM

_Exit criteria_:

- The ConciergePlusSentiments solution is open in Visual Studio on your Lab VM

**Note**: If you attempt to build the solution at this point, you will see many build errors. This is intentional. You will correct these in the exercises that follow.

### Task 3: Create App Services

In these steps, you will provision a Web App and an API App within a single App Service Plan.

_Tasks to complete_:

1.  Provision a Web App to host the website in an App Service Plan in the Resource Group "intelligent-analytics". Name the Web App something like "ConciergePlusWeb".

2.  Configure the Web App to enable WebSockets

3.  Provision an API App, adding it to the same resource group and App Service Plan/location. Name the API App something like "ChatSearchApi".

_Exit criteria_:

- You can navigate to the empty websites for deployed Web App and API App using a web browser

### Task 4: Provision Function App

In this section, you will provision a Function App that will be used as the EventProcessorHost for processing and enriching Event Hubs data.

_Tasks to complete_:

1.  Provision a Consumption Plan-based Function App

_Exit criteria_:

- You have a Function App within the same resource group as your other lab resources

### Task 5: Provision Service Bus

In this section, you will provision a Service Bus Namespace and Service Bus Topic.

_Tasks to complete_:

1.  Provision a Service Bus Topic in the same region/resource group as your App Services

2.  Provision an Event Hub in the same region/resource group as your App Services

_Exit criteria_:

- Your Service Bus Topic is listed in the Azure Portal

### Task 6: Provision Event Hubs

In this task, you will create a new Event Hubs namespace and instance.

_Tasks to complete_:

1.  Provision two Event Hub instances in a new namespace and in the same region/resource group as your App Services. Use a partition count of 32 and message retention of 1 day. The second Event Hub will store messages for archival and be processed by Stream Analytics.

_Exit criteria_:

- Your Event Hubs are listed in the Azure Portal

### Task 7: Provision Azure Cosmos DB

In this section, you will provision an Azure Cosmos DB account, a SQL (DocumentDB) Database, and a DocumentDB collection that will be used to collect all the chat messages.

_Tasks to complete_:

1.  Provision a new Azure Cosmos DB account in the same resource group and region as your other services

2.  Add SQL (DocumentDB) Database

3.  Add a Collection. The pricing tier should be left at Standard with the Throughput at 1000.

_Exit criteria_:

- You can view your Collection in the Azure Portal.

### Task 8: Provision Azure Search

In this section, you will create an Azure Search instance.

_Tasks to complete_:

1.  Provision a new instance of Azure Search in the same resource group and region as your other services, at the Basic Pricing tier

_Exit criteria_:

- You can view your Azure Search instance in the Azure Portal

### Task 9: Create Stream Analytics job

In this section, you will create the Stream Analytics Job that will be used to read chat messages from the Event Hub and write them to Azure Cosmos DB.

_Tasks to complete_:

1.  Provision a new Stream Analytics Job in the same region as your other resources

2.  Add an Input to it that reads from your second Event Hub (the one used for archival). The serialization should be JSON/UTF8.

3.  Add an Output to the Job that targets your Collection in DocumentDB. The Collection name pattern should match your Cosmos DB collection you created, and the Document ID should be messageid (all lower case).

4.  Create **two** Power BI outputs, one with a Dataset/Table name of Messages, and the other TrendingSentiment

5.  Add a Query that selects all data from the Event Hub and sends it to DocumentDB

6.  Add to the query to also select all into the Power BI output that contains the Messages dataset

7.  Finally, add to the query to select the average score and TimeStamp, grouped by a 5-minute tumbling window, into the Power BI output that contains the TrendingSentiment dataset

_Exit criteria_:

- You can view your Stream Analytics instance in the Azure Portal

### Task 10: Start the Stream Analytics Job

In this section, you will run the Stream Analytics Job that will be used to read chat messages from the Event Hub, and write them to Azure Cosmos DB.

_Tasks to complete_:

1.  Start the Stream Analytics Job

_Exit criteria_:

- Your Job starts without error

### Task 11: Provision an Azure Storage account

The EventProcessorHost requires an Azure Storage Account that it will use to manage its state among multiple instances. In this section, you create that Storage Account.

_Tasks to complete_:

1.  Provision a resource model based Storage Account of type Standard LRS in the same Location and Resource Group as your other services

_Exit criteria_:

- You can view your Storage Account in the Portal

### Task 12: Provision Cognitive Services

To provision access to the Text Analytics API (which provides sentiment analysis features), you will need to provision a Cognitive Services account

_Tasks to complete_:

1.  Provision a Text Analytics API in the same Location and Resource Group as your other services. Take note of the value of KEY 1

2.  Provision a Bing Speech API in the same Location and Resource Group as your other services. Take note of the value of KEY 1

3.  Provision a API Type Language Understanding Intelligent Service (LUIS) in the same Location and Resource Group as your other services. Take note of the value of KEY 1.

_Exit criteria_:

- You can view your Cognitive Services in the Portal, you should have one for Text Analytics API, another for Bing Speech API, and a third for LUIS

## Exercise 2: Implement message forwarding

Duration: 45 minutes

In this section, you will implement the message forwarding from the ingest Event Hub instance to an Event Hub instance and a Service Bus Topic. You will also configure the web-based components, which consist of three parts: The Web App UI, a Function App that runs the EventProcessorHost, and the API App that provides a wrapper around the Search API.

### Task 1: Implement the event processor

In this section, you will run the Stream Analytics Job that will be used to read chat messages from the Event Hub and write them to Cosmos DB.

_Tasks to complete_:

1.  In Visual Studio on your Lab VM, open ProcessChatMessage.cs within the ChatMessageSentimentProcessorFunction project

2.  Complete the TODOs numbered 1 through 6

_Exit criteria_:

- There are no errors in the Run method in Visual Studio. Note that at this point the solution will not yet run.

### Task 2: Configure the Chat Message Processor Function App

Navigate to your **Function App** in the Azure portal. You will update the **Application settings** by adding the following application settings:

```
eventHubConnectionString
sourceEventHubName
destinationEventHubName
storageAccountName
storageAccountKey
serviceBusConnectionString
chatTopicPath
textAnalyticsBaseUrl
textAnalyticsAccountName
textAnalyticsAccountKey
```

_Tasks to complete_:

1.  Create a Shared Access Policy for Event Hub with Manage, Send, and Listen permissions

2.  Create a Shared Access Policy for Service Bus with Manage, Send, and Listen permissions

3.  Copy the connection string from the policy and add it to the new eventHubConnectionString App setting

4.  Copy the connection string from the policy and add it to the new serviceBusConnectionString App setting

5.  Set sourceEventHubName to the name of your first Event Hub

6.  Set destinationEventHubName to the name of your second Event Hub

7.  Set storageAccountName to the name of the storage account you created

8.  Set storageAccountKey to the Key for the storage account

9.  Set chatTopicPath the name of the Service Bus Topic you created

10. Set textAnalyticsBaseUrl to the Endpoint of the Text Analytics Cognitive Services account. Be sure to include a trailing slash in the URL.

11. Set textAnalyticsAccountName to the Account Name of the Text Analytics Cognitive Services account

12. Set textAnalyticsAccountKey to the value of KEY 1 from this same Cognitive Services account

_Exit criteria_:

- You should have values for all the app settings except LuisAppId and LuisKey

## Exercise 3: Configure the Chat Web App settings

Duration: 10 minutes

Within Visual Studio Solution Explorer, expand the ChatWebApp project and open Web.Config. You will update the appSettings in this file.

### Task 1: Configure the Chat Web App settings

_Tasks to complete_:

1.  Copy the connection string from the Event Hub policy you created into the eventHubConnectionString setting

2.  Copy the connection string from the Service Bus policy you created into the serviceBusConnectionString setting

3.  Set eventHubName to the name of your first Event Hub

4.  Set chatTopicPath to the name of the Service Bus Topic you created

5.  Set chatRequestTopicPath to the name of the Service Bus Topic you created

_Exit criteria_:

- You should have values for all the app settings except chatSearchApiBase

## Exercise 4: Deploying the App Services

Duration: 15 minutes

With the App Services projects properly configured, you are now ready to deploy them to their pre-created services in Azure.

### Task 1: Publish the ChatMessageSentimentProcessor Function App

_Tasks to complete_:

1.  Publish the ChatMessageSentimentProcessorFunction Function App project to the Function App you had provisioned for it

_Exit criteria_:

- The Output dialog indicates your publish was successful

### Task 2: Publish the ChatWebApp

_Tasks to complete_:

1.  Publish the ChatWebApp to the Web App you had provisioned for it

_Exit criteria_:

- A browser window should appear with content similar to the following:

  ![The Browser window displays the Contoso Hotels webpage, with a Join Chat window open below.](media/image104.png 'Browser window')

### Task 3: Testing hotel lobby chat

_Tasks to complete_:

1.  Open a browser instance and navigate to the deployed Web App

2.  Join the chat

3.  Repeat with one or more additional browser tabs or from different a device and browser

4.  Start a conversation between these users in the Hotel Lobby

_Exit criteria_:

- You should see your messages appear in all other browser instances joined to the chat

## Exercise 5: Add intelligence

Duration: 60 minutes

In this exercise, you will implement code to activate multiple cognitive intelligence services that act on the chat messages.

### Task 1: Implement sentiment analysis

In this task, you will add code that enables the Event Processor to invoke the Text Analytics API using the REST API and retrieve a sentiment score (a value between 0.0, negative, and 1.0, positive sentiment) for the text of a chat message.

_Tasks to complete_:

1.  Complete the TODOs numbered 7 through 11 in the GetSentimentScore method

2.  Complete TODO 12 in the Run method

_Exit criteria_:

- You should see no errors in either the GetSentimentScore or Run methods

### Task 2: Implement linguistic understanding

In this task, you will create a LUIS app, publish it, and then enable the Event Processor to invoke LUIS using the REST API.

_Tasks to complete_:

1.  Use <http://www.luis.ai> to create a new App

2.  Set the App Settings in the LUIS website to use your subscription key (KEY 1) from your LUIS Account setup in the Azure Portal and activate it

3.  Add an intent named OrderIn with an example utterance of "order a pizza"

4.  Add an entity named RoomService with hierarchical children FoodItem and RoomItem

5.  Review the label for the utterance "order a pizza" and set the intent to OrderIn and entity (pizza) to FoodItem

6.  Add new utterances for "Bring me toothpaste", "Bring me blankets", "Order a soda", and "Order me a pizza"

7.  Train the model

8.  Publish the model

9.  Test the model with the query "order me a pizza" and verify you get the intent of OrderIn (with a score close to 1.0) and entity pizza with a type of "RoomService::FoodItem"

10. Copy the App ID and Subscription Key from your LUIS app into and paste them into two new App settings within your Function App in the Azure portal: `luisAppID` and `luisKey` settings respectively

11. Complete TODO 13 in ProcessChatMessage.cs

12. Update the \_luisBaseUrl in ProcessChatMessage.cs to match that base URL from your LUIS app

_Exit criteria_:

- All TODO items in ProcessChatMessage.cs should be completed

### Task 3: Implement speech to text

There is one last intelligence service to activate in the application---speech recognition. This is powered by the Bing Speech API, and is invoked directly from the web page without going through the web server. In the section that follows, you insert your Cognitive Services Speech API key into the configuration to enable speech to text.

_Tasks to complete_:

1.  Add your Speech API Key under the TODO in scripts\\chatClient.js of the ChatWebApp project

_Exit criteria_:

- You should have all TODO items complete in chatClient.js

**Note**: Embedding the API Key as shown here is done only for convenience. In a production app, you will want to maintain your API Key server-side.

### Task 4: Re-deploy and test

Now that you have added sentiment analysis, language understanding, and speech recognition to the solution, you need to re-deploy the apps so you can test out the new functionality.

_Tasks to complete_:

1.  Publish the ChatMessageSentimentProcessorFunction Function App

2.  Publish ChatWebApp

_Exit criteria_:

- Open a browser and navigate to your deployed website using HTTPS. Use multiple browser instances or tabs to simulate multiple users.

- Send chat messages between them and verify that you see the sentiment indicator (a thumbs up or thumbs down icon next to each chat message)

- You can order something from room service, like "bring me towels" and you get a response from the ConciergeBot

- You can select the microphone to the left of the text box and speak for two to three seconds. Your spoken message should appear.

  ![The Live Chat window has a chat going on between the Concierge Bot and the guest.](media/chat-with-luis.png 'Live Chat window')

## Exercise 6: Building the Power BI dashboard

Duration: 30 minutes

Now that you have the solution deployed and exchanging messages, you can build a Power BI dashboard that monitors the sentiments of the messages being exchanged in real time. The following steps walk through the creation of the dashboard.

### Task 1: Create the static dashboard

_Tasks to complete_:

1.  Using PowerBI.com, create a new report from your streamed **Messages** dataset that contains a semi-circular gauge. It should chart the average score, with a range of 0.0 to 1.0.

_Exit criteria_:

- Your report should look like the following:

  ![A half-donut graph displays an Average score of 0.62.](media/image146.png 'Average of score donut graph')

### Task 2: Create the real-time dashboard

This gauge is currently a static visualization. You will use the report just created to seed a dashboard whose visualizations update as new messages arrive.

_Tasks to complete_:

1.  Create a new dashboard by pinning the gauge you created previously

2.  Open the new dashboard

3.  Enter the QA query "average score created between yesterday and today" and visualize it with the Gauge chart

4.  Format the chart so it ranges from 0.0 to 1.0 and has an indicator at 0.5

5.  Pin this visual to the dashboard you created

6.  Delete the old static chart from the dashboard

7.  Navigate to the ConciergePlus website and send some messages, observing that your Gauge updates in real time

8.  Add three additional visualizations so your dashboard looks as follows:

    ![The Power BI dashboard has four panes: two Count of Messages panes, an Average Sentiment, and Upset Users. The first Count of Messages pane displays a number (18). The second Count of Messages is a pie chart broken out by username. The Average Sentiment is a donut chart displaying the Average Sentiment (0.58) in the past 24 hours. Upset Users chart is a horizontal bar chart displaying the average of upset users (0.25) in the past 24 hours.](media/image158.png 'Power BI dashboard')

_Exit criteria_:

- Your chart should update in real-time and appear as above

### Task 3: Add a trending sentiment chart to the dashboard

The sentiment visualization you created is great for getting a sense of sentiment as of this moment. However, AdventureWorks Travel wishes to view sentiment over time for historical reference and to see whether overall sentiment is trending one way or another. To do this, we will use the tumbling window query output from Stream Analytics to display this data in a line chart.

_Tasks to complete_:

1.  Create a new report from the **TrendingSentiment** dataset

2.  The report should have a line chart with "snapshot" as the Axis, and "average" as the Values

3.  Save the report and pin this visual to the dashboard you created

_Exit criteria_:

- Your new line chart should display on the dashboard, showing the trending sentiment:

![Power BI dashboard showing the average score gauges and trending sentiment](media/power-bi-dashboard-with-trending-sentiment.png 'Power BI Dashboard')

## Exercise 7: Enabling search indexing

Duration: 30 minutes

Now that you have primed the system with some messages, you will create a Search Index and an Indexer in Azure Search upon the messages that are collected in Azure Cosmos DB.

### Task 1: Verifying message archival

Before going further, a good thing to check is whether messages are being written to Azure Cosmos DB from the Stream Analytics Job.

_Tasks to complete_:

1.  Using the Azure Portal, navigate to Data Explorer for your Cosmos DB Collection

_Exit criteria_:

- You should see a list of document IDs, similar to the following:

  ![Documents has been selected from within the Data Explorer in the Azure Portal.](media/image161.png 'Data Explorer')

  ![Message contents display.](media/image162.png 'Message contents')

### Task 2: Creating the index and indexer

_Tasks to complete_:

1.  In your Azure Search Index, use the Connect to your data feature to index the collection

2.  Provide any name for the index, but leave the Key set to id

3.  Ensure that the id, message, createDate, and username are Retrievable

4.  Ensure that createDate, username, and sessionId are Filterable

5.  Ensure that create date, username, and sessionId are Sortable

6.  Ensure that the message field is Searchable

7.  Create an Indexer with a 5-minute interval that starts today

_Exit criteria_:

- After a few moments, your indexer status should appear similar to the following:

  ![The Indexer displays Successes (1), and Failed (0).](media/image171.png 'Indexer')

### Task 3: Update the Web App web.config

_Tasks to complete_:

1.  Modify the web.config of the ChatWebApp project

2.  Set the chatSearchApiBase to the URI of the deployed Search API app

_Exit criteria_:

- You should have all app settings completed with values in the web.config

### Task 4: Configure the Search API App

_Tasks to complete_:

1.  Modify the web.config of the ChatApi project

2.  Set the SearchServiceName to the name of your Search service

3.  Set the SearchServiceQueryApiKey to the Key of your Search service

4.  Set the SearchIndexName to the name of the Index you created in Search

_Exit criteria_:

- You should have all app settings completed with values in the web.config

### Task 5: Re-publish apps

_Tasks to complete_:

1.  Publish the ChatWebApp

2.  Publish the ChatApiApp to your API App

_Exit criteria_:

- Navigate to the Search tab on the deployed Web App and try searching for chat messages (Note that there is up to a 5-minute latency before new messages may appear in the search results)

  ![A Search Message box displays over the Web App Search tab.](media/image182.png 'Web App Search tab ')

## Exercise 8: Add a bot using Bot service and QnA Maker

Duration: 30 minutes

At this point, you have created a real-time chat service in Azure, allowing people to interact with one another. Now we will build a bot that will automatically respond to user questions, helping take the load off the hotel staff.

### Task 1: Create a QnA service instance in Azure

_Tasks to complete_:

1.  Sign up for/into <https://www.qnamaker.ai> and create a new **knowledge base** into the same subscription and resource group as your other lab components

2.  Choose the **F** pricing tier

3.  Name your KB "Concierge Plus". [Download this file](lab-files/faq.xlsx) and use it to populate your KB.

4.  Add a **QnA pair** with "Hi" as the Question, and "Hello. Ask me questions about the hotel" as the Answer.

5.  Test and publish your service

_Exit criteria_:

- The Success page that appears after publishing. You have copied and saved the values for **Knowledge base ID**, **Endpoint HostName**, and **Auth Key** to Notepad or similar:

  ![Successful deployment](media/qna-maker-success.png 'Success')

### Task 2: Create a QnA bot

_Tasks to complete_:

1.  Create a new **Functions Bot** using the **Question and Answer (C#)** template

2.  After it is provisioned, update the Functions Bot's Application Settings with the values you copied at the end of the previous task into the **QnAAuthKey**, **QnAEndpointHostName**, and **QnAKnowledgebaseId** settings

3.  Update the settings to set the bot's display name to "Concierge+ Bot"

4.  Copy the bot's embed code, updating it with the Secret key, and paste it into the Views/Home/Bot.cshtml file within your web app, replacing `<!-- PASTE YOUR BOT EMBED CODE HERE -->`

5.  Add `width` and `hieght` values to your iframe, setting the values to "100%" and "300", respectively

6.  Publish your web app

_Exit criteria_:

- After the web app has been published, navigate to it and select the **Bot** menu item. You have typed in a few questions to ensure the bot is functioning correctly.

  ![Type in a few questions on the bot page](media/bot-service-embedded.png 'Bot page')

## After the hands-on lab

Duration: 10 Minutes

In this exercise, attendees will deprovision any Azure resources that were created in support of the lab.

### Task 1: Delete the resource group

1.  Using the Azure portal, navigate to the Resource group you used throughout this hands-on lab by selecting Resource groups in the left menu

2.  Search for the name of your research group and select it from the list

3.  Select Delete in the command bar and confirm the deletion by re-typing the Resource group name and selecting Delete

You should follow all steps provided _after_ attending the Hands-on lab.

