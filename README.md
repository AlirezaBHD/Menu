![Capture](https://github.com/user-attachments/assets/234a267c-1efc-496a-9c0d-f042d704e4a7)

## 🍽️ Restaurant Menu Management System (Multi-Restaurant)

In this project, I've designed and implemented a scalable and secure multi-restaurant menu management system, built with high precision and flexibility for real-world commercial use.

### 🛠️ Technologies & Tools

- **ASP.NET Core Web API** with **Clean Architecture**
- **Entity Framework Core** + **PostgreSQL**
- **JWT Authentication** & **Role-Based Authorization**
- **FluentValidation** + **Swagger Integration**
- **Custom Access Control** to restrict users to their own restaurant's data
- **Clean Routing** with Kebab-case URL Convention

### 🚀 Key Features

- Fully modular structure focused on separation of concerns
- Complete Swagger UI with Persian descriptions, request/response samples, and validation metadata
- Fine-grained access control using ownership-based filtering (users can access only their own data)
- Full implementation of authentication and authorization with support for multiple roles (Admin, Owner, etc.)
- Dynamic validation extensions based on entity Data Annotations
- File management system (e.g., food images) built as a separate service following SOLID principles

### Prerequisites
- .NET 9 SDK
- PostgreSQL
