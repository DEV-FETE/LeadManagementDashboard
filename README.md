# Lead Management Dashboard (.NET 10 & EF Core + MSSQL)

A clean, responsive Kanban-style Lead Management Dashboard built using **.NET 10 Razor Pages**, **Entity Framework Core**, and **MSSQL Server**.

## Key Architecture & Features
- **Clean Architecture & Separation of Concerns**: Exposes DTOs/ViewModels to UI, strictly encapsulating EF entities.
- **Dynamic Kanban Board**: Columns are dynamically grouped and sorted via `DisplayOrder`.
- **Domain Transition Validation**: Backed by service-level checks ensuring state movement constraints.
- **Audit Logging**: Logs all moves with timestamps and state changes (`FromStatusId`, `ToStatusId`).
- **Bootstrap 5 UI**: Fully responsive board with Toast Notifications and an Activity History modal.

## Quickstart

1. **Clone Repository**:
   ```bash
   git clone [https://github.com/DEV-FETE/LeadManagementDashboard.git](https://github.com/DEV-FETE/LeadManagementDashboard.git)
   cd lead-management-dashboard

__________________________________________________________________________________
2. **Database**

**Configure Database Connection:**
Update ConnectionStrings:DefaultConnection in appsettings.json to point to your MSSQL Server instance.

**Database Migration & Startup:**
Run the following in the terminal or Package Manager Console:

dotnet ef migrations add InitialCreate
dotnet run

**Note**: On application execution, DbInitializer.Seed automatically checks and seeds default statuses, sample leads, and activity history.

__________________________________________________________________________________

Thank You!

