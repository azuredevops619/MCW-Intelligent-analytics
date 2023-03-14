![Microsoft Cloud Workshops](https://github.com/Microsoft/MCW-Template-Cloud-Workshop/raw/main/Media/ms-cloud-workshop.png "Microsoft Cloud Workshops")

<div class="MCWHeader1">
Intelligent analytics
</div>

<div class="MCWHeader2">
Before the hands-on lab setup guide
</div>

<div class="MCWHeader3">
June 2021
</div>

Information in this document, including URL and other Internet Web site references, is subject to change without notice. Unless otherwise noted, the example companies, organizations, products, domain names, e-mail addresses, logos, people, places, and events depicted herein are fictitious, and no association with any real company, organization, product, domain name, e-mail address, logo, person, place or event is intended or should be inferred. Complying with all applicable copyright laws is the responsibility of the user. Without limiting the rights under copyright, no part of this document may be reproduced, stored in or introduced into a retrieval system, or transmitted in any form or by any means (electronic, mechanical, photocopying, recording, or otherwise), or for any purpose, without the express written permission of Microsoft Corporation.

The names of manufacturers, products, or URLs are provided for informational purposes only and Microsoft makes no representations and warranties, either expressed, implied, or statutory, regarding these manufacturers or the use of the products with any Microsoft technologies. The inclusion of a manufacturer or product does not imply endorsement of Microsoft of the manufacturer or product. Links may be provided to third party sites. Such sites are not under the control of Microsoft and Microsoft is not responsible for the contents of any linked site or any link contained in a linked site, or any changes or updates to such sites. Microsoft is not responsible for webcasting or any other form of transmission received from any linked site. Microsoft is providing these links to you only as a convenience, and the inclusion of any link does not imply endorsement of Microsoft of the site or the products contained therein.

© 2020 Microsoft Corporation. All rights reserved.

Microsoft and the trademarks listed at <https://www.microsoft.com/legal/intellectualproperty/Trademarks/Usage/General.aspx> are trademarks of the Microsoft group of companies. All other trademarks are property of their respective owners.

**Contents**

<!-- TOC -->

- [Intelligent analytics before the hands-on lab setup guide](#intelligent-analytics-before-the-hands-on-lab-setup-guide)
  - [Requirements](#requirements)
  - [Before the hands-on lab](#before-the-hands-on-lab)
    - [Task: Setup a lab virtual machine](#task-setup-a-lab-virtual-machine)
    - [Task: Create a Power BI account (optional)](#task-create-a-power-bi-account-optional)
<!-- /TOC -->

# Intelligent analytics before the hands-on lab setup guide

## Requirements

- Microsoft Azure subscription must be pay-as-you-go, Azure Pass or MSDN.
  - Trial subscriptions will **not** work.
  - You will need rights to create an Azure Active Directory application and service principal and assign roles on your subscription.
- A virtual machine configured with:
  - Visual Studio Community 2019 (latest release) (<https://www.visualstudio.com/vs/>).

## Before the hands-on lab

Duration: 20 minutes

Synopsis: In this exercise, you will set up your environment for use in the rest of the hands-on lab. You should follow all the steps provided in the Before the Hands-on Lab section to prepare your environment before attending the hands-on lab.

### Task: Setup a lab virtual machine

1. In the [Azure Portal](https://portal.azure.com/), select **+Create a resource** from the left menu, then type `Visual Studio` into the search bar. Then select **Visual Studio Community 2019(latest release) on Windows Server 2019 (x64)** from the results. Note: Do not select enterprise edition. 

    ![In the Azure Portal, Visual Studio 2019 Latest is entered into the search textbox. Visual Studio 2019 Latest is displayed in the Search suggestions.](media/2019-06-19-15-05-08.png "Visual Studio 2019 Latest option is displayed")

    ![The product page for Visual Studio 2019 Latest is displayed. The Select a software plan dropdown list is expanded showing a list of possible VM images.  Visual Studio Community 2019 (latest release) on Windows 2019 (x64) is highlighted.](media/2019-09-03-12-22-16.png "Visual Studio Community 2019 (latest release) on Windows 2019 (x64) selected")

2. Select the **Create** button.

3. Set the following configuration on the Basics tab:

    - **Subscription**: (Your Subscription) Select the subscription you are using for this hands-on lab.

    - **Resource Group**: Select the **Create new** link, and enter `intelligent-analytics` as the name of the new resource group.

    - **Virtual Machine Name**: Enter `LabVM`

    - **Region**: East US

    - **Availability Options**:  Leave the availability option as **No infrastructure redundancy required**

    - **Image**: Select **Visual Studio 2019 Community (latest release) on Windows Server 2019 (x64)**

    - **Size**: Select the **See all sizes** link, and choose **Standard D2s v3** or any other suitable size. 

    - **Username**: `demouser`

    - **Password**: (your password)

    - **Public inbound ports**: **Allow selected ports**

    - **Select inbound ports**: Select **RDP (3389)**.
    
    ![The Create virtual machine, Disks tab is displayed configured with the settings outlined above.](update/ScreenShot00937.png "Create a Virtual Machine")
    
4. Set the following configuration on the Disks tab:

    - **OS disk type**: Select **Standard SSD**.

    - **Advanced** - Use managed disks: **Yes**

    ![The Create virtual machine, Disks tab is displayed configured with the settings outlined above.](update/ScreenShot00936.png "Create a Virtual Machine")

5. Select **Next: Networking**. Leave defaults. 

    ![The Create virtual machine, Disks tab is displayed with the Next: Networking button selected.](media/2019-03-20-11-18-33.png "Review the next blade - Networking.")

6. Leave defaults in all other tabs. 

    ![The Networking tab form is displayed with default values, including a new Virtual network, intelligent-analytics-vnet, and a new Subnet, default(10.0.1.0/24). The NIC network security group is set to Basic, and Public inbound ports is set to Allow selected ports. The Select inbound ports is set to RDP. Accelerated networking is set to Off and Load balancing is set to No.](media/2019-03-20-11-20-21.png "Networking tab - Configure Virtual Networks")

7. Select the **Review + create** button.

    ![The Review + create button displayed.](media/2019-03-20-11-23-20.png "Review and create button")

8. Azure will validate your settings.  If everything is valid, then select **Create**.

    ![A Validation passed success message.](media/2019-03-20-15-18-30.png "Validation passed")

### Task: Create a PowerBI user in Azure AD

1. Goto **Users** in Azure Active Directory.

    ![The Review + create button displayed.](update/ScreenShot00938.png "Review and create button")
2. Click on Add user.
3. Name the user as **powerbi**

    ![The Review + create button displayed.](update/ScreenShot00939.png "Review and create button")
    
4. Fill up other necessary information.
5.  In **Groups and roles** section, Select **Global administrator** role for powerBI user.

    ![The Review + create button displayed.](update/ScreenShot00941.png "Review and create button")
    
6.  Don't forget to copy the temp password of your powerbi user.
7.  Create the user. 

### Task: Give rights to powerBI user on the subscription. 

1. Seach for and goto **Subscriptions** page. Select your subscription. 

    ![The Review + create button displayed.](update/ScreenShot00943.png "Review and create button")
    
2. Click on Access control (IAM) -> Add -> Add role assignment 

    ![The Review + create button displayed.](update/ScreenShot00944.png "Review and create button")
    
3. Select Role -> Owner -> Next

    ![The Review + create button displayed.](update/ScreenShot00945.png "Review and create button")
    
4. On members page click **+ Select members** -> Select powerbi user 

    ![The Review + create button displayed.](update/ScreenShot00946.png "Review and create button")

5. Click on Review & assign until it saves.  

    ![The Review + create button displayed.](update/ScreenShot00948.png "Review and create button")
    
### Task: Create a Power BI account

1. Navigate to [Power BI sign up](https://powerbi.microsoft.com/) to create an account.

    ![The Review + create button displayed.](update/ScreenShot00949.png "Review and create button")
    
2. Use your powerBI user to complete the sign in. 
    
    ![The Review + create button displayed.](update/ScreenShot00950.png "Review and create button")
    
    ![The Review + create button displayed.](update/ScreenShot00951.png "Review and create button")

4. Create a 60 day free powerBI trial.   

    ![The Review + create button displayed.](update/ScreenShot00955.png "Review and create button")

    ![The Review + create button displayed.](update/ScreenShot00956.png "Review and create button")
    
**Note:** Log into Azure portal with PowerBI user and do all the steps (Next lab) with this new user only. 

    ![The Review + create button displayed.](update/ScreenShot00959.png "Review and create button")

You should follow all steps provided *above* performing the next part of the Hands-on lab.
