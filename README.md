Important points to remember while running the application.

1. Execute the database scripts given in 'Assessment.sql' file.
2. Replace the database server name in the connection string in 'appsettings.json' and 'nlog.config' file.

This solution contains all the given assesments completed in Asp.Net Core MVC application.

Below are the list of API calls,

1. BookList By PAT in EF :- This API call is used to get all the books list using EF query by Publisher, Author(last,first), Title sorting.
2. BookList By AT in EF :- This API call is used to get all the books list using EF query by Author(last,first), Title sorting.
3. BookList By PAT in SP :- This API call is used to get all the books list using Stored Procedure by Publisher, Author(last,first), Title sorting.
4. BookList By AT in SP :- This API call is used to get all the books list using Stored Procedure by Author(last,first), Title sorting.
5. Total Price :- This API call is used to get Total Price of the Books using EF query.
6. Insert BookList :- This API call is used to bulk insert Book List into database using EF query.
