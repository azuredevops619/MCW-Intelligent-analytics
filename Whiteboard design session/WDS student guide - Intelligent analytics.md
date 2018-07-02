![](../media/ms-cloud-workshop.png 'Microsoft Cloud Workshops')

Information in this document, including URL and other Internet Web site references, is subject to change without notice. Unless otherwise noted, the example companies, organizations, products, domain names, e-mail addresses, logos, people, places, and events depicted herein are fictitious, and no association with any real company, organization, product, domain name, e-mail address, logo, person, place or event is intended or should be inferred. Complying with all applicable copyright laws is the responsibility of the user. Without limiting the rights under copyright, no part of this document may be reproduced, stored in or introduced into a retrieval system, or transmitted in any form or by any means (electronic, mechanical, photocopying, recording, or otherwise), or for any purpose, without the express written permission of Microsoft Corporation.

Microsoft may have patents, patent applications, trademarks, copyrights, or other intellectual property rights covering subject matter in this document. Except as expressly provided in any written license agreement from Microsoft, the furnishing of this document does not give you any license to these patents, trademarks, copyrights, or other intellectual property.

The names of manufacturers, products, or URLs are provided for informational purposes only and Microsoft makes no representations and warranties, either expressed, implied, or statutory, regarding these manufacturers or the use of the products with any Microsoft technologies. The inclusion of a manufacturer or product does not imply endorsement of Microsoft of the manufacturer or product. Links may be provided to third party sites. Such sites are not under the control of Microsoft and Microsoft is not responsible for the contents of any linked site or any link contained in a linked site, or any changes or updates to such sites. Microsoft is not responsible for webcasting or any other form of transmission received from any linked site. Microsoft is providing these links to you only as a convenience, and the inclusion of any link does not imply endorsement of Microsoft of the site or the products contained therein.

© 2018 Microsoft Corporation. All rights reserved.

Microsoft and the trademarks listed at https://www.microsoft.com/legal/intellectualproperty/Trademarks/Usage/General.aspx are trademarks of the Microsoft group of companies. All other trademarks are property of their respective owners.

May 2018

**Contents**

<!-- TOC -->

