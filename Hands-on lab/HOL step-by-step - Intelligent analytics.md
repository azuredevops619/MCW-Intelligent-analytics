![Microsoft Cloud Workshops](https://github.com/Microsoft/MCW-Template-Cloud-Workshop/raw/main/Media/ms-cloud-workshop.png "Microsoft Cloud Workshops")

<div class="MCWHeader1">
Intelligent analytics
</div>

<div class="MCWHeader2">
Hands-on lab step-by-step
</div>

<div class="MCWHeader3">
June 2021
</div>

Information in this document, including URL and other Internet Web site references, is subject to change without notice. Unless otherwise noted, the example companies, organizations, products, domain names, e-mail addresses, logos, people, places, and events depicted herein are fictitious, and no association with any real company, organization, product, domain name, e-mail address, logo, person, place or event is intended or should be inferred. Complying with all applicable copyright laws is the responsibility of the user. Without limiting the rights under copyright, no part of this document may be reproduced, stored in or introduced into a retrieval system, or transmitted in any form or by any means (electronic, mechanical, photocopying, recording, or otherwise), or for any purpose, without the express written permission of Microsoft Corporation.

The names of manufacturers, products, or URLs are provided for informational purposes only and Microsoft makes no representations and warranties, either expressed, implied, or statutory, regarding these manufacturers or the use of the products with any Microsoft technologies. The inclusion of a manufacturer or product does not imply endorsement of Microsoft of the manufacturer or product. Links may be provided to third party sites. Such sites are not under the control of Microsoft and Microsoft is not responsible for the contents of any linked site or any link contained in a linked site, or any changes or updates to such sites. Microsoft is not responsible for webcasting or any other form of transmission received from any linked site. Microsoft is providing these links to you only as a convenience, and the inclusion of any link does not imply endorsement of Microsoft of the site or the products contained therein.

© 2020 Microsoft Corporation. All rights reserved.

Microsoft and the trademarks listed at <https://www.microsoft.com/legal/intellectualproperty/Trademarks/Usage/General.aspx> are trademarks of the Microsoft group of companies. All other trademarks are property of their respective owners.

**Contents**

- [Intelligent analytics hands-on lab step-by-step](#intelligent-analytics-hands-on-lab-step-by-step)
  - [Abstract and learning objectives](#abstract-and-learning-objectives)
  - [Overview](#overview)
  - [Solution architecture](#solution-architecture)
  - [Requirements](#requirements)
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
    - [Task 10: Start the Stream Analytics job](#task-10-start-the-stream-analytics-job)
    - [Task 11: Provision an Azure Storage account](#task-11-provision-an-azure-storage-account)
    - [Task 12: Provision Cognitive Services](#task-12-provision-cognitive-services)
  - [Exercise 2: Implement message forwarding](#exercise-2-implement-message-forwarding)
    - [Task 1: Implement the event processor](#task-1-implement-the-event-processor)
    - [Task 2: Configure the Chat Message Processor Function App](#task-2-configure-the-chat-message-processor-function-app)
  - [Exercise 3: Deploying the App Services](#exercise-3-deploying-the-app-services)
    - [Task 1: Restore NuGet Packages for the solution](#task-1-restore-nuget-packages-for-the-solution)
    - [Task 2: Publish the ChatMessageSentimentProcessor Function App](#task-2-publish-the-chatmessagesentimentprocessor-function-app)
    - [Task 3: Publish the ChatWebApp](#task-3-publish-the-chatwebapp)
    - [Task 4: Testing hotel lobby chat](#task-4-testing-hotel-lobby-chat)
  - [Exercise 4: Add intelligence](#exercise-4-add-intelligence)
    - [Task 1: Implement sentiment analysis](#task-1-implement-sentiment-analysis)
    - [Task 2: Implement linguistic understanding](#task-2-implement-linguistic-understanding)
    - [Task 3: Re-deploy the function application and test](#task-3-re-deploy-the-function-application-and-test)
    - [(Optional) Task 4: Improve LUIS Model Performance](#optional-task-4-improve-luis-model-performance)
  - [Exercise 5: Building the Power BI dashboard](#exercise-5-building-the-power-bi-dashboard)
    - [Task 1: Provision Power BI](#task-1-provision-power-bi)
    - [Task 2: Create the static dashboard](#task-2-create-the-static-dashboard)
    - [Task 3: Create the real-time dashboard](#task-3-create-the-real-time-dashboard)
    - [Task 4: Add a trending sentiment chart to the dashboard](#task-4-add-a-trending-sentiment-chart-to-the-dashboard)
  - [Exercise 6: Enabling search indexing](#exercise-6-enabling-search-indexing)
    - [Task 1: Verifying message archive](#task-1-verifying-message-archive)
    - [Task 2: Creating the index and indexer](#task-2-creating-the-index-and-indexer)
    - [Task 3: Update the Chat Web App Configuration](#task-3-update-the-chat-web-app-configuration)
    - [Task 4: Re-publish web app](#task-4-re-publish-web-app)
  <!--- [Exercise 7: Add a bot using Bot service and QnA Maker](#exercise-7-add-a-bot-using-bot-service-and-qna-maker)
    - [Task 1: Create a QnA service instance in Azure](#task-1-create-a-qna-service-instance-in-azure)
    - [Task 2: Create a QnA bot](#task-2-create-a-qna-bot)
    - [Task 3: Embed the bot into your web app](#task-3-embed-the-bot-into-your-web-app)-->
  - [After the hands-on lab](#after-the-hands-on-lab)
    - [Task 1: Delete the resource group](#task-1-delete-the-resource-group)

# Intelligent analytics hands-on lab step-by-step

## Abstract and learning objectives

This hands-on lab is designed to provide exposure to many of Microsoft's transformative line of business applications built using Microsoft advanced analytics. The goal is to show an end-to-end solution, leveraging many of these technologies, but not necessarily doing work in every component possible.

By the end of the hands-on lab, you will be more confident in the various services and technologies provided by Azure, and how they can be combined to build a real-time chat solution that is enhanced by Cognitive Services.

## Overview

First Up Consultants specialize in building software solutions for the hospitality industry. Their latest product is an enterprise mobile/social chat product called Concierge+ (aka ConciergePlus). The mobile web app enables guests to easily stay in touch with the concierge and other guests, enabling greater personalization and improving their experience during their stay. Sentiment analysis is performed on top of chat messages as they occur, enabling hotel operators to keep tabs on guest sentiments in real-time.

## Solution architecture

Below is a diagram of the solution architecture you will build in this lab. Please study this carefully so you understand the whole of the solution as you are working on the various components.

![The preferred solution is shown to meet the customer requirements. From right to left there is an architecture diagram which shows the connections from a mobile device to a Web Application. The Web Application is shown setting data to an Event Hub which is connected to a Web Job. From there Event Hub and Service Bus work together with Stream Analytics, Power BI and Cosmos DB to provide the full solution.](media/preferred-solution-architecture3.png "Solution architecture")

Messages are sent from browsers running within laptop or mobile clients via SignalR to an endpoint running in an Azure Web App. Chat messages received by the Web App are sent to an Event Hub where they are temporarily stored. An Azure Function picks up the chat messages and applies sentiment analysis to the message text (using the Text Analytics API), as well as contextual understanding (using LUIS). The function forwards the chat message to an Event Hub used to store messages for archival purposes, and to a Service Bus Topic which is used to deliver the message to the intended recipients. A Stream Analytics Job provides a simple mechanism for pulling the chat messages from the second Event Hub and writing them to Cosmos DB for archiving, and to Power BI for visualization of sentiment in real-time as well as trending sentiment. An indexer runs atop Cosmos DB that updates the Azure Search index which provides full text search capability. Messages in the Service Bus Topic are pulled by Subscriptions created in the Web App and running on behalf of each client device connected by SignalR. When the Subscription receives a message, it is pushed via SignalR down to the browser-based app and displayed in a web page. Bot Services hosts a bot created using QnA maker, which automatically answers simple questions asked by site visitors.

## Requirements

- Microsoft Azure subscription must be pay-as-you-go or MSDN.
  - Trial subscriptions will not work.
- A virtual machine configured with:
  - Visual Studio Community 2019 or later.
  - Azure SDK 2.9 or later (Included with Visual Studio 2019).

## Exercise 1: Environment setup

Duration: 60 minutes

The following section walks you through the manual steps to provision the services required in the [Azure portal](https://portal.azure.com).  First Up Consultants have provided a starter solution for you. They have asked you to use this as the starting point for creating the Concierge Plus intelligent chat solution in Azure.

### Task 1: Connect to the lab VM

1. In the [Azure portal](https://portal.azure.com), and select Resource groups from the left-hand menu, then enter intelligent-analytics into the filter box, and select the resource group from the list.

    ![On the Resource groups screen, intelligent-analytics is typed in the search field. In the search results, intelligent-analytics is selected.](media/image10.png "Azure Portal Resource groups")

2. Next, select **LabVM** from the list of available resources.

    ![In the List of available resources, LabVM is selected.](media/image11.png "List of available resources")

3. On the LabVM blade, select **Connect** from the top menu, which will download an RDP file.

    ![The Connect button is selected on the LabVM blade toolbar.](media/image12.png "LabVM blade")

4. Open the downloaded RDP file.

5. Select Connect on the Remote Desktop Connection dialog.

    ![The Remote Desktop Connection window states that the publisher of the remote connection can't be identified, and asks if you want to connect anyway. The Connect button is selected.](media/image13.png "Remote Desktop Connection")

6. Enter the following credentials (or the non-default credentials if you changed them):

    - **Username**: `demouser`

    - **Password**: (your password)

7. Select Yes to connect, if prompted that the identity of the remote computer cannot be verified.

    ![The Remote Desktop Connection window states that the identity of the remote computer can't be identified, and asks if you want to connect anyway. The Yes button is selected.](media/image15.png "Remote Desktop Connection window")

8. Open **Internet Explorer**, then download and install Edge.

### Task 2: Download and open the ConciergePlus starter solution

1. From your Lab VM, download the starter project by downloading a .zip copy of the Intelligent analytics GitHub repo.

2. In a web browser, navigate to the [Intelligent analytics MCW repo](https://github.com/Microsoft/MCW-Intelligent-analytics).

3. On the repo page, select **Clone or download**, then select **Download ZIP**.

    ![Download .zip containing the Intelligent analytics repository](media/git-hub-download-repo.png "Download ZIP")

    >**Note**: You can alternatively just enter [this](https://github.com/microsoft/MCW-Intelligent-analytics/archive/refs/heads/main.zip) URL into the browser to directly initiate the download.

4. Unzip the contents of the downloaded ZIP file to the folder **C:\\ConciergePlus**\\.

   >**Note**: Make sure to extract to this exact path. If you extract to a longer directory path, you will hit a Windows max 260 character path limit when you try to build the Visual Studio solution. You will not be able to download the NuGet packages. Keep the solution directory path short.

    ![In the Extract Compressed (Zipped) Folders window, files will be extracted to C:\ConciergePlus.](media/image18.png "Extract Compressed (Zipped) Folders window")

5. Open **ConciergePlusSentiment.sln** in the `C:\ConciergePlus\MCW-Intelligent-analytics-main\Hands-on lab\lab-files\starter-project` folder with Visual Studio 2019.

6. Sign in to Visual Studio or select create account, if prompted.

7. If presented with the Start with a familiar environment dialog, select Visual C\# from the Development Settings drop down list, and select Start Visual Studio.

    ![Development Settings is set to Visual C# in the Visual Studio Start with a familiar environment dialog box.](media/image19.png "Visual Studio Start with a familiar environment dialog box")

8. If the Security Warning window appears, uncheck Ask me for every project in this solution, and select OK.

    ![In the Security Warning window, under the Would you like to open this project? prompt, the Ask me for every project in this solution checkbox is highlighted and unchecked.](media/image20.png 'Security Warning window')

    ![The Visual Studio Solution explorer displays with the initial solution folder and files.](media/2020-08-16-17-20-03.png "Initial solution folder and files")

> **Note**: If you attempt to build the solution at this point, you will see many build errors. This is intentional. You will correct these in the exercises that follow.

> **Note**: Visual Studio Installer will show the installed version of Visual Studio and if the Azure SDK is installed. If the Azure SDK is missing, go back to the **Before the HOL** and make sure you created the correct VM. Updating Visual Studio manually may install components that may not work with the lab.

### Task 3: Create App Services

In these steps, you will provision a Web App within a single App Service Plan.

1. Sign in to the Azure Portal (<https://portal.azure.com>).

2. Select **+Create a resource**, then search for `Web` and choose **Web App**. Select the **Create** button.

    ![The Web App resource overview page is displayed with a Create button.](media/2020-06-29-14-43-33.png "Web App blade")

3. On the Create Web App blade, enter the following:

    - **Subscription**: Select your subscription.
    - **Resource Group**: Select Use existing, and select the **intelligent-analytics** resource group created previously.
    - **Name**: Provide **a unique name** that is indicative of this resource being used to host the Concierge+ chat website (e.g., `conciergepluschatapp + (namespace)`).
    - **Publish**: Choose the **Code** option.
    - **Runtime stack**: **.NET Core 3.1**
    - **OS**: **Windows**
    - **Region**: Choose a region close to you.
    - **App Service plan**: Create a new App Service Plan.
    - **Sku and Size**: **Standard S1**

    - Select **Review and Create** to provision both Web App and the App Service Plan. Select the **Create** button.

4. When provisioning completes, navigate to your new Web App in the portal by selecting **App Services** from the left menu, and then selecting your web app from the list.

    ![In the Azure portal, in the App Services screen, the conciergepluschatapp has a status of running and is selected from the list.](media/2019-06-19-16-11-06.png "Azure Portal, App Services pane")

5. On the App Service screen, select **Configuration** from the left menu, and then select the **General settings** tab.

    ![In the App Service screen, the Configuration menu item is selected from the left menu. The General Settings tab is selected, and the value of the Web sockets field is highlighted and set to On. The Save button is selected from the toolbar menu.](media/2020-06-29-13-32-43.png "App Services Configuration General Settings")

6. Select the toggle for **Web Sockets** to **On**.

   > **Note**: Failure to complete this step will not allow the JavaScript client to communicate with the web application and receive continuous data exchange.

7. Select **Save**.

### Task 4: Provision Function App

In this section, you will provision a Function App that will be used as the EventProcessorHost for processing and enriching Event Hubs data.

1. From the Azure Portal left menu, select **+ Create a resource**, and search for `Function App`. Choose the **Function App** search result and select the **Create** button.

    ![The New screen displays the search text box populated with Function and Function App is displayed as a search suggestion.](media/2019-11-13-14-54-06.png "Create Function App")

2. On the Function App form, enter the following on the **Basics** tab:

    - **Subscription**: Select your subscription.

    - **Resource Group**: Select Use existing, and select the **intelligent-analytics** resource group created previously.

    - **App Name**: Provide **a unique name** that is indicative of this resource being used to process chat messages (e.g., `chatprocessor`).

    - **Publish**: **Code**
  
    - **Runtime Stack**:  Select **.NET**.
  
    - **Version**: Select **3.1**.

    - **Region**: Select the location you used for the resource group created previously.

    - Select **Review + create** to provision the Function App.

    ![The Function App basics tab displays with the form populated with the preceding values.](media/new-function-app.png "Function App Configuration")

### Task 5: Provision Service Bus

In this section, you will provision a Service Bus Namespace and Service Bus Topic.

1. Continuing in the [Azure portal](https://portal.azure.com), select **+ Create a resource** from the left menu.

2. Search for **Service Bus** and select the **Create** button.

    ![On Service Bus overview screen is displayed with a Create button.](media/2019-06-19-16-41-00.png "Service Bus Description")

3. On the Create namespace blade enter the following:

    - **Name**: Provide a unique name for the namespace (e.g., `awhotel-namespace`). Namespace could be your initials.

    - **Pricing tier**: Select **Standard**.

    - **Subscription**: Select the Azure subscription you are using for this hands-on lab.

    - **Resource Group**: Select the **intelligent-analytics** resource group.

    - **Location**: Select the location you are using for resources in this hands-on lab.

      ![The Create namespace blade fields display the previously mentioned settings.](media/2019-06-19-16-43-46.png "Create namespace blade")

4. Select **Review + Create** and **Create**.

5. Once provisioning completes, navigate to your new Service Bus in the portal by choosing Resource Groups in the left menu, then selecting the **intelligent-analytics** resource group, and selecting your Service Bus Namespace from the list of resources.

    ![The Service Bus Namespace is shown in a resource list.](media/2019-06-19-16-48-25.png "Azure Portal Resource Groups")

6. On the Overview blade, select on Topic under Entities on the left-hand side of the blade.

    ![In the Entities section of the resource left menu, the Topics item is selected from within the Entities section.](media/image34.png "Overview blade Entities section")

7. Add a new Topic by selecting +Topic.

    ![The Topic toolbar is shown with the +Topic button selected.](media/image35.png "Topic option")

8. On the Create topic blade, enter the following:

    - **Name**: Enter `awhotel`. This represents that this topic will handle the messages for a particular hotel.

    - **Max topic size**: Leave set to **1 GB**.

    - **Message time to live**: Set to `1` day.

    - **Enable partitioning**: Ensure this checkbox remains unchecked. Chat will not function properly if this is checked.

      ![The Create topic blade fields display the previously mentioned settings. In addition, the following fields are highlighted: Name, which is set to awhotel, Message time to live in Days, which is set to 1, and the Enable partitioning check box.](media/image36.png "Create topic blade")

9. Select **Create**.

10. Create a subscription to the Service Bus topic you just created. The web application will use the subscription to retrieve messages and send them messages to the browser client. Enter these configurations:

    - Select the topic you just created.
    - Select the Subscription menu item on the left-hand menu.
  
    ![The topic menu is displayed. The Subscriptions link is highlighted.](media/2020-08-15-06-06-18.png "Select the subscription")

    ![The screenshot shows the Subscription button highlighted.](media/2020-06-30-20-09-57.png "Create the topic subscription.")

    - Enter `ChatMessageSub` as the name.
    - Max delivery count: 10
    - Auto-delete after idle: 1 day.
    - Message time to live and dead-lettering: 1 day.
  
    Select the **Create** button.
  
11. Navigate back to the **Service Bus namespace** in the Azure Portal.

    ![The screenshot shows current context is at the Service Bus Namespace.](media/2020-08-16-07-21-59.png "Service Bus Namespace")

    >**Note:** Do not create the policy at the topic level for this lab. Create it at the Namespace.

    - Select **Shared access policies** within the left menu, under Settings.

    - In the **Shared access policies**, you are going to create a new policy that the **ChatConsole** can use to retrieve messages. Select **+Add**.

    ![On the Service Bus namespace screen, Shared Access polices is selected from the left menu and the + Add button is highlighted in the toolbar.](media/image85.png "Azure Shared Access policies blade")

    - For the New Policy Name, enter `ChatConsole`.

    - In the list of claims, check **Send** and **Listen** claims.

    ![In the New policy form, Policy name is set to ChatConsole, and three check boxes are selected: Send, and Listen.](media/2020-06-29-10-34-21.png "New policy section")

    - Select **Create**.
  
    - Open the newly created **ChatConsole** policy. Capture the **ServiceBusConnectionString** value in a text file.

    ![The screenshot shows the ServiceBusConnectionString](media/2020-06-29-10-43-15.png "ServiceBusConnectionString value")

### Task 6: Provision Event Hubs

In this task, you will create a new Event Hubs namespace and instance.

1. In the [Azure portal](https://portal.azure.com) left menu, select **+Create a resource**, then search for `Event Hubs`.

    ![The Event Hubs resource overview page is displayed with a Create button.](media/2019-06-19-16-52-46.png "Event Hubs is selected. Select Create button")

2. On the **Create namespace** blade enter the following:

    - **Namespace name**: Provide a unique name for the namespace (e.g., `awhotel-events-namespace`).

    - **Pricing tier**: Select **Standard (20 Consumer groups, 1000 brokered connections)**.

    - **Subscription**: Select the subscription you are using for this hands-on lab.

    - **Resource Group**: Select the **intelligent-analytics** resource group.

    - **Location**: Select the location you are using for resources in this hands-on lab.

    - **Throughput Units**: Leave at `1`.

    - **Enable auto-inflate**: **Unchecked**.

    - Select **Review + create** and **Create** to provision the Event Hubs namespace.

      ![The Create Namespace blade fields display the previously mentioned settings.](media/create-event-hubs-namespace.png "Create namespace blade")

3. When provisioning completes, navigate to your new Event Hub namespace in the portal by choosing **Resource Groups** from the Azure Portal left menu. Select the **intelligent-analytics** resource group followed by your Event Hub Namespace.

    ![In the list of resources, the Event Hub Namespace is selected.](media/image39.png "Azure Portal Resource pane")

4. On the **Overview** blade, select **+Event Hub** to add a new Event Hub. This event hub will receive chat messages from the web application.

    ![The add Event Hub is shown](media/image40.png "Add event hub")

5. On the **Create Event Hub** blade, enter the following:

    - **Name**: Enter `awchathub`.

    - **Partition Count**: Set to the max value of `32`. This will enable you to significantly scale up the number of downstream processors on the Event Hub, where each partition consumer (as handled by the EventProcessorHost) can reach up to 1 Throughput Unit per partition should the need arise. You cannot change this value later.

    - **Message Retention**: Leave set to `1`.

    - **Capture**: Leave set to **Off**.

    - Leave the remaining values as their defaults.

    - Select **Create**.

      ![The Create Event Hub blade fields display with the previously mentioned settings.](media/2020-06-29-13-44-07.png "Create Event Hub blade")

6. Repeat step 5 to create another Event Hub in the same namespace. Name the event hub `awchathub2`.This one will store messages for archival and be processed by Stream Analytics. Stream Analytics forwards the message to Cognitive Search.

    If you select the **Event Hubs** menu item from the left menu, this will display the list of event hubs, you should see the following:

    ![A list of Event Hubs is displayed, awchathub and awchathub2 are shown in this list.](media/2019-03-20-17-01-42.png "Event Hubs created")

7. You will create the `ChatConsole` Event Hub shared policy.  Select **Shared access policies**, under **Settings**, within the left-hand menu.

    >**Note**: Scope this policy to the Namespace level.

8. In the **Shared access policies**, you are going to create a new policy that the **ChatConsole** can use to retrieve messages. Select **+Add**.

    ![The +Add button is selected in the Shared access policies toolbar menu.](media/image78.png "Shared access policies pane")

9. For the **New Policy Name**, enter `ChatConsole`.

10. In the list of Claims, select **Send** and **Listen**. Select the **Create** button.

11. Select the **ChatConsole** policy you just created. Open Notepad and save the primary connection string value for the **EventHubConnectionString**.  Later, you will use it for the Application Settings.

    ![The screenshot highlights ChatConsole primary connection string used for the EventHubConnectionString.](media/2020-08-16-07-40-01.png "EventHubConnectionString")

### Task 7: Provision Azure Cosmos DB

Duration: 15 minutes

In this section, you will provision an Azure Cosmos DB account, a database, and a collection that will be used to collect all the chat messages. Cognitive Search will index this data later. Power BI will use the data for visualizations.

1. In the [Azure portal](https://portal.azure.com), select **+Create a resource**.  Search for **Azure Cosmos DB**.

    ![The Azure Cosmos DB resource overview screen shows the Azure Cosmos DB icon as well as the create button.](media/2019-11-13-15-27-32.png "Create the Azure Cosmos DB")

2. On the **Select API option** page, select **Create** below **Core (SQL) - Recommended**.

    ![Selecting the SQL Cosmos DB offering in the Azure portal.](./media/cosmosdb-core-sql.png 'Selecting SQL offering for Cosmos DB')

3. On the **Basics** blade, enter the following:

    - **Subscription**: Select the subscription you are using for this hands-on lab.

    - **Resource Group**: Select the **intelligent-analytics** resource group.

    - **Account Name**: Provide a unique name for the Azure Cosmos DB account (e.g., `awhotelcosmosdb + namespace`).

    - **Location**: Select the region you are using for resources in this hands-on lab.

    - **Capacity mode**: Keep this set to **Provisioned throughput**.

    - **Apply Free Tier Discount**: There is a limit to one free tier Cosmos DB discount per account. If you still have this available, feel free to apply it here.

        ![Populating the Basics blade of the Cosmos DB provisioning UI in Azure portal.](./media/cosmos-db-basics.png "Cosmos DB provisioning Basics blade")

4. Navigate to the **Global Distribution** blade by selecting **Next: Global Distribution**. Enter the following.

    - **Enable geo-redundancy**: Ensure this is set to **Enable**.

    - **Multi-region Writes**: Ensure this is set to **Disable**.

    - **Availability Zones**: Ensure this is set to **Disable**.

5. Select **Review + create**. After validation passes, select **Create**.

6. When the provisioning completes, navigate to your new Azure Cosmos DB account in the portal.

7. On the **Overview screen**, select **+Add Container**.

    ![The screen shows the Cosmos DB name the user chose and the add new container button is circled.](media/2019-11-13-16-10-38.png "Add New Container")

8. On the **Add Container** blade, enter the following:

    - **Database id**: Create new. Enter `awhotels`.

    - **Container Id**: Enter `messagestore`.

    - **Partition Key**: Enter a partition key such as `/username`.

        > **Note**: Pick a field in this schema.  Otherwise, you will have no documents in the Cosmo DB container. Below is a sample of the messages stored in the Cosmo DB at a later part in the lab.

        ![A sample document in Json format is shown displaying all of the fields available to use as a partition key. The username field is highlighted.](media/2019-03-21-13-18-47.png "Possible fields to partition on.")

    - **Throughput**: Set to `400`.

    - Select **OK** to add the container.

    ![The Add Container form is displayed and is populated with the preceding values.](media/create-messagestore-container.png "Add New Container")

9. Add another container with the following:

    - **Database id**: Enter existing database id `awhotels`.

    - **Container Id**: Enter `trendingsentiment`.

    - **Partition Key**: Enter a partition key such as `/Snapshot`.

    - **Throughput**: Set to `400`.

    - Select **OK** to add the container.

    ![The Cosmos DB Data Explorer displays the awhotels database expanded with messagestore and sentiment child containers.](media/2020-06-29-20-17-45.png "Displaying the Cosmos DB Containers")

### Task 8: Provision Azure Search

In this section, you will create an Azure Search instance.

1. Select **+Create a resource**, then search for `Search`. Select **Azure Cognitive Search** from the results, then select the **Create** button on the resource overview screen.

    ![The New resource search field is populated with the term Search. The suggested results shows Azure Cognitive Search.](media/2019-11-16-05-47-04.png "Create Azure Cognitive Search")

2. On the **New Search Service** blade, enter the following:

    - **Subscription**: Select the subscription you are using for this hands-on lab.
  
    - **Resource Group**: Select the **intelligent-analytics** resource group.

    - **Service name**: Provide a **unique name** for the search service (e.g., `conciergeplusapp`).

    - **Location**: Select the location you are using for resources in this hands-on lab, or the next closest location if your location is unavailable in the list.

    - **Pricing Tier**: Select **Change Pricing Tier** and choose **Basic**.

    - Select **Review + Create**, and once validation has passed, select **Create**.

      ![The New Search Service form is shown populated with the previously mentioned settings.](media/provision-search-service.png "New Search Service blade fields")

### Task 9: Create Stream Analytics job

In this section, you will create the Stream Analytics Job that will be used to read chat messages from the Archival Event Hub and write them to the Azure Cosmos DB and Service Bus.

1. From the Azure Portal left menu, select **+Create a resource**, the search for **Stream Analytics** **job**.  Choose the **Create** button.

2. On the **New Stream Analytics Job** blade, enter the following:

    - **Job Name**: Enter `MessageLogger`.

    - **Subscription**: Select the subscription you are using for this hands-on lab.

    - **Resource Group**: Select the **intelligent-analytics** resource group.

    - **Location**: Select the location you are using for resources in this hands-on lab.

    - **Hosting environment**: Select **Cloud**.

    - Select **Create** to provision the new Stream Analytics job.

      ![The New Stream Analytics Job form fields display the previously mentioned settings. ](media/image49.png "New Stream Analytics Job blade")

3. When provisioning completes, navigate to your new Stream Analytics job in the portal by selecting **Resource Groups** in the left menu, and selecting the **intelligent-analytics** resource group, then selecting your **Stream Analytics Job**.

    ![In the resource group resources listing, the MessageLogger Stream Analytics job is selected.](media/image50.png "Azure Portal Resource Groups pane")

4. From the **Stream Analytics job** left menu, beneath **Job topology**, select the **Inputs** menu item.

    ![In the Job Topology section of the menu, the Inputs item is selected.](media/image51.png "Job Topology section")

5. On the **Inputs** blade, select **+Add stream input** and then select **Event Hub**.

    ![The Add Stream Input is shown, and the Event Hub has been selected from the options.](media/image52.png "Add stream input")

6. On the **New Input** blade, enter the following:

    - **Input Alias**: Set the value to `eventhub`.

    - Choose **Select Event Hub from your subscriptions**.

    - **Subscription**: Choose the same subscription you have been using thus far.

    - **Event Hub namespace**: Choose the Namespace which contains **your Event Hubs instance** (e.g., `awhotel-events-namespace`).

    - **Event hub name**: Choose `awchathub2`, the second Event Hub instance you created. awchathub2 is the archiving event hub.  Messages are pushed there from the ChatMessageSentimentProcessFunction Azure function.

    - **Authentication mode**: Choose **Connection string**, rather than **Managed Identity (Preview)**.

    - **Event hub policy name**: Select **Use existing**, and choose **ChatConsole**.

    - **Event hub consumer group**: Select **Use existing**, and select **$Default**.

    - **Event serialization format**: Leave as **JSON**.

    - **Encoding**: Leave as **UTF-8**.

    - **Event compression type**: Leave set to **None**.

    - Select **Save**.

      ![The Event Hub New input form fields display the previously mentioned settings.](media/2020-06-29-14-30-39.png "Event Hub New input")

7. Now, select **Outputs** from the left-hand menu, under **Job Topology**.

    ![Under Job Topology, Outputs is selected.](media/image54.png "Job Topology section")

8. In the **Outputs** blade, select **+Add**, then select **Cosmos DB**.

    ![The +Add button on the Outputs screen is expanded with Cosmos DB selected from the list.](media/image55.png "Add New Outputs")

9. On the **Cosmos DB New output** blade, enter the following:

    - **Output alias**: `cosmosdb`.

    - Choose **Select Cosmos DB from your subscriptions**.

    - **Subscription**: Choose the same subscription you have been using thus far.

    - **Account Id**: Select your Account id (e.g., **awhotel-cosmosdb**).

    - **Database**: `awhotels`

    - **Container name**: Set to the name of your messages collection, `messagestore`.

    - **Document id**: Set to **messageid** (all lowercase).

    - Select **Save**.

    ![The Cosmos DB New Output form fields display the previously mentioned settings.](media/2020-07-01-09-45-19.png "CosmosDB New Output")

10. **Optional**: Test your connection. Select the Output link on the left-hand side.  Your new `cosmosdb` connection should be listed.  Select the test connection icon. Below is an example of a problem with a Cosmos DB configuration.

    ![Screen shows how to test the cosmosdb output connection for the correct configuration.](media/2020-06-29-17-53-13.png "Test Cosmos DB Connection")

    You should get this success message if the configuration is correct.

    ![Screen shows successful Cosmos DB test message.](media/2020-06-29-17-50-13.png "Successful Cosmos DB connection")

11. Create another Output, this time for **Power BI**.

    ![The Add New Outputs is shown with the Power BI option selected.](media/image57.png "Add New Outputs")

12. Select **Authorize** on the **New output** blade to allow Stream Analytics to connect to Power BI.

13. On the **New output** blade, enter the following:

    - **Output alias**: Enter `powerbi`.

    - **Group workspace**: Select **My workspace** or your corporate workspace.

    - **Dataset Name**: Set to `Messages`.

    - **Table Name**: Set to `Messages`.
  
    - **Authentication Mode**: Select **User token**.

      ![The Power BI New output form is shown and is populated with the preceding values.](media/2019-11-16-06-02-09.png "Power BI new output")

    - Select the **Save** button.

14. Create one final Output for **Power BI**.

    ![The Add New Outputs is shown with the Power BI option selected.](media/image57.png "Add New Outputs")

15. If Stream Analytics is not already authorized, select **Authorize** on the **New output** blade to allow Stream Analytics to connect to Power BI. 

16. On the **New output** blade, enter the following:

    - **Output alias**: `trendingsentiment`

    - **Group workspace**: `My workspace`

    - **Dataset Name**: `TrendingSentiment`

    - **Table Name**: `TrendingSentiment`

    - **Authentication Mode**: `User token`

    ![The Power BI New output form is shown populated with the preceding values.](media/2019-09-03-14-41-07.png "Power BI new output")

17. Select **Save**.

18. Next, select **Query** from the left-hand menu, under **Job Topology**.

    ![In the Stream Analytics job left menu, under the Job topology section the Query menu item is highlighted.](media/image59.png "Job Topology section")

19. Paste the following text into the query window:

    ```sql
    SELECT
    *
    INTO
    cosmosdb
    FROM
    eventhub

    SELECT
    *
    INTO
    powerbi
    FROM
    eventhub

    SELECT AVG(score) AS Average, System.TimeStamp AS Snapshot
    INTO trendingsentiment
    FROM eventhub
    WHERE score > 0
    GROUP BY TumblingWindow(minute, 2)
    ```

20. Select **Save** again.

    ![The query editor toolbar is displayed with the Save button selected.](media/image60.png "Save option")

### Task 10: Start the Stream Analytics job

1. Navigate to your Stream Analytics job in the portal by selecting Resource Groups in the left menu, and selecting **intelligent-analytics**, then selecting your **Stream Analytics Job**.

    ![In the list of resources, the MessageLogger Stream Analytics Job is selected.](media/image50.png "Azure Portal, Resource Groups pane")

2. From the **Overview** blade, select **Start**.

    ![The toolbar from the Stream Analytics Overview screen is displayed with the Start button highlighted.](media/image62.png "Now button")

3. In the **Start job** blade, select **Now** (the job will start processing messages from the current point in time onward).

    ![In the Start job blade the Job output start time field is set to Now.](media/image63.png "Start job blade, Now button")

4. Select **Start**.

5. Allow your Stream Analytics Job a few minutes to start. Once the Job starts it will move to a state of Running.

    ![A message is displayed indicating the Stream Analytics Job is running.](media/2019-06-20-15-46-47.png "Stream analytics job running")

### Task 11: Provision an Azure Storage account

The EventProcessorHost requires an Azure Storage account that it will use to manage its state among multiple instances. In this section, you create that Storage account.

1. In the [Azure portal](https://portal.azure.com) left menu, select **+Create a resource**, search for `Storage account`, then select **Storage account**.  Select the Create button on the resource overview page.

2. In the **Create storage account** form, **Basics** tab, enter the following:

    - **Subscription**: Select the subscription you are using for this hands-on lab.

    - **Resource Group**: Choose Use existing and select the **intelligent-analytics** resource group.

    - **Storage account name**: Provide a unique name for the account e.g., `awhotelchatstore + (namespace)`.

    - **Region**: Select the location you are using for resources in this hands-on lab.

    - **Performance**: Set to **Standard**.
  
    - **Redundancy**: Set to **Locally Redundant Storage (LRS)**.

    Go to the **Advanced** tab.

    - **Enable secure transfer**: Uncheck the box.

    - **Access tier**: **Hot**

    - Select **Review + create**.  Select **Create**.

      ![The Create storage account Basics tab fields display the previously mentioned settings. ](media/create-storage-account.png "Create storage account blade")

### Task 12: Provision Cognitive Services

To provision access to the Text Analytics API (which provides sentiment analysis features), you will need to provision a Cognitive Services account. Based on a phrase, you can tell if a hotel guest is happy or upset.

1. From the [Azure portal](https://portal.azure.com) left menu, select **+Create a resource**, then search for `Text Analytics`

    ![The Text Analytics resource overview screen is displayed with a Create button.](media/2019-11-16-06-18-06.png "Azure Text Analytics Search")

2. On the **Select additional features** page, if you are asked to enable **Custom question answering (preview)**, then select **Continue to create your resource**.

3. On the **Create** blade, enter the following:
    
    - **Subscription**: Select the subscription you are using for this hands-on lab.

    - **Resource Group**: Select the **intelligent-analytics** resource group.
    
    - **Region**: Select the location you are using for resources in this hands-on lab.
    
    - **Name**: Enter a unique name like `awhotels-sentiment`.

    - **Pricing tier**: Choose **Free F0 (5K Transactions per 30 days)**.

    - Acknowledge the Responsible AI notice. 

    ![The Create Text Analytics from is shown populated with the preceding values.](media/create-text-analytics.png "Create Text Analytics")

4. Select the **Review + create** button, and then select **Create**.

5. When it finishes provisioning, browse to the newly created cognitive service by selecting **Resource Groups** in the left menu, then selecting  the **intelligent-analytics** resource group, and selecting the Cognitive Service, **awhotels-sentiment**.

6. Acquire the key for the API by selecting **Keys and Endpoint** on the left-hand menu.

    ![The Resource Management section of the Cognitive Services left menu is displayed, the Keys and Endpoint item is selected.](media/image69.png "Resource Management section")

7. Capture the configuration settings and paste them into Notepad. You will create Application Settings later. Capture:

   - TextAnalyticsAccountName = NAME
   - TextAnalyticsBaseUrl = ENDPOINT
   - TextAnalyticsAccountKey = KEY 1

    ![The screenshot shows the Text Analytics configuration settings highlighted.](media/2020-08-16-07-13-30.png "Text Analytics Configuration Settings")

8. Select **+Create a resource**, select **Language Understanding**, and **Create**.

     ![The Language Understanding icon is displayed.](media/image72.png "Language Understanding")

9. On the **Basics** tab, populate the form fields as follows:

    - **Subscription**: Select the subscription you are using for this hands-on lab.

    - **Resource Group**: Select the **intelligent-analytics** resource group.  

    - **Name**: Enter a unique name, like `luis-api-namespace`, where namespace may be your initials.
  
    - **Authoring Resource: Authoring Location**: Select a region closest to you.
  
    - **Authoring Resource: Authoring pricing tier**: Select **Free F0 (5 Calls per second, 1M Calls per month)**.
  
    - **Prediction Resource: Prediction Location**: Select the location you are using for resources in this hands-on lab.

    - **Prediction Resource: Prediction Pricing Tier**: Select **Free F0 (5 Calls per second, 10K Calls per month)**.

     ![The Create Cognitive Services screen is displayed with the Basics tab selected and the form is populated with the preceding values.](media/luis-basics.png "Create Cognitive Services")

10. Select **Review + create**, then select **Create**.

11. Select the **Keys and Endpoint** link under Resource Management to  retrieve the **Key 1** value for the **luis-api-namespace** Cognitive Service.

12. Verify that you have captured the two API keys (for both Cognitive Services resources) for later reference in this lab.

## Exercise 2: Implement message forwarding

Duration: 45 minutes

In this section, you will implement the message forwarding from the ingest Event Hub instance to an Event Hub instance and a Service Bus Topic. You will also configure the web-based components, which consist of two parts: The Web App UI and the Function App that runs the EventProcessorHost.

### Task 1: Implement the event processor

1. On your Lab VM, open the **ConciergePlusSentiment.sln** file that you downloaded using Visual Studio, if it is not already open.

2. Open **ProcessChatMessage.cs** (found within the **ChatMessageSentimentProcessorFunction** project in the Solution Explorer).

    ![Visual Studio Solution Explorer displays the ChatMessageSentimentProcessorFunction project expanded with the ProcessChatMessage.cs file selected.](media/vs-process-chat-message.png "Visual Studio")

3. Scroll down to the **Run** method. This method represents the heart of the message processing logic utilized by the Event Processor Host running in an Azure function. It is provided a collection of EventData instances, each of which represent a chat message in the solution.

    ![A Visual Studio source code window is displayed with the method signature of the Run method shown.](media/2020-06-30-14-37-10.png "Run method")

4. Locate **TODO: 1** through **TODO: 5** comments and uncomment the code:

    ```csharp
    //TODO: 1.Extract the JSON payload from the binary message
    string sourceEventHubEventBody = Encoding.UTF8.GetString(eventData.Body);
    var sentimentMessage = JsonConvert.DeserializeObject<MessageType>(sourceEventHubEventBody);

    //TODO: 6 Append sentiment score to chat message object
    //if (sentimentMessage.messageType.Equals("chat", StringComparison.OrdinalIgnoreCase))
    //{
    //    sentimentMessage.score = await GetSentimentScore(sentimentMessage);
    //    log.LogInformation("SentimentScore: " + sentimentMessage.score);
    //}

    //TODO: 2.Create a Message (for Service Bus) and EventData instance (for EventHubs) from source message body
    var updatedMessage = JsonConvert.SerializeObject(sentimentMessage);
    var chatMessage  = new Message(Encoding.UTF8.GetBytes(updatedMessage));

    // Write the body of the message to the console.
    log.LogInformation($"Sending message: {updatedMessage}");

    //TODO: 3.Copy the message properties from source to the outgoing message instances
    foreach (var prop in eventData.Properties)
    {
        chatMessage.UserProperties.Add(prop.Key, prop.Value);
    }

    //TODO: 4.Send chat message to Topic
    // Send the message to the topic which will be eventually picked up by ChatHub.cs in the web app.
    await topicClient.SendAsync(chatMessage);

    //TODO: 5.Send chat message to next EventHub (for archival)
    using var eventBatch = archiveEventHubClient.CreateBatch();
    EventData updatedEventData = new EventData(Encoding.UTF8.GetBytes(updatedMessage));

    eventBatch.TryAdd(updatedEventData);
    await archiveEventHubClient.SendAsync(eventBatch);
    Console.WriteLine("Forwarded message to event hub.");
    ```

5. Save the file.

### Task 2: Configure the Chat Message Processor Function App

1. Navigate to your Function App in the [Azure portal](https://portal.azure.com). You can find it by opening your **intelligent-analytics** Resource Group and looking through the list of resources.

2. Select the **Configuration** link in the **Overview** blade of the **Function App**. Choose the **Application settings** link.

    ![The Overview tab is selected in the Function App. The Configuration link is highlighted in the Configured features section.](media/2020-08-16-16-44-35.png "Configuration Link")

3. You will add the following application settings. The following sections walk you through the process of retrieving the values for the Application settings.  If you have already captured these values, you can skip ahead to the adding the **New application setting**.

    ```text
    ChatTopicPath  (e.g. awhotel)
    DestinationEventHubName  (e.g. awchathub2)
    EventHubConnectionString

    ServiceBusConnectionString
    SourceEventHubName   (e.g. awchathub)
    StorageAccountKey
    StorageAccountName

    TextAnalyticsAccountKey
    TextAnalyticsAccountName
    TextAnalyticsBaseUrl
    ```

    > **Note**: It is beneficial to open a second tab or browser window and access the Azure Portal. This way one instance of the Azure Portal may be used to populate the application settings, and the second one can be used to navigate to different resources to obtain key values, connection strings, and URLs. If you decide to use only a single instance of the Azure Portal, be sure to **Save** application settings (available in the Application Settings toolbar) before leaving the screen.

#### Event Hub connection string

Capture the Event Hub connection string.

1. To get the **EventHubConnectionString**, navigate to the Event Hub namespace in the Azure Portal by selecting **Resource Groups** on the left menu, then selecting the **intelligent-analytics** resource group, and selecting your **Event Hubs Namespace** from the list of resources.

    ![In the list of resources, the awhotel-events-namespace Event Hub Namespace is selected.](media/image39.png "Azure Portal, Resource Groups pane")

2. Select **Shared access policies**, under **Settings**, within the left-hand menu.

3. Select the **ChatConsole** from the list of policies, and then copy the **Connection string--primary key** value.

    ![In the SAS Policy: ChatConsole pane, the Connection string-primary key is selected.](media/2020-06-30-14-48-44.png "Shared access policies, and SAS Policy: Chat Console pane")

4. Return to the **Application Settings** for the Function App in the [Azure portal](https://portal.azure.com) by selecting the **Configuration** link on the Overview pane. Select **+ New application setting** in the toolbar menu of the Application Settings section.

    ![The Application settings tab is selected and the + New application setting button is selected in the toolbar menu located in the Application settings section of the page.](media/2019-11-16-12-39-18.png "Add new application setting.")

5. Enter `EventHubConnectionString` into the **Name** field, and paste the copied connection string value from step 6 into the **Value** field. Select the **OK** button.

    ![EventHubConnectionString is shown in the list of application settings.](media/function-app-eventhubconnectionstring.png "Application setting value")

#### Event Hub name

Your event hubs can be found by going to your Event Hub overview blade, and selecting Event Hubs from the left menu.

![Event hubs overview blade](media/image81.png 'Event hubs')

1. Create a new application setting with the **Name** `SourceEventHubName`. For the Value, enter the name of your first Event Hub, `awchathub`.

2. Create a new application setting with the **Name** `DestinationEventHubName`, enter the name of your second Event Hub, `awchathub2`, as the **Value**.

#### Storage account

Your storage accounts can be found by going to the intelligent-analytics resource group, and selecting the Storage account.

1. Create a new application setting with the **Name** `StorageAccountName` and enter the name of the storage account you created in the **Value** field.

2. Create a new application setting with the **Name** `StorageAccountKey` and enter the Key for the storage account you created (which you can retrieve from the Portal).

    - From your storage account's blade, select **Access Keys** from the left menu, under **Security + networking**.

      ![Under Settings section of the Storage Account left menu, the Access Keys menu item is selected.](media/find-storage-account-access-keys.png "Settings section")

    - Copy the Key value for **key1**, and paste that into the value for **StorageAccountKey**. You may need to select **Show keys** first.

      ![In the Access Keys section, the value for Key1 and its copy button are selected.](media/2019-03-20-19-39-42.png "Access Keys section")

#### Service Bus connection string

1. Navigate to the **Service Bus Namespace Overview** page. From the left menu, select the **Shared Access policies** link in the **Settings** section. Select the **ChatConsole** from the list of policies and copy the **Primary Connection String** value and paste into your text file.

    ![The screen shows the service bus Shared access policies.  The ChatConsole policy is listed.](media/2019-11-16-12-53-19.png "Service Bus - Shared access policies")

2. Return to the Function App's Application Settings and create a new setting with the **Name** `ServiceBusConnectionString` and paste this connection string as the **Value**.

#### Chat topic

1. Create a new application setting with the **Name** `ChatTopicPath`. Enter the name of the Service Bus Topic you had created (e.g., `awhotel`) as the **Value**. This can be found under **Topics** on the **Service Bus Namespace Overview blade**.

    ![The Entities portion of the Service Bus Namespace left menu is displayed with the Topics menu item highlighted.](media/image89.png "Service bus entities overview blade")

#### Text Analytics API settings

1. In the [Azure portal](https://portal.azure.com), open the Cognitive Service Text API (e.g. **awhotels-sentiment**) in your resource group.

2. On the left-hand menu of the Text API blade, select **Keys and Endpoint**. You will use these configuration values for creating Function Application settings. 

3. Create a new application setting with the **Name** `TextAnalyticsBaseUrl` and enter the **ENDPOINT** as the **Value**. **Use all lower case alpha characters with no spaces**.

4. Create a new application setting with the **Name** `TextAnalyticsAccountName` and paste the value of the Cognitive Service **Name** into the **Value** field.

5. Create a new application setting with the **Name** `TextAnalyticsAccountKey` and paste the value of **KEY 1** of the Cognitive Service into the **Value** field.

6. Scroll to the top of the function **Application Settings** and select **Save** from the toolbar. Your application settings should now resemble the following (to see the hidden values, select the eye icon in the Value column)

    ![The Application Settings listing for the Function App is shown.](media/2020-08-16-10-07-23.png "Function Application settings")

## Exercise 3: Deploying the App Services

Duration: 15 minutes

1. Navigate to the web application and then select the **Configuration** menu item on the left hand side. Add these new application settings, and select **Save** once you are done.  

    ```config
    ChatMessageSubscriptionName  (e.g. ChatMessageSub)
    ChatTopicPath (e.g. awhotel)
    EventHubConnectionString
    ServiceBusConnectionString
    SourceEventHubName   (e.g. awchathub)
    ```

    ![The screenshot shows the web application configuration settings.](media/2020-06-29-10-27-36.png "Web application configuration settings")

With the App Services projects properly configured, you are now ready to deploy them to their pre-created services in Azure.

### Task 1: Restore NuGet Packages for the solution

1. In **Visual Studio Solution Explorer**, right-click on the Solution at the top of the tree, and select **Restore NuGet Packages** from the context menu. Build the Solution.

   ![Visual Studio Solution Explorer is shown with the context menu displaying for the solution file and the Restore NuGet Packages option selected from the context menu.](media/restorenugetpackages.png "Visual Studio Solution Explorer")

### Task 2: Publish the ChatMessageSentimentProcessor Function App

1. Within **Visual Studio Solution Explorer**, right-click the `ChatMessageSentimentProcessorFunction` project, and select **Publish...** from the context menu.

    ![In Solution Explorer, the sub-menu for ChatMessageSentimentProcessorFunction displays, with Publish... selected.](media/vs-publish-function-menu.png "Solution Explorer")

2. In the **Publish** dialog, choose **Azure** as the publish target. Then, select **Next**.

    ![In the Pick a publish target dialog, Azure is selected.](media/azure-publish-target.png "Publish dialog box")

3. Select **Azure Function App (Windows)** as the specific target. Select **Next**.

4. In the **Functions instance** tab, choose the **Subscription** that contains your Function App you provisioned earlier. Expand your **Resource Group** (e.g., **intelligent-analytics**), then select the node for your **Function App** in the tree view to select it.

    ![In the App Service dialog box, the resource group is expanded and the function app chatprocessor is selected.](media/choose-function-app-blur.png "App Service dialog box")

5. Select **Finish** and then **Publish**.

    ![The Azure Function publish dialog box is displayed.](media/vs-publish-function-publish.png "Publish dialog box")

6. When the publish completes, the Output window should indicate success similar to the following:

    ![The Output window is set to show output from Build. Output indicates it is updating files, and that Publish Succeeded.](media/vs-publish-function-output.png "Output window")

    > **Note**: If you receive an error in the Output window, as a result of the publish process failing (The target "MSDeployPublish" does not exist in the project), expand the Properties folder within the Visual Studio project, then delete the PublishProfiles folder.

### Task 3: Publish the ChatWebApp

1. Within **Visual Studio Solution Explorer**, right-click the ChatWebApp project and select **Publish...** from the context menu.

    ![In the Visual Studio Solution Explorer ChatWebApp sub-menu, Publish is selected.](media/image100.png "Visual Studio Solution Explorer")

2. In the **Publish** dialog, choose **Azure** as the publish target. Then, select **Next**.

    ![In the Pick a publish target dialog, Azure is selected.](media/azure-publish-target.png "Publish dialog box")

3. In the **Specific target** blade, select **Azure App Service (Windows)**.

    ![In the Specific target window, the Microsoft Azure App Service (Windows) option is selected.](media/azure-app-service-windows-target.png "Windows App Service target")

4. In the **App Service** dialog, choose your **Subscription** that contains your Web App you provisioned earlier. Expand your **Resource Group**, **intelligent-analytics**, then select the node for your **Web App** in the tree view to select it.

5. Select **OK** (or **Finish**). Then, publish the app.

6. When the publishing is complete, a browser window should appear with content like the following:

    ![The Browser window displays the Contoso Hotels webpage with a Join Chat section that has a username textbox and a chat room selection drop down list.](media/2020-06-29-10-10-20.png "Join chat landing page")

    > **Note**: It may take a couple of minutes for the browser to render. You must use a modern browser like Edge. If the site is opened in Internet Explorer, copy the URL from the address bar, open Edge (that you installed earlier), and navigate to the site with Edge instead.

### Task 4: Testing hotel lobby chat

1. Open a browser instance (Edge is recommended for this web app), and navigate to the deployment URL for your Web App.

    - If you are unsure what this URL is, it can be found in two places:

      - First, you can find it on the **ChatWebApp** document in **Visual Studio**, that was opened when you published the Web App.

        ![In the Visual Studio ChatWebbApp tab, under Summary, the Site URL is displayed.](media/2019-06-20-17-53-33.png "Visual Studio ChatWebbApp tab")

      - Alternatively, this can be found in the [Azure portal](https://portal.azure.com) on the **Overview** blade for your **Web App**.

2. Under the **Join Chat** area, enter your username (anything will do).

3. Leave **Hotel Lobby** selected.

4. Select the **Join** button.

5. The Live Chat should appear. Wait for a 1 minute. The first message warms up the system. You should see a message stating you have connected to the chat service and you have joined the session.

   The **connected** message means you have connected to the web application via SignalR.  The **join** message means you have sent a message to the event hub and the function app Event Hub Trigger copied the message to the service bus topic. Also, the web application has received the message using the topic subscription and pushed it the browser client.

   > **Warning**: Failure to see these messages means your configuration could be incorrect and will cause problems in the next exercises. Debugging steps are listed below.

    ![The Live Chat window displays, showing that it is connected to the chat service.](media/2020-06-29-10-15-14.png "Live Chat window")

    - Optional: Open the browser console. You should see similar SignalR debug messages.  Check for:
      - A WebSocket message (wss://). If you do not see this message, configure web sockets in your web application.
      - "ReceivedMessage was called." This means the event listener was set up correctly for the Service Bus topic subscription `ChatMessageSub`.

    ![The screenshot shows SignalR debug messages.](media/2020-08-17-08-58-40.png "SignalR Debug Messages")

6. Open another browser tab and navigate to the web site.

7. Enter `HotelLobby` (no spaces), and select **Join**.

8. From either session, fill in the Chat text box and select **Send**. You can try using @ and \# too, just to seed some text for search.

    ![The Live Chat window shows a chat going on between two users.](media/2020-06-29-10-17-13.png "Live Chat window")

9. Optional: Debugging Chat Messages

    - Event Hub Message Debugging.
      - Navigate back to the Azure Portal and select your resource group.
      - Select the event hub.
      - Select your event hub link.  e.g. awchathub.
      - Select the **Process Data** in the left-hand menu item.
      - Select the **Explore** button.

        ![The screenshot shows how to process event hub data.](media/2020-08-16-15-29-27.png "Process Event Hub Data")

      - Select the **Test query** link.  You may have to grant permissions.
        ![The image shows the Test query link highlighted](media/2020-08-16-15-33-01.png "Test query")

        Your chat messages should appear here.  This means your web application and event hub configuration settings are correct. If messages do not show, go back and check your Application Settings. Notice your messages are spread across the different partitions.

        ![The image shows test messages in the event hub.](media/2020-08-16-15-37-22.png "Event hub test messages")

    - Check your function app.

        > **Note**: _Debugging Tips_: If you navigate to the Azure Function Monitor, you can get more clues to any problems. Below is example of the log output. It can take up to 5 minutes for log entries to display.

        - Select your **ProcessChatMessage** function from the list of functions.

        - Select **Monitor** from the left-hand menu.

        ![The image shows the Function App Monitor link highlighted.](media/2020-08-16-16-12-48.png "Function App Monitor Link")

    ![The Azure Function log is shown with the output of an error displayed.](media/2019-11-24-07-23-56.png "Azure Function Monitor Log Example")

    - Check your Service Bus Subscription activity.
      - Navigate back to your service bus overview.  You should see the message count greater than zero. The web application is going to pull these messages and display them in the browser.

    ![The image shows the service bus activity. Message counts are greater than zero.](media/2020-08-16-16-28-44.png "Service bus activity")

10. Test your Stream Analytics Query.

    - Navigate back to your MessageLogger Stream Analytics job.
    - Select the Query item on the left-hand side.
    - Review the Event Hub `Input preview` and the Stream Analytics `Test results`.

    ![The screen shows how to test your Stream Analytics queries. The arrows point to the Query menu item and Test query button.](media/2020-07-02-16-53-08.png "Test your query")

## Exercise 4: Add intelligence

Duration: 60 minutes

In this exercise, you will implement code to activate multiple cognitive intelligence services that act on the chat messages.

### Task 1: Implement sentiment analysis

In this task, you will add code that enables the Event Processor to invoke the Text Analytics API using the REST API and retrieve a sentiment score (a value between 0.0, negative, and 1.0, positive sentiment) for the text of a chat message.

1. In the **Solution Explorer** in **Visual Studio**, open **ProcessChatMessage.cs** in the **ChatMessageSentimentProcessorFunction** project.

2. Scroll down to the method **Run**.

3. Uncomment the code for **TODO: 6**. It should look like:

    ```csharp
    //TODO: 6 Append sentiment score to chat message object
    if (sentimentMessage.messageType.Equals("chat", StringComparison.OrdinalIgnoreCase))
    {
        sentimentMessage.score = await GetSentimentScore(sentimentMessage);
        log.LogInformation("SentimentScore: " + sentimentMessage.score);
    }
    ```

4. Build your **ChatMessageSentimentProcessFunction** project.
5. Publish your **ChatMessageSentimentProcessFunction** project to Azure.
6. Test your sentiment query by selecting the sentiment query and selecting the `Test selected query` button.  Check your results.  Look at the **score** column. You should have all of the negative chat messages. Sentiment analysis has not been applied just yet. The sentiment analysis will be tested after the Language Understanding configuration has been completed.

    ![The screen shows the ability to test your queries before saving them.](media/testing-sentiment.png "Test your queries")

### Task 2: Implement linguistic understanding

What is the hotel guest's intention?

In this task, you will create a LUIS app, publish it, and then enable the Event Processor to invoke LUIS using the `Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime` NuGet package. You will be setting up the LUIS model hierarchy and executing these steps.

![The diagram depicts the exercise steps. Create model, train model, test model, publish.](media/2020-06-30-14-22-42.png "LUIS workflow")

1. Using a browser, navigate to <http://www.luis.ai>.  

    > **Note**: If in Exercise 1, Step 12 you created your Luis account in Azure in a European region (e.g. West Europe), user <http://eu.luis.ai> instead. If you selected an Australian region use <http://au.luis.ai>.

2. Select **Login / Sign Up**.

3. Sign in using your Microsoft account (or \@Microsoft.com account if that is appropriate to you). The new account startup process may take a few minutes. If you are prompted to migrate to the Preview version of the LUIS portal, select **Migrate Later**.

4. On the **Welcome to the Language Understanding Intelligent Service (LUIS)!** window, select the appropriate Azure subscription in your tenant.

5. If you are prompted to **Choose an Authoring resource**, select the **luis-api-namespace-Authoring** resource and select **Done**

6. Select **Accept** terms button.

7. You should be redirected to the **Conversation apps** list page. In the toolbar, select **+ New app**.

    ![The My Apps table is displayed with the + New app button highlighted in the toolbar.](media/luis-new-conversation-app.png "Create LUIS app")

8. Complete the **Create a new app** form by providing `awchat` as the name for your LUIS app, selecting the **English** culture, selecting the **luis-api-namespace** prediction resource, and then selecting **Done**.

    ![In the Create a new app dialog box, the Name field is set to awchat, and Culture is set to English.](media/2020-08-18-17-46-45.png "Create new app")

9. Scroll through the examples of how to create the intents and utterances. Close the dialog.

10. In a moment, your new `awchat` app will appear.

11. Choose **Build** from the toolbar. In the **Intents** pane, select **+ Create** link.

    ![The top toolbar is displayed. The Build menu item is selected.](media/2019-06-20-19-55-55.png "Build Menu Item")

12. In the **Intents** dialog, for the **Intent Name** enter `OrderIn` and select **Done**.

    ![The OrderIn intent has been entered into the Create new intent dialog and the Done button is selected.](media/image118.png "Create a new intent")

13. Select **Entities** from the menu on the left.

    ![The Entities menu item is selected in the left menu.](media/2020-06-28-10-09-17.png "Entities menu")

14. Select **+Create**.

    ![On the Entities screen, the Create new entity button is selected on the toolbar.](media/2020-08-18-17-59-38.png "Create new entity")

15. For the **Entity name**, enter **RoomService**.  Select the **Create** button.

    ![The Add Entity dialog is displayed. Name and Entity Type are set to the preceding values.](media/2020-06-28-08-18-43.png "Add Entity dialog box")

16. Return to the **OrderIn** intent screen to enter `utterances`.

    ![The OrderIn intent item is selected.](media/2020-06-28-08-31-26.png "Return to OrderIn Intent")

17. In the example input text box, enter the "order a pizza" utterance and **press the Enter key to save your work**.

18. Your newly created pizza utterance is shown below. Select the utterance with your mouse and associate it with the RoomService entity in the dropdown. Upon entity selection, your work will be saved.

    ![The screen shows the order a pizza utterance highlighted and a dropdown menu displaying RoomService entity.](media/2020-06-28-08-36-52.png "Associate the entity")

    Your work is saved automatically.

    ![The order pizza utterance is displayed with associated RoomService entity.](media/2020-06-28-08-40-56.png "Example of saved utterance")
19. Time to train your model.  Select the Train button in the upper right.  You should see the following result:

    ![The picture shows the Train button with a green circle and a message of 'Predictions loaded' displayed.](media/2020-06-28-08-45-49.png "Prediction loaded confirmation")

20. Now, it is time to test your model.  Select the Test button. Type in, "I would like to order a pizza" into the text box and select the `Enter` key. Select the **Inspect** link. You should see a correlation result above to 90% as well as the associated ML entity.

    ![The screen shows the original test phrase and the test results.](media/2020-06-28-08-55-50.png "Inspecting the test results")

    It is important to see a high correlation (above .75) and a resulting ML entity because the chat application needs it in order to send the message to the correct hotel department. Below is the code from the function application that determines if there is an utterance match.

    ![The screen shows the chat processing code and the reason for making sure the entity and intent is returned.](media/2020-06-29-05-10-51.png "Code sample of intent handler")

21. Repeat this process for the following phrases and associate them with the **RoomService** entity:

    - I am hungry
    - order food
    - order a hamburger
    - order a soda
    - order dinner
    - order breakfast
    - order a drink

22. Create a **Housekeeping** entity. It is also a **Machine learned** entity.

23. Create the following utterances and associate them with the Housekeeping entity. To do this, select **Intents** and **OrderIn**.

    - more towels
    - more blankets
    - room too warm
    - room too cold
    - I am cold
    - I am too hot

24. Train and test your model.  Did you get the expected test results?

25. Enter one more utterance, `order a hotdog`. There is some new functionality in LUIS. Notice the predicted label/entity may be suggested for you.  Confirm the **RoomService** entity. Train and test your model.

    ![The screenshot shows the new machine learning prediction for the utterance. The user did not have to select the phrase and associated it with the entity.](media/2020-08-18-19-02-58.png "Machine learning entity prediction")

    ![The screenshot shows the Confirm RoomService option highlighted](media/2020-08-18-19-04-47.png "Confirm RoomService")

26. Select **Manage** from the toolbar, then select **Azure Resources** from the left menu. In the **luis-api-namespace** section, the URL is available in the **Example Query** textbox. 

    ![The Azure Resources menu item is selected from the left menu and the Example Query URL is shown in a textbox.](media/2020-08-18-19-27-46.png "LUIS Key Information")

27. Select **Publish** at the upper right-hand corner of the page to publish the endpoint. Publish to the **Staging Slot** and select **Done**.

    ![Publishing the awchat app LUIS endpoint to the staging slot.](./media/publish-luis-to-staging-slot.PNG "Staging slot deployment")

28. Open a new tab in your browser. Paste the **Example Query** URL into the address bar and modify the end of the URL (the text following q= ) so it contains the phrase `bring me towels` and press **ENTER**. You should receive output similar to the following. Observe that it correctly identified the intent as **OrderIn** (in this case with a confidence of nearly 100%) and the entity as having an entity type of **Housekeeping** (in this case with a confidence score of 97.5%).

    ```json
    {
        "query": "bring me towels",
        "prediction": {
            "topIntent": "OrderIn",
            "intents": {
                "OrderIn": {
                    "score": 0.98740774
                },
                "None": {
                    "score": 0.008991768
                }
            },
            "entities": {
                "Housekeeping": [
                    "bring me towels"
                ],
                "$instance": {
                    "Housekeeping": [
                        {
                            "type": "Housekeeping",
                            "text": "bring me towels",
                            "startIndex": 0,
                            "length": 15,
                            "score": 0.97544956,
                            "modelTypeId": 1,
                            "modelType": "Entity Extractor",
                            "recognitionSources": [
                                "model"
                            ]
                        }
                    ]
                }
            }
        }
    }
    ```

29. Note that LUIS can learn from challenges it faces when it's deployed. For example, if your LUIS endpoint failed to determine entities for the "bring me towels" query, navigate to **Review endpoint utterances** below **Improve app performance**. Once you ensure that the **Predicted Intent** is **OrderIn**, associate the utterance with the proper entity (such as **Housekeeping**). Select **Save**. Then, select **Train** and **Publish**.

    ![Associating a low-performing utterance with the correct entity.](./media/review-endpoint-utterances-luis.png "Improving LUIS model performance after deployment")

30. Go back to the **luis-api-namespace** screen. Capture three LUIS values and add them to the Azure Function application settings.

    ```text
    LuisPredictionKey
    LuisBaseUrl
    LuisAppId
    ```

    !["The screenshot shows the LUIS Starter_Key settings. Three application settings are highlighted. LuisPredictionKey, LuisBaseUrl, and LuisAppId"](media/2020-08-18-19-32-28.png "LUIS Application Settings")

31. In the LUIS web page, select the **Publish** button.

    ![The screenshot shows the Publish button highlighted.](media/2020-08-18-20-08-05.png "Publish Slot Button")

32. Select the **Production Slot** setting.  Select the **Done** button.

    ![The screenshot shows the publishing slot settings. The Production Slot is selected.](media/2020-08-18-20-05-23.png "Production Slot")

33. **Save** your Application Settings. The Event Processor is pre-configured to invoke the LUIS API using the provided App ID and key.

34. Open **Visual Studio** then open **ProcessChatMessage.cs** within the **ChatMessageSentimentProcessorFunction** project, and navigate to the Run method.

35. Locate **TODO: 7** and uncomment the code:

    ```csharp
    //TODO: 7.Respond to chat message intent if appropriate
    var updatedMessageObject = JsonConvert.DeserializeObject<MessageType>(updatedMessage);

    // Get your most likely intent based on your message.
    var intent = await GetIntentAndEntities(updatedMessageObject.message);
    await HandleIntent(intent, updatedMessageObject, topicClient);
    ```

36. Take a look at the implementation of both methods if you are curious how the entity and intent information is used to generate an automatic chat message response from a bot.

37. Save the file.

### Task 3: Re-deploy the function application and test

Now that you have added sentiment analysis and language understanding to the solution, you need to re-deploy the apps so you can test out the new functionality.

1. Build and Publish the **ChatMessageSentimentProcessorFunction** Function App using **Visual Studio**.

2. Open the Hotel Lobby web page. Join a chat in the **Hotel Lobby**.

3. Type a message with a positive sentiment, like `I love this weather`. Observe the **thumbs-up** icon that appears next to the chat message you sent. Next, type something like, `I hate this weather` and observe the **thumbs-down** icon. These are indicators of sentiment (as applied by your solution in real-time).

    ![In the Live Chat window, callouts point to the thumbs-up and thumbs-down icons. The 'love this weather' statement produces a thumbs-up icon. Thumbs down for the 'I have this weather'](media/2020-06-29-05-44-34.png "Live Chat window")

4. Next, try ordering some items from room service, like `bring me towels` and `order a pizza`. Observe that you get a response from the **ConciergeBot**, and that the reply indicates whether your request was sent to **Housekeeping** or **Room Service**, depending on whether the item ordered was a room or food item.

    ![In the chat window, Tim is having a conversation with a ConciergeBot. He asks for towels, and the ConciergeBot says they are forwarding the request to Housekeeping.](media/2020-06-29-05-47-15.png "Live Chat window")

### (Optional) Task 4: Improve LUIS Model Performance

As mentioned previously, LUIS captures challenging queries from production use and allows developers to improve, train, test, and deploy their Language Understanding models. The key component that supports this feature is the `log=true` query parameter that is part of the LUIS endpoint.

1. In [LUIS](luis.ai), select the **awchat** conversation app.

2. Select **Review endpoint utterances** below **Improve app performance**.

    ![LUIS endpoint utterances.](./media/review-endpoint-utterances-after-deploy.png "Utterances from the LUIS production endpoint")

3. Consider the first utterance shown above (`the food was terrible`). As this is not a request, and should thus not be routed to neither the Housekeeping nor RoomService entities, the developer must change the **Predicted Intent** to **None**. Then, they must select **Save utterance**.

4. Same applies to the `hello!` utterance. It is not a request, so the developer will change the predicted intent to **None**.

    >**Note**: Is it possible to define an intent for greetings, given that customers will likely issue a greeting before conversing through the app? Will that add value to the app? What entities could you associate with a greetings intent? Defining a logical schema is a critical part of using LUIS.

5. Move test entries that have incorrectly been classified as requests to the **None** intent. Notice how those utterances are now accessible under the **None** intent.

    ![None intent in LUIS with utterances.](./media/none-intent.png "Utterances properly classified as the None intent")

6. Once you've dealt with the suggested utterances, **Train** and **Test** the LUIS model.

## Exercise 5: Building the Power BI dashboard

Duration: 30 minutes

Now that you have the solution deployed and exchanging messages, you can build a Power BI dashboard that monitors the sentiments of the messages being exchanged in real time. The following steps walk through the creation of the dashboard.

### Task 1: Provision Power BI

If you do not already have a Power BI account:

1. Go to <https://powerbi.microsoft.com/features/>.

2. Scroll down until you see the **Try Power BI for free!** section of the page, and select the **Try Free** button.

    ![Screenshot of the Power BI Try for free section.](media/setup3.png "Power BI Try for free section")

3. On the page, enter your work email address (which should be the same account as the one you use for your Azure subscription), and select **Sign up**.

    ![The Get started page has a field for entering your work email address.](media/setup4.png "Get started page")

4. Follow the on-screen prompts, and your Power BI environment should be ready within minutes. You can always return to it via <https://app.powerbi.com/>.

### Task 2: Create the static dashboard

1. Sign in to your Power BI subscription (<https://app.powerbi.com>).

2. Select **My Workspace** on the left-hand menu, then select the **Datasets** tab. Depending on your Power BI version, it may read **Datasets + dataflows**.

    ![In the Power BI window, on the left menu, My Workspace is selected. In the right pane, the Datasets tab is selected.](media/image140.png "Power BI window")

3. Under the **Datasets** (or **Datasets + dataflows**) list, select the **Messages** dataset.

    ![On the Datasets tab, the Messages dataset is highlighted in the table.](media/image141.png "Datasets tab")

4. Select the **Create report** button after clicking the three dots.

    ![On the Datasets + dataflows tab, the Create Report button is selected.](media/create-report-from-messages.png "Datasets + dataflows tab")

5. On the **Visualizations** palette, select **Gauge** to create a semi-circular gauge.

    ![On the Visualizations palette, the Gauge icon is highlighted.](media/image143.png "Visualizations palette")

6. In the **Fields** listing, select and drag the **score** field and drop it onto the **Value** field.

    ![In the Visualizations blade, the Gauge is selected. From the Fields blade under Messages, the score check box is selected. An arrow points from this to the Value field in the Visualizations pane indicating a drag and drop operation. Score is now listed as the Value in Visualizations.](media/image144.png "Visualizations and Fields listings")

7. Select the drop-down menu that appears where you dropped score and select **Average**.

    ![Average is selected from the Value drop down box in the Visualizations blade.](media/image145.png "Drop-down menu")

8. You now should have a gauge that shows the average sentiment for all the data collected so far, which should look similar to the following. If you have not sent many messages, the average sentiment may be a negative value, as the chat processor set a sentiment of -1 before you implemented proper Text Analytics sentiment analysis.

    ![A semi-circle gauge graph displays for Average of score, which is 0.62.](media/image146.png "Gauge graph")

9. From the **File** menu, select **Save** to save your visualization to a new report.

    ![On the toolbar the File menu is expanded and Save (Save this report) is selected.](media/image147.png "File menu")

10. Enter `ChatSentiment` for the report name, and select **Save**.

    ![In the Save your report window, ChatSentiment is typed in as the name of the report.](media/image148.png "Save your report window")

### Task 3: Create the real-time dashboard

This gauge is currently a static visualization. You will use the report just created to seed a dashboard whose visualizations update as new messages arrive.

1. Select the **Pin to a dashboard** item located on the toolbar. You will need to select the three dots to find this item.

    ![On the toolbar, the Pin to a dashboard button is selected.](media/pin-gauge-to-dashboard.png "Gauge control menu bar")

2. Select **New dashboard**, enter `Real-time Sentiment` as the name, and select **Pin Live**.

    ![On the Pin to dashboard dialog box, on the left, a Preview of the ChatSentiment Gauge graph displays. On the right, under Where would you like to pin to, the New dashboard radio button is selected.](media/2019-06-21-09-41-59.png "Pin to dashboard dialog box")

3. Return to the **My Workspace** page, and select your newly created dashboard from the list of dashboards.

    ![My Workspace dashboards list is displayed with the Real-time Sentiment dashboard selected.](media/image151.png 'My Workspace dashboards')

4. Real-time dashboards are created in Power BI using the Q&A feature, by typing in a question to visualize in the space provided. In the **Ask a question about your data** field, enter: `average score created between yesterday and today`.

    ![The input text box placeholder texts states Ask a question about your data.](media/2019-03-21-10-26-35.png "Ask a question about your data")

    ![average score created between yesterday and today is typed in the Ask question about your data textbox. An average of score (0.62) displays below.](media/image152.png "Ask question about your data field")

5. Next, convert this to a Gauge chart by expanding the **Visualizations** palette at right, and selecting on the **Gauge** control. You will need to set the **New Q&A experience** to **Off** in order to see the **Visualizations** palette. This switch is on the toolbar on the right-hand side.

    ![The Visualizations palette expand button is selected.](media/image153.png "Visualizations palette")

6. In the **Visualizations** palette, select the **Gauge** control. Select the **Format** (paint roller) icon and expand the **Gauge axis** section. Format the Gauge axis so it ranges between **0.0** and **1.0** and has a **target** (indicator) set at **0.5**.

    ![On the Visualizations palette, the Gauge graph icon is selected. Beneath that, the brush icon is selected. Under Gauge axis, the following values are defined: Min, 0. Max, 1.0. Target, 0.5.](media/image154.png "Visualizations list")

7. Your gauge should now look similar to the following:

    ![The Gauge graph for average score created between yesterday and today displays with an average of score of 0.62.](media/image155.png "Gauge graph")

8. In the top-right corner, select **Pin visual**.

    ![Pin visual option from the toolbar.](media/image156.png "Pin visual option")

9. In the dialog that appears, select the dashboard you recently created and select **Pin**.

    ![On the Pin to dashboard dialog box, on the left, a Preview of the Gauge graph displays. On the right, under Where would you like to pin to, the Existing dashboard radio button is selected.](media/image157.png "Pin to dashboard dialog box")

10. In the list of dashboards, select your **Real-time Sentiment** dashboard. Your new gauge should appear next to your original gauge. If the original gauge fills the whole screen, you may need to scroll down to see the new gauge. You can delete the original gauge if you prefer. (Select the top of the visualization, then ellipses that appear, and then, the trash can icon.)

    ![Two Average of score Gauge graphs display, and both share the same data.](media/image158.png "Gauge graphs")

11. Navigate to the chat website you deployed and send some messages and observe how the sentiment gauge updates with moments of you sending chat messages.

### Task 4: Add a trending sentiment chart to the dashboard

The sentiment visualization you created is great for getting a sense of sentiment as of this moment. However, First Up Consultants wishes to view sentiment over time for historical reference and to see whether overall sentiment is trending one way or another. To do this, we will use the tumbling window query output from Stream Analytics to display this data in a line chart.

1. While still in **Power BI**, select **My Workspace** once again, then select the **Datasets** (or **Datasets + dataflows**) tab. You should see the **TrendingSentiment** dataset dynamically created by Stream Analytics. Select the **Create Report** action.

    ![Go to My Workspace, then the Datasets tab, the TrendingSentiment dataset is listed, the Create Report button is selected next to this dataset.](media/create-report-trending-sentiment-dataset.png "Power BI Datasets")

2. Select the **Line chart** visualization.

    ![In the Visualizations palette, the Line chart visualization is selected.](media/power-bi-line-chart-visualization.png "Power BI Line Chart")

3. Drag the **average** field to the **Values** setting, and **snapshot** to **Axis**.

    ![Arrows indicating drag and drop operations show average in the Values field and snapshot in the Axis field.](media/power-bi-line-chart-settings.png "Power BI Line Chart")

4. Resize the line chart and observe how the average sentiment is tracked over time.

    ![Screenshot of the line chart displaying trending sentiment over time.](media/power-bi-trending-sentiment-chart.png "Power BI Line Chart")

5. Select **Save this report** on the top of the page. Name the report `Trending Sentiment` when prompted.

    ![The Save icon is selected from the toolbar.](media/power-bi-save-report.png "Save this report")

6. Next, select **Pin to a dashboard**.

    ![The Pin to a dashboard button is selected from the toolbar.](media/pin-to-dashboard.png "Pin to a dashboard")

7. In the **Pin to dashboard** dialog, select **Existing dashboard**, select the dashboard you created previously, then select **Pin live**.

    ![The Pin to dashboard dialog shows the report will be pinned to an existing dashboard and has the Real-time Sentiment dashboard selected in a drop down list.](media/power-bi-pin-to-dashboard.png "Pin to dashboard")

8. Position the Trending Sentiment line chart beneath the average score gauge.

    ![Power BI dashboard showing the average score gauges and trending sentiment.](media/power-bi-dashboard-with-trending-sentiment.png "Power BI Dashboard")

9. Try building out the rest of the real-time dashboard that should look as follows. We provide the following Q&A questions you can use to get started.

    ![The Power BI dashboard has four panes: two Count of Messages panes, an Average Sentiment, and Upset Users. The first Count of Messages pane displays a number (18). The second Count of Messages is a pie chart broken out by username. The Average Sentiment is a donut chart displaying the Average Sentiment (0.58) in the past 24 hours. Upset Users chart is a horizontal bar chart displaying the average of upset users (0.25) in the past 24 hours.](media/image159.png "Power BI Dashboard")

    - Count of Messages (Card visualization): Count of messages between yesterday and today.

    - Count of Messages by Username (Pie chart visualization): Count of messages by username between yesterday and today.

    - Upset Users (Bar chart visualization): Average score by username between yesterday and today.

10. Invite some peers to chat and monitor the sentiments using your new, real-time dashboard.

## Exercise 6: Enabling search indexing

Duration: 30 minutes

Now that you have primed the system with some messages, you will create a Search Index and an Indexer in Azure Search upon the messages that are collected in Azure Cosmos DB.

### Task 1: Verifying message archive

Before going further, a good thing to check is whether messages are being written to Azure Cosmos DB from the Stream Analytics Job.

1. In the **Azure Portal**, navigate to your **Azure Cosmos DB account**.

2. On the left-hand menu, select **Data Explorer**.

    ![The Data Explorer icon from the Cosmos DB left menu.](media/image160.png "Data explorer menu")

3. Under the **awhotels** Cosmos DB, select **messagestore**, then **Items**. You should see some data here.

    ![In Data Explorer, the awhotels, and messagestore items are expanded. The Items item below messagestore is selected.](media/2020-06-30-19-31-44.png "Documents selected in Data Explorer")

4. If you want to peek at the message contents, select any item in the listing.

    ![A list of messages is displayed with one selected, the JSON message displayed to the right.](media/2020-06-30-15-56-18.png "Cosmos DB Item Contents")

    > **Note**: If you don't see messages, then check for errors in MessageLogger, Outputs, Cosmos DB.  If you have to delete the collection and recreate them, make sure to stop and start the MessageLogger.  Test the connection.

    ![The output details of a Cosmos DB error is shown.](media/2019-03-21-13-45-17.png "CosmoDB output details error")

### Task 2: Creating the index and indexer

1. Select **Resource Groups** from the left menu, then select the **intelligent-analytics** resource group.

2. Select your **Search service** instance from the list.

    ![The conciergeplusapp search service icon is displayed.](media/2019-03-21-13-59-10.png "ChatSearchApi icon")

3. Select **Import data**.

    ![The Search Service toolbar has the Import data item selected.](media/image163.png "Intelligent-analytics resource group")

4. On the **Import data** blade, select **Connect to your data**.

    ![The import data blade is shown with the Connect to your data selected.](media/2019-03-21-14-00-40.png "Import data blade")

5. For **Data Source**, select **Azure Cosmos DB** from the dropdown.

6. Enter `messagestore` for the **Data Source Name**.

7. **Connection string**: Select the **Choose an existing connection**, and select the **awhotelcosmosdb-namespace** database. This will auto-populate the connection string.

8. For **Database**, select your **awhotels** database.

9. For **Collection**, choose your **messagestore** collection.

    ![Import data form is displayed populated with the preceding values.](media/2019-03-21-14-12-53.png "Display all of the fields")

10. Select the **Next: Add cognitive skills (Optional)** button.

    ![The Next: Add cognitive skills button.](media/add-cognitive-skills.png "Next button")

    Select **Skip to: Customize target index**.

    ![The Skip to: Customize target index button.](media/2019-03-21-14-20-45.png "Skip to Customize target index")

11. Select **Customize target index**, and observe that the field list has been pre-populated for you based on data in the collection.

12. Enter `chatmessages` for the name of the index. Capture this value for later web application configuration.

13. Set the Key to **id**

    ![The Key field is set to id.](media/image167.png 'Key ID field')

14. Select the **Retrievable** check box for the following fields: **message, createDate**, and **username** (id will be selected automatically). Only these fields will be returned in query results.

15. Select the **Filterable** check box for **createDate, username**, and **sessionId**. These fields can be used with the filter clause only (not used by this Tutorial, but useful to have).

16. Select the **Sortable** check box for **createDate**, **username**, and **sessionId**. These fields can be used to sort the results of a query.

17. Select the **Searchable** check box for **message**. Only this field will be indexed for full text search.

18. Confirm your grid looks similar to the following, and select **Next: Create an Indexer**.

    ![On the Import data blade, the Customize target index tab is selected. The Index blade fields and check boxes are set to previously mentioned settings.](media/2019-03-21-14-26-25.png "Import data and Index blades")

19. On the **Create an Indexer** blade, enter `messages-indexer` as the name.

20. Set the **Schedule** toggle to **Custom**.

21. Enter an **interval** of **5** minutes (the minimum allowed).

22. Keep **Start time** as the default value, which is the current date and time.

23. The description and other fields can be ignored.

24. Select **Submit** to begin importing data using your indexer.

25. You should test your index and configure it to be searchable by client applications. After a few moments, examine the Indexers tile for the status of the Indexer.

    ![The Indexers tile shows that there was one success, and zero failed.](media/image171.png "Indexers tile")

    You should see your messages indexed.

    ![In the Search service Overview screen, the Indexes tab is selected. The chatmessages index is listed and displays number of documents indexed.](media/2019-03-21-16-22-06.png "Documents Indexed")

    Select the **chatmessages** index.  You can test the searches by entering values in the **Query string** text box. Enter `search=test` into the Query string box and select **Search**.

    ![Testing the chat message search, the term search=test is entered in the Query string textbox and the results of the search are shown in JSON format."](media/2019-03-21-16-24-18.png "Testing Search Indexes")

    Select the **CORS** tab. Select **All** for the option. Press the Save button.

    ![On the Index screen, the CORS tab is selected. The Allowed origin type is set to All.](media/2019-06-22-17-59-36.png "CORS Options")

    > **Note**: The **All** setting allows search requests from other client applications to successfully execute. For a production application, you would choose the **Custom** option and enter the domain you will be receiving requests from.

26. We need to capture the index query api key for the Azure Web App configuration.

    - On the **Search service** blade, select **Keys** on the left-hand menu. Capture the **Primary admin key**. The value will be used for the ChatSearchApiKey.

    ![The Azure Cognitive Search Service key configuration screen is displayed.](media/2020-06-30-16-12-38.png "Display Search Keys")

### Task 3: Update the Chat Web App Configuration

1. Navigate to your web app.

2. Select the configuration blade. We are going to add the following values:

    ```text
    ChatSearchApiBase
    ChatSearchApiIndexName (chatmessages)
    ChatSearchApiKey
    ```

3. For the `ChatSearchApiBase` key, enter the URI of the Search App (e.g., <https://conciergeplusappsearchth.search.windows.net)>.

    - You can find this by going to **Resource Groups**, selecting the **intelligent-analytics** resource group, and selecting your **search app service** from the list.

    ![The Azure Search Service overview is displayed. On the right hand side, the Url value is highlighted.](media/2019-09-08-15-35-46.png "Search Service URL Displayed")

4. Save the web application configuration.

### Task 4: Re-publish web app

1. Publish the updated **ChatWebApp** using Visual Studio, as was shown previously in [Exercise 4, Task 3](#task-3-publish-the-chatwebapp).

2. When the publishing is complete, a browser window should appear.

3. Navigate to the **Search** tab on the deployed Web App and try searching for chat messages.

    ![In the Search Messages box, in the Search messages for text box, Hi is typed. Below that, 32 results have been found.](media/2020-06-30-18-55-16.png "Search Messages box")

<!--

## Exercise 7: Add a bot using Bot service and QnA Maker

Duration: 30 minutes

At this point, you have created a real-time chat service in Azure, allowing people to interact with one another. Now we will build a bot that will automatically respond to user questions, helping take the load off the hotel staff.

### Task 1: Create a QnA service instance in Azure

Microsoft's QnAMaker is a Cognitive Service tool that uses your existing content to build and train a simple question and answer bot that responds to users in a natural, conversational way.

1. In a new web browser window, navigate to <https://www.qnamaker.ai>.

2. On the home page, select **Sign In** on the top right of the page. Sign in with the same credentials you use for the [Azure portal](https://portal.azure.com).

    ![Microsoft QnA Maker home page is displayed, the Sign In link in the upper right corner of the screen is highlighted.](media/qna-maker-home.png "QnA Maker home page")

3. Select **Create a knowledge base**.

    ![The QnA Maker dashboard is displayed. It indicates that there are no knowledge bases. The Create a knowledge base item is selected from the toolbar menu.](media/qna-maker-create-kb-link.png "Select create a knowledge base")

4. Within the **knowledge base creation** page, select **Create a QnA Service** under Step 1. Ensure that you configure the **Stable** tier.

    ![Step 1 indicates to Create a QnA service in Microsoft Azure. There is a button labeled Create a QnA Service. It is configured with the Stable tier.](media/create-qna-maker-stable.png "Knowledge base creation page")

5. Within the **Create QnA Maker** form in Azure, provide the following:

    - **Subscription**: Choose the same subscription you used previously.

    - **Resource Group**: Choose the **intelligent-analytics** resource group.

    - **Name**: Provide a **unique name** for the QnA Maker Service (e.g., `awhotel-qna`).

    - **Pricing tier**: Choose **Free F0 (3 managed documents per month ...**

    - **Azure Search location**: Choose the **same location** you used previously. If the region you've been using isn't available, select a different location for this resource.

    - **Azure Search pricing tier**: Choose **Free F (3 Indexes)**.

    - **App name**: Provide a **unique name** for the QnA Maker Service (e.g., `awhotel-qna`).

    - **Website location**: Choose the **same location** you used previously. If the region you've been using isn't available, select a different location for this resource.

    - **App insights**: Select **Disable**.

    ![The Create QnA Maker form is shown populated with the preceding values.](media/qna-maker-details.png "Create QnA Maker")

6. Select **Review + create** and **Create**.

7. Once the service has been created, switch back to the browser tab with the **QnA Maker knowledge base creation** page and select the **Refresh** button in the **Step 2** section.

8. Underneath Step 2, select your **Microsoft Azure Directory ID** under which you created the QnA Maker service, select the **Azure subscription name**, select the **Azure QnA service**, and **English** as the **Language**.

    ![Connect your QnA service to your KB form is displayed populated with the preceding values.](media/qna-maker-connect-qna-service.png "Azure QnA service")

9.  Underneath Step 3 (Name your KB), provide a unique name, such as `ConciergePlus`.

10. Underneath Step 4 (Populate your KB), select **+ Add file**. [Upload this file](lab-files/faq.xlsx) to the service.

    ![On STEP 4 Populate your KB, the +Add file button is selected.](media/create-qna-maker-add-file.png "Knowledge base creation page")

11. Finally, underneath Step 5 (Create your KB), select **Create your KB** (keep **Chit-chat** at its default, **None**).

    ![The uploaded file from the previous step is highlighted in the File Name section of Step 4. Step 5, Create your KB, has the Create your KB button selected.](media/qna-maker-create-kb.png "Knowledge base creation page")

12. When the KB is being created, the Knowledge base will be displayed in the window. It takes a few seconds for the extraction process to read the Excel document and identify questions and answers.

    ![The Knowledge base screen displays a sample QnA question. Are pets allowed?](media/2019-11-25-05-37-13.png "Sample QnA Question")

13. Select **+ Add QnA pair** in the toolbar to add a new row in the **Editorial** section of the Knowledge Base. Enter `Hi` into the **Question** field and `Hello. Ask me questions about the hotel.` into the **Answer** field of the new row you created.

    ![On the Knowledge base screen, + Add QnA pair is selected from the toolbar. In the Source: Editorial context section, the question Hi that has the answer Hello. Ask me questions about the hotel. is highlighted.](media/qna-maker-created-kb.png "Knowledge base")

14. Choose **Save and train** button on toolbar of the page. This will save your changes and train the bot how to respond to questions, given the information you imported.

15. Once your changes have been saved, select **Test** at the top of the page. Try typing `hi` and press enter. You should see the **Hello. Ask me questions about the hotel.** response. Experiment with asking different questions.

    ![A test session is displayed with sample messages and responses.](media/qna-maker-test.png "QnA Maker Test")

16. Select **Inspect** underneath one of your test questions. The **Inspect** pane will appear, showing the question you typed, the answer, and a confidence score. This pane provides you an easy way to add alternate phrasing or change the answer. Choose **Save and train**. Select the **Test** button to hide the testing pane.

    ![The inspect link from one of the QnA test responses is selected. The Inspect blade is displays a Question pane with the question and the ability to add alternate phrasing and an Answer pane that shows the appropriate answer and Confidence score. There is also the ability to enter a new answer on this pane.](media/qna-maker-inspect.png "QnA Maker Inspect")

17. Select **Publish** on top of the page. In the publish page that appears, select the **Publish** button.

    ![The Concierge Plus Publish screen is displayed with the Publish button selected.](media/qna-maker-publish.png "QnA Maker Publish")

### Task 2: Create a QnA bot

1. Select the **Create Bot** button.

    ![On the Successful deployment screen, the Create Bot button is selected.](media/2019-09-09-14-34-41.png "Success")

2. Enter the **Web App Bot** configuration as follows:

    - **Bot handle**: Enter the Bot name, e.g. `awhotel-qna-bot`.
    - **Subscription**: Select your subscription.
    - **Resource Group**: Select **intelligent-analytics**.
    - **Location**: Select the region you've been using throughout this lab.
    - **Pricing tier**: Select **F0 (10K Premium Messages)**.
    - **App name**: This will be defaulted to the same name as the **Bot handle**.
    - **Application Insights**: Set to **Off**.

    > **Note**: Do not change the QnA Auth Key.
    
    > **Note**: You may receive a message that the Resource provider 'Microsoft.BotService' is not registered from the subscription. If this is the case, it can be rectified by following [one of these solutions (choose 1)](https://docs.microsoft.com/en-us/azure/azure-resource-manager/templates/error-register-resource-provider).

    ![The Web App Bot form is displayed and is populated with the values described above.](media/2019-09-09-14-40-26.png "Web App Bot Configuration")

3. Select the **Create** button.

4. Choose your new **QnA Web App Bot** from the resource group.

    ![The Web App Bot resource is selected in the intelligent-analytics resource group.](media/2019-09-09-14-49-19.png "QnA bot resource")

5. Test out the bot by selecting **Test in Web Chat** on the left-hand menu (it may take a couple minutes to appear the first time). Type in a few questions to make sure it responds as expected.

    ![In the Web App Bot screen, Test in Web Chat is selected from the left menu. In the Test blade, messages and responses are displayed. The Type your message textbox is highlighted at the bottom of the Test blade.](media/test-bot-azure-portal.png "Function Bot Test")

6. Select **Bot profile** from the left-hand menu. Change the display name to something like `Concierge+ Bot`, then select **Apply**.

    ![In the Web App Bot screen, Bot profile is selected from the left menu. In the Bot profile form the display name shows Concierge+ Bot.](media/change-bot-display-name.png "Function Bot Profile")

7. Select **Channels** from the left-hand menu, then select **Get bot embed codes** underneath the **Web Chat channel**.

    ![Select Get bot embed codes](media/function-bot-channels.png "Function Bot Channels")

8. A dialog will appear for the embed codes. Select the **Click here to open the Web Chat configuration page** option.

9. Select **Copy** next to the **Embed code** textbox. Paste that value to notepad or other text application. Select **Show** beside the first Secret key. Copy the value and replace **YOUR_SECRET_HERE** within the embed code with that secret value. Example: `<iframe src='https://webchat.botframework.com/embed/awhotel-qna-bot-skm?s=EnZZZrVOZjY.KujPgaoTtfxSAMObBCWLTPnvibXExfrXOY3d82nhcMI'  style='min-width: 400px; width: 100%; min-height: 500px;'></iframe>`.

    ![The Configure Web Chat screen is displayed. The Embed code is located in a textbox with the Copy button next to it selected. In the Secret keys section, the Show button next to the first textbox is highlighted.](media/function-bot-embed.png "Function Bot Embed")

### Task 3: Embed the bot into your web app

1. Open **Visual Studio** and open **Bot.cshtml** located within the **Views\Home** folder of the **ChatWebApp**.

    ![In Visual Studio Solution Explorer, the ChatWebApp project is expanded. The Views and Home folders are expanded and the Bot.cshtml is selected.](media/2020-06-30-18-58-47.png "Visual Studio")

2. Find `<!-- PASTE YOUR BOT EMBED CODE HERE -->` within the page and paste your iframe embed code on a new line beneath.

3. Modify the iframe style values to match as follows. The iframe code should look like:

    ```html
    <!-- PASTE YOUR BOT EMBED CODE HERE -->
    <iframe src='YOUR_SOURCE' style='min-width: 400px; width: 100%; height: 300px;'></iframe>
    ```

    ![In Visual Studio, the contents of the Bot.cshtml page is displayed with the embed code highlighted.](media/vs-bot-embed.png "Visual Studio")

4. **Publish** your web app.

5. After the web app has been published, navigate to it by selecting the **Bot** menu item. Type in a few questions to ensure the bot is functioning correctly.

    ![The Bot page of the web app is displayed with a chat interface where messages can be answered. Sample questions and responses are displayed.](media/bot-service-embedded.png "Bot page")
    
  -->

## After the hands-on lab

Duration: 10 minutes

In this exercise, attendees will deprovision any Azure resources that were created in support of the lab.

### Task 1: Delete the resource group

1. Using the Azure portal, navigate to the Resource group you used throughout this hands-on lab by selecting Resource groups in the left menu.

2. Search for the name of your research group and select it from the list.

3. Select Delete in the command bar and confirm the deletion by re-typing the Resource group name and selecting Delete.

4. Power BI - Delete **Real-time Sentiment** workspace.

5. LUIS - <https://www.luis.ai/applications>.  Delete the **awchat** app.

You should follow all steps provided _after_ attending the Hands-on lab.
