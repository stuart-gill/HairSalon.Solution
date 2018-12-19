# Hair Salon

A C# program that allows the user to view, edit, enter (save), and delete both stylists and clients from a SQL database with a table for each. Stylists are related to clients in a one to many relationship. A stylist "foreign key" is stored for each client in the client table, allowing clients to be viewed and deleted in batches as they relate to their stylist. 

## Plain English specifications for program functionality, starting from an empty database:

1. Program asks user if they would like to add a stylist
2. Input page allows for stylist name
* EXAMPLE INPUT: "Jenny"
3. Program reads the input and sends them to the stylist table of the SQL database
4. User may then add another stylist or view the details on the one stylist (ie. Jenny)
5. If adding another stylist, return to step 2. If viewing details on one stylist, proceed to step 6. 
* EXAMPLE INPUT: [user clicks on "Jenny"]
6. Program brings up Jenny's details from database and displays them, including a list of Jenny's clients. At this point, list is empty and program says: 
* EXAMPLE OUTPUT: "Jenny does not currently have any clients. Click below to add"
* EXAMPLE INPUT: [user clicks on "add client"]
7. If the user clicks on "Add clients", a form appears to add a client and their phone number to Jenny's client list. 
* EXAMPLE INPUT: "Roxy Client", "2065555555"
8. Program then reads this information and stores in it the client table of the databse, inluding Jenny's stylist Id in the entry, so that "Roxy Client" is associated with Jenny. 
9. Program then returns user to Jenny's detail page, including a list of her clients, each of which can now be clicked on to get more detail about the individual client. 
10. On the stylist detail page, the user may choose to delete this stylist AND all their clients, which runs a delete function on both stylist and client tables to make sure both stylist and ALL clients sharing this stylist's id are deleted.
11. From the individual client view pages, a single client may be edited or deleted. 
12. Specialties may also be created. Unlike stylists, specialties may not be edited or deleted. 
13. Once created on the new specialty page, a specialty may be associated with a stylist. 
14. Similarly, once created on the new stylist page, a stylist may be associated with a specialty. 
15. Any stylist may be remo

### Setup

Download .NET Core 2.1.3 SDK and .NET Core Runtime 2.0.9 and install them. Download Mono and install it. Download and install MAMP (requires MAC OSX)

Clone this repository: $ git clone https://github.com/stuart-gill/HairSalon.Solution.git
Change into the work directory:: $ cd WordCounter.Solution
To edit the project, open the project in your preferred text editor.

Find stuart_gill.sql and stuart_gill_test.sql files in the top level of the project directory. These are the databases that the program will access. 

Setup and Run MAMP. Click 'Start Servers' when the MAMP window pops up. On the Starting page, click on the 'Tools' tab and open 'PHPMYADMIN'.
Click on the 'Import' tab and follow instructions to import stuart_gill.sql and stuart_gill_test.sql files into the current server.

ALTERNATELY, if you would like to set up your own database rather than using the included databse, use the following command in your terminal to enter MySQL:
'/Applications/MAMP/Library/bin/mysql --host=localhost -uroot -proot'

You should then see a prompt like this:

mysql>

Once you do, enter the following commands:

> CREATE DATABASE stuart_gill;
> USE stuart_gill;
> CREATE TABLE clients (client_id serial PRIMARY KEY, client_name VARCHAR(255), client_phone VARCHAR(255), stylist_id(INT));
> CREATE TABLE stylists (stylist_id serial PRIMARY KEY, stylist_name VARCHAR(255), specialty VARCHAR(255));

> CREATE DATABASE stuart_gill_test;
> USE stuart_gill_test;
> CREATE TABLE clients (client_id serial PRIMARY KEY, client_name VARCHAR(255), client_phone VARCHAR(255), stylist_id(INT));
> CREATE TABLE stylists (stylist_id serial PRIMARY KEY, stylist_name VARCHAR(255), specialty VARCHAR(255));


To run the program, first navigate to WordCounter.Solution/WordCounter and then type dotnet restore in terminal, then dotnet build, then dotnet run. Then navigate in your browser to the URL listed in your terminal. 

To run the tests, use these commands: $ cd WordCounter.Tests $ dotnet test 

## Running the tests

To run tests, navigate to HairSalon.Test in your editor and run the command "dotnet test"

### List of tests

The tests test the ability of the program to:
1. Create a client object of the class Client
2. Get the name of the client from the database
3. Set the name of the client
4. Get an empty list of all clients when no clients have yet been added
5. Get a list of all clients when two clients have been added
6. Find a particular client when searching by that client's ID (primary key)
7. Save a client's information to the database
8. Automatically assign a new ID to each new client object when it is created
9. Edit a client's information in the database
10. Delete a client's information from the databse
11. Get the stylist ID from the client's database entry
12. Create a stylist object of the class Stylist
13. Get the name of the stylist from the database
14. Get the Id of the stylist from the database
15. Get a list of all stylist object from the database
16. Find a particular stylist in the database by searching stylist ID
17. Get an empty list of all stylists when no stylists have yet been added
18. Save a new stylist object to the database
19. Automatically assign a new ID to each new stylist object when it is created




## Built With

* C#
* .NET Core
* MSTest
* MySQL
* MAMP



## Author

* **Stuart Gill** 

Contact me at stuart.a.gill@gmail.com

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details