Создание миграций:

dotnet ef migrations add "InitialMigration" --project InfTech.Infrastracture --startup-project InfTech --output-dir Database/Migrations

Применение миграций:

dotnet ef database update --project InfTech.Infrastracture --startup-project InfTech
