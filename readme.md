<img src="icon.png" align="right" />

# Synword

An application for checking and enhancing the uniqueness of the text.

**[Website](https://synword.com)**

## Connection strings
1. To initialize the connection to SQLite, you need to enable user secrets.

    ```
    dotnet user-secrets init
    ```

1. Defining the connection strings

    ```
    dotnet user-secrets set "UserDataConnection" "data source=UserDataDB.sqlite"
    ```
    
    ```
    dotnet user-secrets set "IdentityConnection" "data source=IdentityDB.sqlite"
    ```

    ```
    dotnet user-secrets set "RusSynonymDictionaryDbConnection" "data source=RusSynDictionary.sqlite"
    ```

    ```
    dotnet user-secrets set "EngSynonymDictionaryDbConnection" "data source=EngSynDictionary.sqlite"
    ```
## Docker
1. Execute in root directory.

    ```
    docker-compose up --build -d 
    ```
2. Open http://localhost:3000.

## Techical Stack

- C# 10
- ASP.NET 6.0
 - ASP.NET WebApi Core with JWT Bearer 
 Authentication
- Minimal APIs
- Ardalis API Endpoints
- ASP.NET Identity Core
- .NET Core Native DI
- Entity Framework Core 6.0
- SQLite
- AutoMapper
- MediatR
- Swagger UI

UI
- React + Redux

## Practices

- Domain Driven Design
- CQRS
- Repository & Generic Repository
- Unit Of Work
- Inversion of Control / Dependency injection
- ORM
- Mediator
- Specification Pattern

## Layers

**PublicApi**

**Application:** Flow control.

**Domain:** Entities and domain logic.

**Infrastructure** Data sources and third party services.

## Domain database model

<p align="center">
    <img src="docs/db_model.png" alt="drawing" width="80%"/>
</p>

## Mobile app screens

<p align="center">
    <img src="docs/Images/Main screen.png" alt="drawing" width="30%"/>
</p>

<p align="center">
    <img src="docs/Images/Plagiarism check layer.png" alt="drawing" width="30%"/>
</p>

<p align="center">
    <img src="docs/Images/Rephrase layer.png" alt="drawing" width="30%"/>
</p>