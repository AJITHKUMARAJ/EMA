# Sample1 - Employee Management API

A RESTful Web API built with ASP.NET Core 8.0 for managing employee data with full CRUD operations, AutoMapper integration, and comprehensive test coverage.

## 🚀 Features

- ✅ Full CRUD operations for Employee management
- ✅ Entity Framework Core with SQL Server
- ✅ AutoMapper for DTO↔Entity mapping
- ✅ Swagger/OpenAPI documentation
- ✅ 100% code coverage (unit + integration tests)
- ✅ RESTful API design
- ✅ Input validation with Data Annotations

## 📊 Tech Stack

- **Framework**: .NET 8.0
- **Database**: SQL Server (Entity Framework Core)
- **Mapping**: AutoMapper
- **Testing**: xUnit, FluentAssertions, EF Core InMemory
- **Coverage**: Coverlet + ReportGenerator
- **API Documentation**: Swagger/Swashbuckle

## 🏗️ Project Structure

```
Sample1/
├── Controllers/          # API Controllers
│   ├── EmployeeController.cs
│   └── WeatherForecastController.cs
├── Data/                 # Database Context
│   └── ApplicationDbContext.cs
├── Models/               # DTOs and Entities
│   ├── EmployeeDto.cs
│   └── Entities/
│       ├── Employee.cs
│       └── WeatherForecast.cs
├── Profiles/             # AutoMapper Profiles
│   └── MappingProfile.cs
├── Migrations/           # EF Core Migrations
├── tests/                # Test Projects
│   ├── Sample1.Tests.Unit/
│   └── Sample1.Tests.Integration/
├── appsettings.json      # Configuration
└── Program.cs            # Application Entry Point
```

## 🔧 Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server or SQL Server Express
- Visual Studio 2022 / VS Code / Rider

### Setup

1. **Clone the repository**
```bash
git clone https://github.com/yourusername/Sample1.git
cd Sample1
```

2. **Update connection string**
Edit `appsettings.json` and update the `DefaultConnection`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=EMP;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

3. **Run migrations**
```powershell
dotnet ef database update
```

4. **Run the application**
```powershell
dotnet run
```

5. **Access Swagger UI**
Navigate to: `https://localhost:5001/swagger`

## 📚 API Endpoints

### Employee Management

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Employees/GetAllEmployees` | Get all employees |
| GET | `/api/Employees/GetEmployeeById/{id}` | Get employee by ID |
| POST | `/api/Employees/AddEmployee` | Create new employee |
| PUT | `/api/Employees/UpdateEmployee/{id}` | Update employee |
| DELETE | `/api/Employees/DeleteEmployee/{id}` | Delete employee |

### Sample Request Body (POST/PUT)
```json
{
  "name": "John Doe",
  "email": "john.doe@example.com",
  "phone": "123-456-7890",
  "gender": "M",
  "salary": 75000
}
```

## 🧪 Testing

### Run all tests
```powershell
dotnet test
```

### Run with code coverage
```powershell
.\run-coverage.ps1        # Full HTML report
.\coverage-summary.ps1    # Console summary only
```

### Current Test Coverage
- **Line Coverage**: 100% ✅
- **Branch Coverage**: 100% ✅
- **Method Coverage**: 100% ✅
- **Total Tests**: 28 (20 unit + 8 integration)

See [COVERAGE.md](COVERAGE.md) for detailed coverage information.

## 🗄️ Database Schema

### Employee Table
| Column | Type | Constraints |
|--------|------|-------------|
| Id | GUID | Primary Key |
| Name | NVARCHAR(MAX) | Required |
| Email | NVARCHAR(MAX) | Required, EmailAddress |
| Phone | NVARCHAR(MAX) | Required |
| Gender | NVARCHAR(1) | Required, MaxLength(1) |
| Salary | DECIMAL(18,2) | Required |

## 🔐 Configuration

### User Secrets (Recommended)
Store sensitive data in user secrets instead of appsettings.json:

```powershell
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YOUR_CONNECTION_STRING"
```

## 🛠️ Development

### Adding Migrations
```powershell
dotnet ef migrations add MigrationName
dotnet ef database update
```

### Build
```powershell
dotnet build
```

### Clean
```powershell
dotnet clean
```

## 📦 NuGet Packages

### Main Project
- AutoMapper.Extensions.Microsoft.DependencyInjection
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Swashbuckle.AspNetCore

### Test Projects
- xUnit
- FluentAssertions
- Microsoft.AspNetCore.Mvc.Testing
- Microsoft.EntityFrameworkCore.InMemory
- coverlet.msbuild
- coverlet.collector

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License.

## 👤 Author

Your Name - [@yourhandle](https://twitter.com/yourhandle)

## 🙏 Acknowledgments

- ASP.NET Core Team
- Entity Framework Core Team
- AutoMapper Community
