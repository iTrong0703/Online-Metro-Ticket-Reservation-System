# Metro Ticket Reservation System

🚇 A learning project for building a modern metro ticket reservation system using ASP.NET Core and Clean Architecture.

> ⚠️ This is a **learning project**, developed during my journey of studying **ASP.NET Core**, **Clean Architecture**, **CQRS**, and related technologies. Therefore, some features may be incomplete or still under development.

## 📁 Project Structure

This project follows the Clean Architecture approach and is structured into the following layers:

-   **MetroTicketReservation.API** – Presentation layer: Contains controllers and API endpoints.
-   **MetroTicketReservation.Application** – Application layer: Contains use cases, CQRS (commands & queries), interfaces, and validations.
-   **MetroTicketReservation.Domain** – Domain layer: Core business entities and domain logic.
-   **MetroTicketReservation.Infrastructure** – Infrastructure layer: Data access (EF Core), third-party service integration (e.g., MoMo), external interface implementations.
-   **ControllerIntegrationTests** – Integration tests for API controllers using xUnit.

## 🛠️ Technologies & Tools Used

-   **.NET 8**
-   **ASP.NET Core Web API**
-   **Entity Framework Core**
-   **PostgreSQL** as the database
-   **MediatR** for CQRS (Command and Query Responsibility Segregation)
-   **FluentValidation** for request validation
-   **Google OAuth 2.0** for user authentication
-   **MoMo Payment Gateway** integration
-   **xUnit** for integration testing
-   **Seeding** for initial data population

## 📌 Current Status

This project is still a **work in progress** and mainly serves as a **hands-on practice** for mastering Clean Architecture and related patterns. Some features like ticket usage history, full payment flow, and advanced user management are **not yet implemented**.
