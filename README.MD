# MinimalAPI Project

This repository contains a minimal .NET API project with a clean architecture, including domain models, endpoints, services, infrastructure, and comprehensive tests. Below is a detailed overview of the project structure and its main components.


## Technologies Used

- [.NET 9](https://dotnet.microsoft.com/)
- Entity Framework Core
- JWT (JSON Web Token)
- FluentValidation
- AutoMapper
- Swagger/OpenAPI
- SQLite or PostgreSQL (configurable)
- User Secrets (for sensitive data)
- Custom Middlewares

---

## Project Structure

```
MinimalAPI.sln                # Solution file
Api/                          # Main API project
  appsettings.json            # Main configuration file
  appsettings.Development.json# Development configuration
  MinimalAPI.csproj           # Project file
  Program.cs                  # Entry point for the API
  Domain/                     # Domain layer
    ApiModels/                # DTOs for API requests/responses
      CarModel.cs
      UserModel.cs
    Entities/                 # Core business entities
      Car.cs
      PagedResult.cs
      User.cs
    Enums/                    # Enumerations
      UserRoleEnum.cs
    Interfaces/               # Abstractions for repositories and services
      Repository/
      Service/
    Mappers/                  # Mapping logic between domain and models
      DomainToModelMapping.cs
      ModelToDomainMapping.cs
    Endpoints/                # API endpoint definitions
      CarEndpoints.cs
      Login.cs
      UserEndpoint.cs
    Helper/                   # Validation and helper classes
      CarModelValidator.cs
    Infrasctructure/          # Infrastructure layer
      DataBase/               # Database context
        CarsContext.cs
      Repository/             # Repository implementations
        CarsRepository.cs
        EntityRepository.cs
    IoC/                      # Dependency injection setup
      DependencyInjection.cs
    Middlewares/              # Custom middlewares
      ExceptionHandlingMiddleware.cs
    Migrations/               # Entity Framework migrations
      <timestamp>_<migration_name>.cs
      <timestamp>_<migration_name>.Designer.cs
      CarsContextModelSnapshot.cs
    Properties/
      launchSettings.json     # Launch profiles
    Services/                 # Business logic services
      CarsService.cs
      JwtService.cs
      PasswordService.cs
      UserService.cs
Tests/                        # Test project
  MinimalAPITests.csproj      # Test project file
  MSTestSettings.cs           # MSTest configuration
  Services/                   # Service layer tests
    CarsServiceTest.cs
  Validations/                # Validation tests
    CarModelValidatorTests.cs
```

## Main Components

### API Project (`Api/`)
- **Program.cs**: Configures and starts the web API.
- **appsettings.json**: Contains configuration settings for the application.
- **Domain Layer**: Defines models, entities, enums, interfaces, and mapping logic.
- **Endpoints**: Implements HTTP endpoints for cars, users, and authentication.
- **Helper**: Contains validators and utility classes.
- **Infrastructure**: Handles database context and repository implementations.
- **IoC**: Manages dependency injection.
- **Middlewares**: Custom middleware for exception handling.
- **Migrations**: Entity Framework migrations for database schema changes.
- **Services**: Business logic for cars, users, authentication, and password management.

### Test Project (`Tests/`)
- **Services/**: Unit tests for service classes.
- **Validations/**: Unit tests for validators.
- **MSTestSettings.cs**: Configuration for MSTest framework.

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (or update connection string in `appsettings.json`)

## JWT Configuration

Configure the keys in `appsettings.json` **or via User Secrets**:

```json
"JwtSettings": {
  "SecretKey": "your-strong-secret-key",
  "Issuer": "CarApiIssuer",
  "Audience": "CarApiUsers",
  "ExpiresInMinutes": 60
}
```

```user-secrets

dotnet user-secrets init
dotnet user-secrets set "JwtSettings:SecretKey" "your-secret"
```

### Running the API
1. Restore dependencies:
   ```powershell
   dotnet restore
   ```
2. Apply migrations (if needed):
   ```powershell
   dotnet ef database update --project Api
   ```
3. Run the API:
   ```powershell
   dotnet run --project Api
   ```

### Running Tests
```powershell
dotnet test
```

## API Endpoints

- **CarEndpoints**: CRUD operations for cars.
- **UserEndpoint**: User management endpoints.
- **Login**: Authentication endpoints (JWT).

## Contributing

Feel free to fork this repository and submit pull requests. For major changes, please open an issue first to discuss what you would like to change.