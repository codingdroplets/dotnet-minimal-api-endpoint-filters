# dotnet-minimal-api-endpoint-filters

![.NET](https://img.shields.io/badge/.NET-10.0-blueviolet)
![License](https://img.shields.io/badge/license-MIT-green)
![Last Commit](https://img.shields.io/github/last-commit/codingdroplets/dotnet-minimal-api-endpoint-filters)

A beginner-friendly ASP.NET Core Minimal API sample showing endpoint filters for reusable request validation, with Swagger/OpenAPI.

## Key Features
- Minimal API product endpoints
- Reusable `IEndpointFilter` validation
- Swagger/OpenAPI for testing
- Beginner-friendly clean structure

## Architecture Overview
- `Program.cs`: endpoint registration and filter wiring
- `ProductValidationFilter`: reusable validation before handler execution

## Tech Stack
- .NET 10
- ASP.NET Core Minimal APIs
- Swagger/OpenAPI

## Getting Started
```bash
dotnet restore
dotnet build
dotnet run --project DotnetMinimalApiEndpointFilters.Api
```

## Author / Maintainer
- Visit Now: https://codingdroplets.com
- Join our Patreon to Learn & Level Up: https://www.patreon.com/codingdroplets
