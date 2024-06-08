This is an online bookstore application built with .NET Core, Dapper, and PostgreSQL. It allows users to browse books, add them to their shopping cart, make purchases, and view their purchase history.

Step 1: Setting Up the Project

Clone the Repository: Clone the Online Bookstore project repository from GitHub or your version control system.
Open in Visual Studio: Open the solution file (`OnlineBookstore.sln`) in Visual Studio.

Step 2: Installing Dependencies

The project relies on several dependencies, including ASP.NET Core, Dapper, and Npgsql. Ensure that these dependencies are installed in your development environment. If not, you can install them using NuGet Package Manager.

Step 3: Configuration

Database Connection: Ensure that the database connection string is correctly configured. You can find it in the `appsettings.json` file of the `OnlineBookstore` project. Adjust the connection string according to your database setup.

Step 4:Building the Solution

Build Solution: In Visual Studio, go to the "Build" menu and select "Build Solution". Ensure that the solution builds successfully without any errors.

Step 5: Running the Application

Set Startup Project: Right-click on the `OnlineBookstore` project in the Solution Explorer and select "Set as Startup Project".
Run the Application: Press F5 or click on the "Start" button in Visual Studio to run the application.
Verify: Once the application is running, open a web browser and navigate to `https://localhost:<port>/api/books` to verify that the API endpoints are accessible.

Ensure that unit tests are passing by running the unit test project. You can do this by right-clicking on the unit test project in Solution Explorer and selecting "Run Tests".

Project Structure: The project is structured into separate class libraries (`OnlineBookstore`, `OnlineBookstore.Domain`, `OnlineBookstore.Core`, `OnlineBookstore.Application`, `OnlineBookstore.Infrastructure`) to maintain modularity and separation of concerns.
Domain Models: Defined domain models (`Book`, `ShoppingCart`, `PurchaseHistory`) in the `OnlineBookstore.Domain` library to represent entities within the application.
Repositories: Implemented repository interfaces (`IBookRepository`, `IShoppingCartRepository`, `IPurchaseHistoryRepository`) in the `OnlineBookstore.Core` library to define data access operations.
Application Layer: Implemented repository implementations (`BookRepository`, `ShoppingCartRepository`, `PurchaseHistoryRepository`) using Dapper in the `OnlineBookstore.Application` library to interact with the database.
Dependency Injection: Set up dependency injection in the `OnlineBookstore.Infrastructure` library to register repository services.
Controllers: Created controllers, such as BooksController`, `ShoppingCartController`, `PurchaseHistoryController`, `CheckoutController`) in the `OnlineBookstore` project to define API endpoints for CRUD operations and checkout functionality.
Unit Testing: Provided an example of unit testing for the `BookRepository` using the Moq framework to ensure the correctness of data access operations.

