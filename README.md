
# OrderProcessingSystem

The **OrderProcessingSystem** is an API built using **ASP.NET Core** that allows customers to place, retrieve, and fulfill orders. This API includes endpoints for creating orders, getting order details, and marking orders as fulfilled. The system supports logging via **Serilog** and interacts with a database via **Entity Framework Core**.

## Features
- **Create Order**: Allows customers to create orders with product IDs.
- **Get Order Details**: Allows fetching the order details by order ID.
- **Fulfill Order**: Allows marking an order as fulfilled.

## Table of Contents
- [Overview](#overview)
- [Technologies Used](#technologies-used)
- [API Endpoints](#api-endpoints)
- [Running the Project](#running-the-project)
- [License](#license)

---

## Overview

This project provides an API for managing orders in a processing system. The **OrderProcessingSystem** handles:
1. **Order Creation** (POST): Creates an order for a customer with selected products.
2. **Order Retrieval** (GET): Retrieves the details of a particular order.
3. **Order Fulfillment** (PUT): Marks an order as fulfilled.

---

## Technologies Used

- **ASP.NET Core**: Framework for building the web API.
- **Entity Framework Core**: ORM for database interaction.
- **Serilog**: For logging purposes in the API.
- **SQL Server**: Database used to persist order data.

---

## API Endpoints

### 1. **POST /api/orders**

- **Description**: Create an order for a customer.
- **Request Body**:
  ```json
  {
    "CustomerId": 1,
    "ProductIds": [101, 102]
  }
  ```
- **Response**:
  - `201 Created` if the order is successfully created.
  - `400 Bad Request` if the request body is invalid.

### 2. **GET /api/orders/{id}**

- **Description**: Fetch order details by order ID.
- **Request**: Order ID as a path parameter.
- **Response**:
  - `200 OK` with the order details if found.
  - `404 Not Found` if the order does not exist.

### 3. **PUT /api/orders/{id}/fulfill**

- **Description**: Fulfill an order.
- **Request**: Order ID as a path parameter.
- **Response**:
  - `204 No Content` if the order is successfully fulfilled.
  - `404 Not Found` if the order does not exist.

---

## Running the Project

### Prerequisites

1. **.NET SDK**: Make sure you have the latest .NET SDK installed on your machine. You can download it from [here](https://dotnet.microsoft.com/download).
2. **SQL Server**: Ensure that SQL Server is installed and running on your machine for database interactions.

### Steps to Run the API

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/emeermeer8057/OrderProcessSystem
   cd OrderProcessingSystem
   ```

2. **Restore NuGet Packages**:
   ```bash
   dotnet restore
   ```

3. **Build the Project**:
   ```bash
   dotnet build
   ```

4. **Run the API**:
   ```bash
   dotnet run
   ```

5. Open your browser and navigate to `https://localhost:5001/api/orders` to interact with the API.

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more information.

