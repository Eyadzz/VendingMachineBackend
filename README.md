# Vending Machine

## Project Overview
This ASP.NET Web API project serves as a vending machine service, providing functionalities for user authentication, registration, and various vending machine-related operations. The project utilizes Postgres as the primary database for data storage and Redis for caching to improve performance.

The project leverages several essential packages and libraries, including MediatR, BCrypt, JWT Bearer Authentication, Entity Framework Core (EF Core), and Serilog. Each of these components plays a critical role in the functionality and security of the application.

## Tech Stack

1. **.NET 8**
2. **Postgres**
3. **Redis**


## Architecture

**Clean Architecture** is used as the architectural pattern for this project, promoting the separation of concerns and maintainability by organizing code into distinct layers: Presentation, Application, Domain, and Infrastructure.

## Packages and Libraries

1. **MediatR**:
   - MediatR is a simple, unambitious mediator implementation in .NET that helps manage and route commands and queries to handlers. In this project, it is used to separate and manage command and query logic.

2. **BCrypt**:
   - BCrypt is a password hashing library that provides secure password hashing and verification. It ensures that user passwords are stored securely in the database.

3. **JWT Bearer Authentication**:
   - JSON Web Token (JWT) Bearer Authentication is used to secure API endpoints. It verifies the identity of users by validating JWT tokens, allowing access to protected resources.

4. **Entity Framework Core (EF Core)**:
   - EF Core is a lightweight and extensible Object-Relational Mapping (ORM) framework that is used to interact with the Postgres database. It simplifies data access and database operations.

5. **Serilog**:
   - Serilog is a structured logging library for .NET. It is used for logging events, errors, and debugging information in the application. Serilog can be configured to store logs in various sinks like text files, databases, or cloud services.

6. **StackExchange Redis**:
   - StackExchange Redis is a high-performance .NET client for Redis. It is used for interacting with the Redis cache, which is used to improve the performance of the application.

7. **Mapster**:
   - Mapster is a fast and easy-to-use object-to-object mapper. It is used to map objects between layers, such as from the domain objects to DTOs.

8. **RateLimiting**:
   - RateLimiting is a middleware that is used to limit the number of requests that can be made to the API. It is used to prevent brute force attacks.

## Endpoints

![image](https://github.com/Eyadzz/VendingMachineBackend/assets/66397595/52853b33-e15f-4612-89d0-14fbdce8be83)


## Running the Application

- You can use the provided docker-compose file to run the whole stack on docker in case you don't have the necessary tools to run on your machine using "docker compose up -d --build"
- In case you do not want to use Docker, You need VS, .NET 8 SDK, Postgres and Redis installed on your machine.
- You can then start using the backend from Swagger page "http://localhost:5099/swagger/index.html"
- There are pre-instered rows in the database to help with testing which includes (2 Users with different roles, 2 Roles, 3 Products) and the default login for the users are (username: seller or buyer, password:$String1)
