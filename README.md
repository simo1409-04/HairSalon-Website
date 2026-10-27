# Graphite Barber Studio

Учебен ASP.NET Core MVC проект за онлайн резервации във фризьорски салон. Посетителят избира фризьор, свободна дата и час, добавя услуги и получава потвърждение с изчислената цена.

![Интериор на Graphite Barber Studio](HairSalon-Website/HairSalon-Website/wwwroot/images/home/graphite-barber-hero.png)

## Какво работи

- Начална страница и каталог с услуги и локално съхранявани изображения.
- Календар с актуално заети часове за избрания фризьор. Работните дни са сряда–неделя; началните часове са от 10:00 до 19:00, с почивка между 13:15 и 14:00.
- Резервация с една или повече услуги. Общата цена се пресмята на сървъра от цените в базата.
- Проверки на входните полета и проверки в сървиса за дата, час, фризьор и услуги. Уникален индекс в SQL Server не допуска два записа за един фризьор в един и същ час.
- Страница за успешно записана резервация, която се връща след POST заявката.
- Съобщение на началната страница, когато записът в базата е неуспешен.

## Технологии и структура

.NET 10, ASP.NET Core MVC, Razor, Entity Framework Core 10, SQL Server, Bootstrap и JavaScript.

| Проект | Отговорност |
| --- | --- |
| `HairSalon-Website` | Контролери, Razor изгледи и статични файлове |
| `HairSalon.Services.Core` | Правила за резервации и услуги, интерфейси |
| `HairSalon.Data` | `DbContext`, EF конфигурации, миграции и repository |
| `HairSalon.Data.Models` | Модели за базата |
| `HairSalon.DataTransferObjects` | Входни и изходни модели за приложението |
| `HairSalon.Common` | Споделени константи за валидация |

## Стартиране локално

Необходими са .NET 10 SDK и работещ SQL Server Express с Windows Authentication. От корена на репозиторията:

```powershell
cd HairSalon-Website
dotnet restore HairSalon-Website.slnx
dotnet build HairSalon-Website.slnx
```

Приложете миграциите. Във Visual Studio Package Manager Console:

```powershell
Update-Database -Project HairSalon.Data -StartupProject HairSalon_Website
```

Или с .NET CLI, след като инсталирате `dotnet-ef` версия 10.0.14:

```powershell
dotnet tool install --global dotnet-ef --version 10.0.14
dotnet ef database update --project HairSalon.Data/HairSalon.Data.csproj --startup-project HairSalon-Website/HairSalon_Website.csproj
```

След това стартирайте приложението:

```powershell
dotnet run --project HairSalon-Website/HairSalon_Website.csproj --launch-profile http
```

Адресът на профила `http` е `http://localhost:5002`. Миграциите създават таблиците и началните записи за фризьори, услуги и примерни резервации.

## Връзка с базата

В `HairSalon-Website/appsettings.json` има локална връзка към `.\SQLEXPRESS` и база `HairSalonDb` чрез Windows Authentication. Тя не съдържа потребителско име или парола и може да остане в Git. Ако използвате друг SQL Server instance, задайте `ConnectionStrings__DefaultConnectionString` като environment variable или променете локалната конфигурация. Не записвайте реални пароли в репозиторията.

Пример за текущата PowerShell сесия:

```powershell
$env:ConnectionStrings__DefaultConnectionString = 'Server=.\SQLEXPRESS;Database=HairSalonDb;Trusted_Connection=True;Encrypt=False'
```

Други environment variables или външни API ключове не са необходими за локално стартиране.

## Основни адреси

| Адрес | Страница |
| --- | --- |
| `/` | Начало |
| `/Service` | Услуги |
| `/Reservation/Create` | Форма за резервация |

Потвърждението `Reservation/Result` се показва след успешно изпратената форма; няма самостоятелен GET адрес.

## Статус

Това е прототип за учебния проект. Публичният поток за резервации работи; все още няма вход с акаунти, администраторски екран, редактиране и изтриване на резервации. 
