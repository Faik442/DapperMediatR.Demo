DapperMediatR.Demo

This project is a simple demonstration of how to integrate Dapper with MediatR in a .NET Web API.
It was built as a learning experiment to explore lightweight data access patterns and the mediator pattern in action.

✨ Features

Dapper ORM for lightweight data access.

MediatR for implementing the CQRS pattern and decoupling business logic from controllers.

.NET Web API structure for a simple, extendable service.

Clean and minimal project setup focusing on business logic flow rather than full enterprise complexity.

📂 Project Structure
.
├─ DapperMediatR.Demo.sln
├─ DapperMediatR.Demo/
│  ├─ Controllers/
│  ├─ Commands/
│  ├─ Queries/
│  ├─ Data/
│  ├─ Domain/
│  └─ Program.cs

🚀 Getting Started
Prerequisites

.NET 8 SDK

SQL Server (or any database supported by Dapper; connection string can be updated in appsettings.json)

Run Locally
# Clone the repository
git clone https://github.com/Faik442/DapperMediatR.Demo.git

cd DapperMediatR.Demo

# Restore dependencies
dotnet restore

# Run the project
dotnet run


API will be available at: http://localhost:5000

🎯 Purpose

This repository is not intended for production use. It was created as a demo project to:

Practice using Dapper for fast, SQL-based data access.

Explore MediatR’s pipeline behavior and request/response handling.

Showcase a minimal CQRS-style setup in .NET.

📜 License

MIT License – feel free to use this project as reference or learning material.
