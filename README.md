This application can work with two types of databases: MongoDB and SQL Server. From appSettings file, select the specific database in **UseNoSQL** item.

**For MongoDB:**
All collections will be created by application when repository methods are executed for the first time.
Set the MongoDbSettings item from  appSettings file the MongoDb Connection String and the Database name.

**For SQL Server:**
Apply Update-Dabase commando from Entity Framework Migration to create the new database.
Set the ConnectionStrings item from appSettings file the SQL Server Connection String.
