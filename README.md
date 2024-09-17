# Short Description
The **Car Rental Service** web application provides an integrated system for managing the rental process of vehicles. The system allows for the management of cars, customers, and rental transactions, including car returns. The application ensures efficient rental operations with validations for customer eligibility (18+ years), vehicle availability, overlapping rental dates, and return date calculations. Additionally, the system manages rental policies, such as minimum days for renting and late return fees, through a centralized **RentParams** configuration. The system includes both **Admin** and **User** roles, with separate access control and permissions. The backend is connected to Microsoft Azure for extended services, although certain external services are currently inactive.

This project is CRUD-based and structured to handle additional functionalities in the future, including a frontend expansion with modern UI frameworks and deeper integration with external services through APIs.

---

# Technologies Used
The **Car Rental Service** web application is built using:

- **C# .NET Core**: Provides the foundation for server-side application development and business logic.
- **Entity Framework Core**: Manages the ORM (Object-Relational Mapping) to interact with the database.
- **Microsoft SQL Server**: The database layer for storing and retrieving car, customer, and rental records.
- **Azure Cloud Services**: For hosting and communication with external applications.
- **ASP.NET Core MVC**: Facilitates the Model-View-Controller pattern for web development.
- **Identity Framework**: Manages user authentication and authorization, with roles for Admin and User.

---

# Application Structure

The application follows a layered architecture with **Domain Models**, **Service Layer**, **Repository Layer**, and **Controllers**.

## Domain Models
The domain models represent the core entities of the system:

### BaseEnum
- A base class inherited by all other entities to ensure unique identification.

### Car
Represents a vehicle available for rent:
- `Name`: The name of the car.
- `Description`: Description of the car.
- `Model`: Car model.
- `DateManufactured`: The date the car was manufactured.
- `KilometersTraveled`: Distance the car has traveled.
- `ColorVehicle`: Enum representing car color.
- `LicensePlate`: Unique identifier for each car.
- `Rents`: List of rental transactions associated with this car.

### Rent
Represents a car rental transaction:
- `CarId`: The car being rented.
- `CustomerId`: The user renting the car.
- `RentDate`: The start date of the rental.
- `ReturnDate`: The expected return date.
- `RentAmount`: The cost of the rental.
- `isActive`: Indicates if the rental is currently active.
- Relationships with `Car`, `Customer`, and `Return` entities.

### RentParams
Represents global rental policies:
- `MinimumDaysForRent`: The minimum number of days a car can be rented.
- `DiscountPercentage`: Discount for long-term rentals.
- `AdditionalFees`: Fees for late returns.

### Return
Represents the return of a rented car:
- `RentId`: Reference to the rental transaction.
- `ReturnDate`: Actual return date.
- `LateFee`: Fee for late return, if applicable.
- `TotalPrice`: Final price after applying fees and discounts.

---

## Web Layer
This layer handles the HTTP requests and routes them to the appropriate services.

### API Controllers
- **CarController**: Manages car CRUD operations.
- **RentController**: Handles the rental process, including validation for overlapping dates, rental eligibility, and car availability.
- **ReturnController**: Handles the return of rented cars, including late fees and final price calculations.
- **OfferParamsController**: Provides the configuration for rental rules, including minimum days and fees.
- **ExportController**: Exports rental data and other information through API endpoints.

### Validation
- **18+ Age Validation**: Ensures that customers renting cars are at least 18 years old.
- **License Plate Validation**: Checks for the uniqueness and format of car license plates.
- **Date Validation**: Prevents cars from being rented on overlapping dates.
- **Minimum Days Validation**: Ensures rental transactions adhere to minimum rental days specified in **RentParams**.
- **Late Return Validation**: Calculates late fees if the car is returned after the expected return date.

---

## Service Layer
The service layer contains business logic and communication with repositories.

### RentService
- **CheckAvailability**: Verifies if a car is available for the requested dates.
- **CalculateTotalAmount**: Computes the total rental amount, applying discounts and additional fees from **RentParams**.
- **ValidateRent**: Checks for overlapping rental dates and other business rules.

### ReturnService
- **ProcessReturn**: Handles the return process, calculates late fees, and finalizes the total rental price.
  
---

## Repository Layer
Handles database operations and interactions via **Entity Framework Core**.

### CarRepository
- **FindAvailableCars**: Retrieves cars available for the specified rental dates.
  
### RentRepository
- **GetActiveRents**: Retrieves currently active rentals for validation purposes.
- **FindByCustomer**: Fetches rental records associated with a specific customer.

### RentParamsRepository
- **GetCurrentParams**: Fetches the single record of **RentParams**, which defines the business rules.

---

## Authorization
The application enforces role-based access control with two key roles:
- **Admin**: Can manage all entities (cars, rents, returns) and modify system-wide parameters such as **RentParams**.
- **User**: Can rent and return cars, view their rental history, and update their profile.

---

## Future Development
- **Frontend Development**: Future iterations of this project will involve the development of a frontend using modern frameworks such as React.js or Angular.
- **Improved Azure Integration**: Currently, the application is linked to Azure services for scalability, but certain services are not yet fully operational.
- **Advanced Reporting and Analytics**: More advanced reporting and data analytics functionalities, based on rental statistics, may be introduced.

---

# How It Works?

1. **Car Management**: Admin users can add, edit, or delete car records, ensuring the availability of the vehicle for future rentals.
2. **Rental Process**: Users can browse available cars, rent a vehicle, and manage their reservations. The system validates availability and enforces business rules like minimum days for rent.
3. **Returns and Fees**: After a car is returned, the system calculates any applicable late fees based on the difference between the expected and actual return dates.
4. **Authorization and Authentication**: The system secures access based on roles, ensuring that only authorized users can perform certain operations.
5. **Data Export**: Admins can export data related to cars, customers, and rental transactions through dedicated API endpoints.



