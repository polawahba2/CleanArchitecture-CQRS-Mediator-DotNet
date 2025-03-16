# CleanArchitecture-CQRS-Mediator-DotNet

**A production-ready .NET template implementing Clean Architecture with CQRS and MediatR pattern**

[![.NET 9](https://img.shields.io/badge/.NET-8-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-8.0-%23512BD4?logo=asp.net)](https://learn.microsoft.com/aspnet/core)
[![CQRS](https://img.shields.io/badge/Pattern-CQRS-2CA5E0)](https://learn.microsoft.com/azure/architecture/patterns/cqrs)



![Clean-Architecture-1](https://github.com/user-attachments/assets/9a65bee8-51b7-468f-b343-90a02b3b8a82)

## Architecture Overview
This template follows Clean Architecture principles with clear layer separation:
- **Domain** (Core Business Logic)
- **Application** (Use Cases & CQRS)
- **Infrastructure** (Persistence & External Services)
- **Presentation** (Web API Endpoints)

## Features
✅ **Clean Architecture** implementation  
✅ **CQRS Pattern** with MediatR  
✅ **JWT Authentication** & Authorization  
✅ **Entity Framework Core 8** with SQL Server  
✅ Validation Pipeline with **FluentValidation**  



## Tech Stack
- .NET 9
- ASP.NET Core Web API
- Entity Framework Core 9
- MediatR 
- AutoMapper 
- SQL Server

## Getting Started
### Prerequisites
- .NET 8 SDK

### Installation 🚀
```bash
# Clone repository
git clone git@github.com:polawahba2/CleanArchitecture-CQRS-Mediator-DotNet.git
cd CleanArchitecture-CQRS-Mediator-DotNet

# Run migrations
dotnet ef database update -p src/Infrastructure -s src/Presentation

# Start application
cd src/Presentation
dotnet run

