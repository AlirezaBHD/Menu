## 🍽️ Restaurant Menu Management System (Multi-Restaurant)

In this project, I've designed and implemented a scalable and secure multi-restaurant menu management system, built with high precision and flexibility for real-world commercial use.

<img width="1520" height="1298" alt="2025-07-10 17 05 37 localhost 532e71cf9141" src="https://github.com/user-attachments/assets/003037b7-b982-4b5f-b8b0-5e385e402d96" />

### 🛠️ Technologies & Tools

- **ASP.NET Core Web API** with **Clean Architecture**
- **Entity Framework Core** + **PostgreSQL**
- **JWT Authentication** & **Role-Based Authorization**
- **FluentValidation** + **Swagger Integration**
- **Custom Access Control** to restrict users to their own restaurant's data
- **Clean Routing** with Kebab-case URL Convention
- **Security Headers** middleware for XSS, clickjacking, MIME sniffing protection

### 🚀 Key Features

- Fully modular structure focused on separation of concerns
- Complete Swagger UI with Persian descriptions, request/response samples, and validation metadata
- Fine-grained access control using ownership-based filtering (users can access only their own data)
- Full implementation of authentication and authorization with support for multiple roles (Admin, Owner, etc.)
- Dynamic validation extensions based on entity Data Annotations
- File management system (e.g., food images) built as a separate service following SOLID principles
- Structured and filterable **JSON logging** (Serilog) with per-user context, log levels, and rotation
- **Rate limiting** with configurable request caps per IP

### Prerequisites
- .NET 9 SDK
- PostgreSQL
