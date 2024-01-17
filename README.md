<p>
 <img src="https://img.shields.io/badge/.Net%20Core-0078D4?style=for-the-badge&logo=dotnet&logoColor=white" />
 <img src="https://img.shields.io/badge/EF%20Core-0078D4?style=for-the-badge&logo=dotnet&logoColor=white" />
 <img src="https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white" />
 <img src="https://img.shields.io/badge/PostgreSQL-4169E1?style=for-the-badge&logo=postgresql&logoColor=white" />
 <img src="https://img.shields.io/badge/Stripe-008CDD?style=for-the-badge&logo=stripe&logoColor=white" />
</p>

## E-Commerce
This is a backend system that is specifically designed for a company's e-commerce operations. It is created by using .Net Core Web API project, applying Domain-Driven Design, Clean Architecture and Vertical Slice Architecture. Furthermore, Domain Events were implemented and CQRS was utilized for a decoupled and maintable system. The user authentication and authorization were also accomplished through JWT, and unit tests were created to ensure the project's functionality.

## Getting Started
### Dependencies
- [.Net Core](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) Version 6.0
- [Entity Framework Core](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore) Version 6.0.25
```
dotnet add package Microsoft.EntityFrameworkCore --version 6.0.25
```
- [PostgreSQL](https://www.postgresql.org/download/) Version 16.1
- [Npgsql EF Core PostgreSQL](https://www.nuget.org/packages/Npgsql.EntityFrameworkCore.PostgreSQL) Version 6.0.22
```
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 6.0.25
```
- [Asp.Net Core JWT Bearer](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer) Version 6.0.25
```
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 6.0.25
```
- [BCrypt.Net-Next](https://www.nuget.org/packages/BCrypt.Net-Next) Version 4.0.3
```
dotnet add package BCrypt.Net-Next --version 4.0.3
```
- [AutoMapper](https://www.nuget.org/packages/AutoMapper) Version 12.0.1
  - [AutoMapper DI](https://www.nuget.org/packages/AutoMapper.Extensions.Microsoft.DependencyInjection) Version 12.0.1
```
dotnet add package AutoMapper --version 12.0.1
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 12.0.1
```
- [MediatR](https://www.nuget.org/packages/MediatR) Version 12.2.0
```
dotnet add package MediatR --version 12.2.0
```
- [FluentValidation](https://www.nuget.org/packages/FluentValidation) Version 11.9.0
  - [FluentValidation DI](https://www.nuget.org/packages/FluentValidation.DependencyInjectionExtensions) Version 11.9.0
```
dotnet add package FluentValidation --version 11.9.0
dotnet add package FluentValidation.DependencyInjectionExtensions --version 11.9.0
```
- [Stripe.Net](https://www.nuget.org/packages/Stripe.net) Version 43.10.0
```
dotnet add package Stripe.net --version 43.10.0
```
#### (For unit tests)
- [Moq](https://www.nuget.org/packages/Moq) Version 4.20.70
```
dotnet add package Moq --version 4.20.70
```
#### (For code-first development)
- [EF Core Tools](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools) Version 6.0.25
```
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 6.0.25
```
- [EF Core Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design) Version 6.0.25
```
dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.25
```

## License
This project is licensed under the GPL-3.0 License - see the LICENSE.md file for details
