![Microsoft Cloud Workshops](https://github.com/Microsoft/MCW-Template-Cloud-Workshop/raw/master/Media/ms-cloud-workshop.png "Microsoft Cloud Workshops")

<div class="MCWHeader1">
Intelligent analytics
</div>

<div class="MCWHeader2">
Before the hands-on lab setup guide
</div>

<div class="MCWHeader3">
August 2018
</div>

Information in this document, including URL and other Internet Web site references, is subject to change without notice. Unless otherwise noted, the example companies, organizations, products, domain names, e-mail addresses, logos, people, places, and events depicted herein are fictitious, and no association with any real company, organization, product, domain name, e-mail address, logo, person, place or event is intended or should be inferred. Complying with all applicable copyright laws is the responsibility of the user. Without limiting the rights under copyright, no part of this document may be reproduced, stored in or introduced into a retrieval system, or transmitted in any form or by any means (electronic, mechanical, photocopying, recording, or otherwise), or for any purpose, without the express written permission of Microsoft Corporation.

The names of manufacturers, products, or URLs are provided for informational purposes only and Microsoft makes no representations and warranties, either expressed, implied, or statutory, regarding these manufacturers or the use of the products with any Microsoft technologies. The inclusion of a manufacturer or product does not imply endorsement of Microsoft of the manufacturer or product. Links may be provided to third party sites. Such sites are not under the control of Microsoft and Microsoft is not responsible for the contents of any linked site or any link contained in a linked site, or any changes or updates to such sites. Microsoft is not responsible for webcasting or any other form of transmission received from any linked site. Microsoft is providing these links to you only as a convenience, and the inclusion of any link does not imply endorsement of Microsoft of the site or the products contained therein.
© 2018 Microsoft Corporation. All rights reserved.

Microsoft and the trademarks listed at <https://www.microsoft.com/legal/intellectualproperty/Trademarks/Usage/General.aspx> are trademarks of the Microsoft group of companies. All other trademarks are property of their respective owners.

## Requirements

- Microsoft Azure subscription must be pay-as-you-go or MSDN
  - Trial subscriptions will not work
- A virtual machine configured with:
  - Visual Studio Community 2017 or greater, version 15.7 or later (<https://www.visualstudio.com/vs/>)
  - Azure SDK 2.9 or later (Included with Visual Studio 2017)

## Before the hands-on lab

Duration: 20 minutes

Synopsis: In this exercise, you will set up your environment for use in the rest of the hands-on lab. You should follow all the steps provided in the Before the Hands-on Lab section to prepare your environment before attending the hands-on lab.

### Task 1: Provision Power BI

If you do not already have a Power BI account:

1. Go to <https://powerbi.microsoft.com/features/>

2. Scroll down until you see the Try Power BI for free! section of the page, and select the Try Free\> button ![Screenshot of the Power BI Try for free section.](media/setup3.png 'Power BI Try for free section')

3. On the page, enter your work email address (which should be the same account as the one you use for your Azure subscription), and select Sign up ![The Get started page has a field for entering your work email address.](media/setup4.png 'Get started page')

4. Follow the on-screen prompts, and your Power BI environment should be ready within minutes. You can always return to it via <https://app.powerbi.com/>

### Task 2: Setup a lab virtual machine (VM)

1. In the [Azure Portal](https://portal.azure.com/), select +Create a resource, then type "Visual Studio" into the search bar. Select Visual Studio Community 2017 on Windows Server 2016 (x64) from the results. ![In the Azure Portal Everything section, under Results, under Name, Visual Studio Community 2017 on Windows Server 2016 is circled.](media/setup5.png 'Azure Portal Everything section')

2. On the blade that comes up, at the bottom, ensure the deployment model is set to Resource Manager, and select Create

    ![At the Bottom of the blade, Resource Manager is selected as the deployment model.](media/setup6.png 'Bottom of the blade')

3. Set the following configuration on the Basics tab

    - Name: Enter **LabVM**

    - VM disk type: Select **SSD**

    - User name: Enter **demouser**

    - Password: Enter **Password.1!!**

    - Subscription: Select the subscription you are using for this hands-on lab

    - Resource Group: Select Create new, and enter **intelligent-analytics** as the name of the new resource group

    - Location: Select a region close to you

    ![The Basics blade fields fields display the previously mentioned settings.](media/setup7.png 'Basics blade')

4. Select **OK** to move to the next step

5. On the Choose a size blade, select **DS2_V3 Standard**

    ![The Choose a size blade has the D2S_V3 Standard option selected](media/setup-vm-size.png 'Choose a size blade')

6. Choose **Select** to move on to the Settings blade

7. On the Settings blade, select **RDP (3389)** from the Select public inbound ports drop down, then select **OK**

    ![Select RDP (3389) from the Select public inbound ports drop down](media/setup-vm-settings.png 'Setting blade')

8. Select Create on the Create blade to provision the virtual machine

    ![The Create blade shows that validation passed, and provides the offer details.](media/setup9.png 'Create blade')

9. It may take 10+ minutes for the virtual machine to complete provisioning