- [Intelligent analytics whiteboard design session student guide](#intelligent-analytics-whiteboard-design-session-student-guide)
  - [Abstract and learning objectives](#abstract-and-learning-objectives)
  - [Step 1: Review the customer case study](#step-1-review-the-customer-case-study)
    - [Customer situation](#customer-situation)
    - [Customer needs](#customer-needs)
    - [Customer objections](#customer-objections)
    - [Infographic of common scenarios](#infographic-of-common-scenarios)
  - [Step 2: Design a proof of concept solution](#step-2-design-a-proof-of-concept-solution)
  - [Step 3: Present the solution](#step-3-present-the-solution)
  - [Wrap-up](#wrap-up)
  - [Additional references](#additional-references)

<!-- /TOC -->

# Intelligent analytics whiteboard design session student guide

## Abstract and learning objectives

In this whiteboard design session, you will work with a group to design a solution for building a real-time chat pipeline, incorporating machine learning and analytics to detect and visualize customer sentiment. You will also design a lambda architecture to handle both real-time chat processing and data archiving and search indexing for analyzing all data flowing through the system. Finally, you will determine whether a bot can be incorporated in the solution, and how it fits alongside the messaging capabilities.

## Step 1: Review the customer case study

**Outcome**

Analyze your customer’s needs.

Timeframe: 15 minutes

Directions: With all participants in the session, the facilitator/SME presents an overview of the customer case study along with technical tips.

1.  Meet your table participants and trainer
2.  Read all of the directions for steps 1–3 in the student guide
3.  As a table team, review the following customer case study

### Customer situation

AdventureWorks Travel (AWT) specializes in building software solutions for the hospitality industry. They are designing their latest product, an enterprise grade, social chat app called Concierge+. The mobile friendly web app is intended to enable guests to easily stay in touch with the concierge and other guests, enabling greater personalization and improving their experience during their stay.

The original requirements for the product were to support:

- One-on-one chat sessions between a hotel guest and a hotel concierge

- A public chat room for hotel guests and hotel staff, called the Hotel Lobby

- A bot that can automatically answer guest questions about the hotel

- Full-text message search, including via \#hashtags and \@usernames

AdventureWorks wants to build a solution that is both scalable and extensible. According to Marc Tripp, the CTO of AdventureWorks: "We want it scalable in the sense that it could support the chat requirements of the largest hotels in the world, currently with as many as 7,200 rooms. While we don't anticipate having any single chat room with a thousand guests or 2,000 concurrent one-on-one chats between a guest and the concierge staff, we want a solution that could handle that if it needed to."

Extensible in the sense that they can add new features on top of the solid, baseline real-time messaging functionality. For example, while they are starting by supporting search (for their backend) across chat messages, they already have a set of extensions they know they want to perform.

An important extensibility point for them is a way for them to gather the sentiment of their guests as they interact in the public chat rooms and with the concierge. Hotel operators are motivated to keep tabs on guest sentiment in real-time, so they can respond to any upset guests quickly and turn a miserable stay into an amazing stay. To this end, they would like a dashboard (that updates in real-time) showing the volume of chat messages flowing thru their system, a pie chart showing the most active users at a glance, a bar chart highlighting upset users (who need to be addressed ASAP), and some form of gauge showing the average real-time sentiment for window of time (e.g., the last hour, last 24 hours). They would also like to view trending sentiment over time, alongside the real-time sentiment data.

While guest sentiment is important, it is a reactive measure. AdventureWorks would like to take a proactive approach in positively affecting sentiment by expediting the requests of their guests via chat. In particular, they are looking to experiment with automating the routing of routine guest requests (e.g., "Can I get more towels?", "I forgot my toothbrush" and "Can I get a bottle of champagne") that would otherwise require the attention of an already overloaded front desk attendant. These requests, once automatically routed, could be sent directly to housekeeping or room service as is most appropriate. AdventureWorks has heard of active machine learning\-- whereby the system improves constantly with use, while still knowing what it is unsure of and asking for help when it determines it needs assistance.

Another way they would like to be proactive and reduce load on hotel staff, is to have a bot that can answer guest questions about the hotel. This is something that can be separate from the real-time chat, and it is a feature that they would like automated as much as possible.

Finally, to help them reflect on chats that occur in public chat rooms, they would like to log the messages to a durable store and make them full text searchable. In the future, they are thinking of using this same approach to enable Twitter-like search for \@entities and \#labels.

### Customer needs

1.  AdventureWorks would like their Concierge+ service to avoid using any servers or VMs that they would have to maintain

2.  Their real-time chat solution needs to be scalable to support their largest hotel customers

3.  Automatically respond to guest questions with a bot

4.  The chat solution needs to be extensible and provide support for sentiment analysis and contextual understanding

5.  The public chat history needs to be fully searchable

6.  The dashboard they use to visualize sentiment needs to update in real time, as well as display trending sentiment over time

### Customer objections

1.  It is not clear if we should be using the Bot Framework for our request forwarding or something else?

2.  We do not want to build our own machine learning models in order to detect the sentiment of chat in real time

3.  Can we really build a real-time, intelligent chat solution entirely in Azure?

### Infographic of common scenarios

![Screenshot of a sample Internet of Things workflow, which is broken into On-Premises and Azure services.](media/image2.png 'Internet of Things workflow')

## Step 2: Design a proof of concept solution

**Outcome**

Design a solution and prepare to present the solution to the target customer audience in a 15-minute chalk-talk format.

Timeframe: 60 minutes

**Business needs**

Directions: With all participants at your table, answer the following questions and list the answers on a flip chart.

1.  Who should you present this solution to? Who is your target customer audience? Who are the decision makers?
2.  What customer business needs do you need to address with your solution?

**Design**

Directions: With all participants at your table, respond to the following questions on a flip chart.

_High-level architecture_

1.  Without getting into the details (the following sections will address the particular details), diagram your initial vision for handling the top-level requirements for supporting the baseline chat, sentiment analysis, and request forwarding

_Baseline chat_

1.  How would you recommend that AdventureWorks receive message from mobile and desktop browsers?

2.  How would you store ingested messages? Would you use Event Hubs or Service Bus? Be specific on your reasoning and how you would configure it.

3.  How would you forward messages to the intermediate storage from which the recipients will receive them? Would you use Stream Analytics or Azure Functions?

4.  What queued storage would you use for the recipients and why? Be specific on your reasoning and how you would configure it.

5.  Given your choice of intermediate storage, how would you implement a public chat room? How would you implement one on one chat?

_Sentiment Analysis_

1.  What service would you recommend AdventureWorks capitalize on in order to scalably apply a sentiment score to each message as it enters the system?

2.  How would you enhance your baseline chat flow to incorporate this sentiment processing?

_Request forwarding_

1.  What Azure service or API would you suggest AdventureWorks utilize for understanding how to route guest requests to housekeeping or room service?

2.  How would you implement or configure this service?

3.  How would you integrate this service into your chat message processing flow?

_Q&A Bot_

1.  What Azure services would you recommend AdventureWorks use for creating a Q&A bot?

_Message search_

1.  What Azure services would you use to durably store the messages and enable them for full text search? How would you extend your messaging pipeline so that all messages get archived after they have been tagged with sentiment?

_Visualization and reporting_

1.  What tool would you recommend AdventureWorks utilize for constructing their real-time sentiment dashboard?

2.  How would you build this dashboard using the tool you recommended?

3.  How would you show display the trending sentiment data over time within the dashboard?

**Prepare**

Directions: With all participants at your table:

1.  Identify any customer needs that are not addressed with the proposed solution
2.  Identify the benefits of your solution
3.  Determine how you will respond to the customer’s objections

Prepare a 15-minute chalk-talk style presentation to the customer.

## Step 3: Present the solution

**Outcome**

Present a solution to the target customer audience in a 15-minute chalk-talk format.

Timeframe: 30 minutes

**Presentation**

Directions:

1.  Pair with another table
2.  One table is the Microsoft team and the other table is the customer
3.  The Microsoft team presents their proposed solution to the customer
4.  The customer makes one of the objections from the list of objections
5.  The Microsoft team responds to the objection
6.  The customer team gives feedback to the Microsoft team
7.  Tables switch roles and repeat Steps 2–6

## Wrap-up

Timeframe: 15 minutes

Directions: Tables reconvene with the larger group to hear the facilitator/SME share the preferred solution for the case study.

## Additional references

|                   |                                                                                                                        |
| ----------------- | :--------------------------------------------------------------------------------------------------------------------: |
| **Description**   |                                                       **Links**                                                        |
| Infographic       |                 <https://www.microsoft.com/legal/intellectualproperty/Trademarks/Usage/General.aspx/>                  |
| Service Bus       |                     <https://azure.microsoft.com/documentation/articles/event-hubs-api-overview/>                      |
| Event Hubs        |    <https://azure.microsoft.com/documentation/articles/stream-analytics-define-outputs/#event-processor-host-apis/>    |
| Stream Analytics  |                 <https://azure.microsoft.com/documentation/articles/hdinsight-apache-spark-overview/>                  |
| Power BI          |                                   <https://powerbi.microsoft.com/what-is-power-bi/>                                    |
| Cosmos DB         |                                     <https://docs.microsoft.com/azure/cosmos-db/>                                      |
| QnA Maker         |                    <https://docs.microsoft.com/azure/cognitive-services/QnAMaker/overview/overview>                    |
| Azure Bot Service | <https://docs.microsoft.com/azure/bot-service/bot-service-scenario-enterprise-productivity?view=azure-bot-service-3.0> |
| Azure Functions   |                         <https://docs.microsoft.com/azure/azure-functions/functions-overview>                          |
