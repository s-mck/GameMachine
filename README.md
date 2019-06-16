# GameMachine

This is a C# web application that was submitted as my final project for PROG37721 at Sheridan College. 
It was written using Visual Studio 2017 and the 4.6 .NET Framework in April 2019.

The project was intended to test our ability to integrate knowledge of web forms, SQL databases, password hashing, 
authentication/authorization, and state management into a functional web application. The application requires users to 
login or signup, then they can choose to play either Blackjack or Connect 4. I worked with a partner on this project (Robert Struck)
who created the Connect 4 component of the game himself. All other aspects, including the Blackjack component, CSS, data access layer,
and signin/login/home pages were created by me.

****IMPORTANT******

The application requires a working SQL server instance to operate. In order to test the database functionality, test users and data
were added to the tables. I used SQLEXPRESS and named the database 'GameMachine'.

In order to successfully login and play, you must:

1. Create a local SQL database and run the script <gamemachine_db_script.sql>

2. Open the solution in Visual Studio and ensure that your connection string matches the one in Web.config (GameMachineWebApp >
  Web.config > inside the connectionStrings tags > connectionString="your connection string goes here" )
  
3. Replace the connection string if necessary. Save.

4. You will need to use the signup function to create new user(s) as the table will be empty initially.

