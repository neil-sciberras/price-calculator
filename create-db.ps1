dotnet ef migrations add InitialCreate --project ./backend/infrastructure/backend.infrastructure.database --startup-project ./spa

dotnet ef database update --project ./backend/infrastructure/backend.infrastructure.database --startup-project ./spa