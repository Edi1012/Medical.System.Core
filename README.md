# Medical.System.Core

Medical.System.Core is a class library packaged as a [NuGet package](https://www.nuget.org/packages/Medical.System.Core/). It's designed to manage medical systems, providing a robust architecture incorporating best practices and design patterns such as Repository, Unit of Work, and Services.

## NuGet Package
The library is available as a NuGet package and can be found at [Medical.System.Core on NuGet](https://www.nuget.org/packages/Medical.System.Core/).

## Directory Structure

### Models
This folder contains two subfolders:
- **Dtos**: Data Transfer Objects used for shaping the data between client and server.
- **Entities**: Domain models representing the structure of the objects within the system.

### Repositories
Defines the contract and implementation of data access:
- **Interfaces**: Defines the contracts that repositories must adhere to.
- **Implementations**: Concrete classes that implement the repository interfaces, handling the actual data access.

### Services
Manages business logic:
- **Interfaces**: Service interfaces that define the methods.
- **Implementations**: Actual service classes that implement the business logic.

### UnitOfWork
Contains the implementation of the Unit of Work pattern, providing a way to coordinate transactions across the repositories.

### Validators
Includes classes that handle validation logic, possibly using FluentValidation, to ensure that objects meet the necessary criteria before processing.

### Migrations
(Optional) Stores database migration files if needed.

### Extensions
This directory contains extension methods, potentially for dependency injection configuration, that keep the main configuration files clean and organized.

## Conclusion
The Medical.System.Core structure is designed to encourage clean architecture and separation of concerns, enhancing maintainability, readability, and scalability. The alignment with microservices principles allows for flexible and scalable architecture, making it suitable for complex medical system management.

As a class library available as a NuGet package, it offers easy integration and usage across different .NET projects, including RESTful APIs and microservices that may utilize this core functionality.

For further details, please refer to the inline documentation within each directory and file, or visit the [NuGet package page](https://www.nuget.org/packages/Medical.System.Core/).
