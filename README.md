# GoVisit Appointment Service

A robust, modular .NET Web API for appointment scheduling.  
This project leverages **CQRS** and clean architecture principles, with **MongoDB** as the primary data store.

---

## Features

- Centralized appointment management
- Separation of concerns using CQRS (Commands for writes, Queries for reads)
- Clean, maintainable codebase following SOLID principles
- Interactive API documentation with Swagger / OpenAPI
- Full dependency injection for all services

---

## Technologies

- **.NET 8.0+** (Web API)
- **MongoDB** (NoSQL persistence)
- **CQRS** (Command Query Responsibility Segregation)
- **Swagger / OpenAPI** (API documentation)
- **Dependency Injection** (built-in .NET DI)

---

## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [MongoDB Community Server](https://www.mongodb.com/try/download/community) (default port: 27017)

### Setup

1. Clone the repository: 
   git clone https://github.com/your-org/govisit-appointment-service.git cd govisit-appointment-service
2. Ensure MongoDB is running locally on port 27017.
3. Restore dependencies:
   dotnet restore

---

## Running the Service

Start the API with:

The API will be available at:  
`https://localhost:7181`

### API Documentation

Access the interactive Swagger UI at:  
[https://localhost:7181/swagger/index.html](https://localhost:7181/swagger/index.html)

