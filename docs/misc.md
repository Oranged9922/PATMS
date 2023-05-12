## Initial Build sqlite database
- navigate to folder in which the solution is
- type ``dotnet ef migrations add InitialCreate -p .\Infrastructure -s .\API``
This generates migration file to build the database from using the ``PatmsDBContext.cs`` file.

## Apply migration
- navigate to `.\Infrastructure` folder
- type ``dotnet ef database update``
This creates sqlite database file in your ``Users\$username\AppData\Local\`` folder as ``PatmsDb.db``
