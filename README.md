UserProductApp is a simple, yet robust app. The user registration process is straightforward, with a dedicated POST endpoint specified in the Swagger API. The request body requires essential parameters such as email, password, first name, and last name. To ensure data integrity, backend validation mechanisms are in place to verify compliance with predefined criteria, including valid email formats and robust passwords. Upon successful registration, users receive a confirmation email, and their details are securely stored in the database.The login process is equally efficient, with a POST endpoint designated for user authentication. Upon validation, a JSON Web Token (JWT) is generated and returned in the response, enabling secure access to subsequent API requests. This token must be included in the Authorization header for authenticated requests.The application's core functionality revolves around product management, offering a range of CRUD operations. These endpoints enable users to effortlessly manage products, with pagination and filter support ensuring a seamless experience.

IMPLEMENTATIONS:

The Register Endpoint:
Parameters: first name,   last name, email, password
Validations: email format, unique password
Confirmation email sent to the user
User details saved in the database

The Login Endpoint:
Parameters: email, password
Validations: authenticate user details
Generates Token (JWT) upon successful login
Token included in the Authorization header

Product Endpoints:
    POST /api/Products - Creates a new product
    GET /api/Products/{id} - Retrieves a product by its Id
    GET /api/all/Products - Retrieves all products with pagination and filtering
    PUT /api/Products/{id} - Updates an existing product
    DELETE /api/Products/{id} - Deletes a product


.NET 8 SDK
SQL Server

NAVIGATION:

Clone the Repo 
git clone https://github.com/dejidee0/User-Management-Profile.git

 Run 'dotnet restore' to restore dependencies

Database : MSSQL was used
Configure connectionstring in the appsettings

Database Setup

Add-Migration <Name of your migration>
Update-Database

Running the Program

Start the Application: 
Enter 'dotnet run' on your terminal

Access Swagger UI
    Navigate to https://localhost:7228 swagger in your web browser to view and interact with the API endpoints

