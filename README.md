# Class Registration System

A robust console-based class registration system built with C#, Entity Framework Core, and Dependency Injection.
Took around 12 hours to make without AI

## Features

- **Class Management**:
  - Create classes with configurable capacity
  - Track class occupancy and availability
- **Student Management**:
  - Register new students
  - Enroll/drop students from classes
- **Reporting**:
  - View student schedules
  - List available classes
- **Data Persistence**:
  - SQL Server database integration
  - Entity Framework Core migrations

## Technical Architecture

### Core Components
| Layer              | Key Components                     |
|--------------------|-----------------------------------|
| **Domain**         | `Class`, `Student` models         |
| **Application**    | `RegistrationService` (business logic) |
| **Infrastructure** | EF Core, Repository pattern       |
| **Presentation**   | Console menu system               |

### Key Technologies
- .NET Core
- Entity Framework Core
- Dependency Injection
- Repository Pattern
- SQL Server

## Getting Started

### Prerequisites
- .NET 6+ SDK
- SQL Server
- Configure connection string in `appsettings.json`

### Installation
1. Clone the repository
2. Run migrations:
   ```bash
   dotnet ef database update
   ```
3. Run the application:
	```bash
	dotnet run
	```

### Usage Examples

	
	1. Create a Class
	2. Register student to a class
	3. Remove student from a class
	4. See which class a student is enrolled to
	5. Display available classes
	6. Register a student
	7. Exit

	Choose option: 1
	Enter class name: Advanced C#
	Enter class type: Programming
	Enter max occupancy: 25
	Class successfully added!
	


### Code Quality Highlights
- Clean architecture separation
- Proper use of DI and interfaces
- Entity Framework Core best practices
- Comprehensive input validation
- Asynchronous operations throughout
- Meaningful return tuples (Success, Message)

