# Project LoremIpsum: Gym Management Software

## Descritpion
LoremIpsum is a gym management software, which let employees manage a gym in a userfriendly modern ui wpf environment. Therefore it can perform various tasks like creating a csv file for all the occured transactions or creating/updating/deleting members, contracts, articles, orders and logins.

## How to Install and Run the Project
If you already have Visual Studio installed (we recommend Visual Studio 2022) you can download the project and open the loremipsum.sln file it with Visual Studio. Or you can directly [clone this repository](https://inf-git.fh-rosenheim.de/ap-wif-ws22/loremipsum.git) into your Visual Studio.
Otherwise you can download Visual Studio [HERE](https://visualstudio.microsoft.com/de/vs/).  
  
After installing the project set the GUI as the Startup Project.  
We used EntityFrameworkCore for the local database. Therefore you have to write in the Package Manager Console: "Update-Database -Project loremipsum". (To see the Package Manager Console, go to View --> Other Windows --> Package Manage Console). After this you are ready to go.

## How to Use the Project
After starting the GUI the user needs to log-in. If no log-in exists, the user will be redirected to the AddLog-In page to create an admin log-in. After the log-in the user will be redirected to the homepage, from which the user sees the currently training members and mutiple buttons which navigate to the different entity pages.
To enter one entity page you have to enter a log-in. All entities pages have different authorization requirements. For example the log-in page can only be entered as an admin.  
  
On each entity page you can see all the current exisiting entities of this type. Also the user can create, delete or update one entity. There is also an option to delete all entites of this type, but this option is only executable by a user with admin rights.  
On the Employee Page you can checkout all the members. This will create or update the MemberBills.csv with all the transactions which happened till now. 

## Important notes:
- Relations:
    - a member can only exist with a contract
    - an order can only be created with an article and a member
  
- the program calculates in cent prices and displays them in cent prices. But the comma notation is accepted in the wpf 
- Contract checkout:
    - the amount the member has to pay for the contract each month depends on since when the contract operates
    - the right amount will be calculated percentual
    - for example a member joined the gym on the 15. last month. Therefore he only has to pay for the remaining days of the month
    - This is a memberfriendly implementation
- checkouts can only be performed if no other process uses the MemberBills.csv file
- when deleting a member. The contract and the orders of the member will be checkouted
- deleting one order means returning the articles and the currentbill of the member will be decreased by the order price
- deleting all orders means no articles are being returned and the currentbill of the member will be set to 0

## Teammembers

Arlind Tahiri  
Dominik Schiffer  
Lorenz Huber
