# Employee Management System

## 1. Project Overview

This project is a Workforce Management SaaS solution for managing employees and their skills. It includes a RESTful API built with ASP.NET Core and a responsive frontend built with Next.js and Material-UI.  
Key features include employee and skill management, CSV import/export, filtering, sorting, and a clean UI for easy navigation.

## 2. Technologies Used
### Backend:
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swashbuckle (Swagger) for API documentation

### Frontend:
- Next.js (React.js)
- Material-UI (MUI) for UI components
- Axios for HTTP requests

### Tools:
- Visual Studio 2022 (Backend Development)
- Visual Studio Code (Frontend Development)
- Postman or Swagger for API testing
- Git for version control

## 3. Application Features
### Backend Features:
#### Employee Management:
- CRUD operations for employees.
- Assign or remove skills for employees.
- Filter employees by skills.
- Sort employees by surname or hire date.

#### Skill Management:
- CRUD operations for skills.
- View the list of all skills.

#### Import/Export Functionalities:
- Import employees with their skills from a CSV file.
- Export skills to a CSV file.

#### API Documentation:
- Swagger documentation is available at `/swagger/index.html`.

### Frontend Features:
#### Employees Page:
- View all employees.
- Filter employees by skill.
- Sort employees by surname or hire date.
- Search employees by name.
- Add, update, and delete employees.
- Import employees via CSV.

#### Skills Page:
- View all skills.
- Export skills to CSV.
- Add, update, and delete skills.

#### UI Design:
- Material-UI components for clean, responsive design.
- Pages adapt to desktop and tablet devices.

## 4. Installation and Setup Instructions
### Backend Setup:
1. Clone the project repository:
    ```bash
    git clone https://github.com/your-repo-url.git
    cd EmployeeManagementAPI
    ```
2. Install dependencies:
    Ensure you have .NET SDK installed.
3. Configure the database:
    Update the connection string in `appsettings.json` to match your SQL Server configuration.
4. Apply migrations:
    ```bash
    dotnet ef database update
    ```
5. Run the API:
    ```bash
    dotnet run
    ```
    API will be available at `https://localhost:5001` (or the port shown in the terminal).

### Frontend Setup:
1. Navigate to the frontend directory:
    ```bash
    cd employee-management-frontend
    ```
2. Install dependencies:
    ```bash
    npm install
    ```
3. Configure the API base URL:
    Update the API base URL in `src/app/utils/axios.js`:
    ```javascript
    const instance = axios.create({
      baseURL: "https://localhost:5001/api", // Your backend API URL
    });
    export default instance;
    ```
4. Run the Next.js application:
    ```bash
    npm run dev
    ```
    Access the frontend at `http://localhost:3000`.

## 5. API Documentation
API endpoints can be tested using Swagger.

Launch the backend and visit:
```bash
https://localhost:5001/swagger/index.html
```

### Key API Endpoints:
- `GET /api/employee`: Get all employees (with filters and sorting).
- `POST /api/employee`: Create a new employee.
- `PUT /api/employee/{id}`: Update an employee.
- `DELETE /api/employee/{id}`: Delete an employee.
- `POST /api/employee/import`: Import employees from CSV.
- `GET /api/skills`: Get all skills.
- `POST /api/skills`: Create a new skill.
- `PUT /api/skills/{id}`: Update a skill.
- `DELETE /api/skills/{id}`: Delete a skill.
- `GET /api/skills/export`: Exports all skills to a CSV

