# OrderProcessSystem - Unit Test Coverage

This document provides information on the **unit test coverage** for the **OrderProcessSystem** API. The API is designed to handle order-related operations, and unit tests have been written using **xUnit** and **Moq** to verify the correctness of the code.

---

## Table of Contents
- [Overview](#overview)
- [Unit Test Coverage](#unit-test-coverage)
- [Test Cases](#test-cases)
- [Running the Tests](#running-the-tests)
- [Packages and Dependencies](#packages-and-dependencies)
- [Architecture Diagram](#architecture-diagram)

---

## Overview

The **OrderProcessSystem** API provides functionality for:
1. **Creating Orders**: A POST request that allows a customer to create an order with selected products.
2. **Getting Order Details**: A GET request to fetch order details by order ID.
3. **Fulfilling Orders**: A PUT request to mark an order as fulfilled.

In this project, **unit tests** are written to test the core logic and verify that the API works as expected. Mocking is done using **Moq** to simulate dependencies like the service layer and the database.

---

## Unit Test Coverage

The unit tests cover the following functionalities:

1. **Order Creation**:
   - Tests that the **POST /api/orders** endpoint correctly creates an order for a customer when valid data is provided.
   - Verifies that the order contains the correct products and customer ID.
   - Ensures that the total price of the order is correctly calculated.

2. **Order Retrieval**:
   - Tests that the **GET /api/orders/{id}** endpoint retrieves the correct order based on order ID.
   - Ensures that a `404 Not Found` response is returned when the order does not exist.

3. **Order Fulfillment**:
   - Tests that the **PUT /api/orders/{id}/fulfill** endpoint successfully marks an order as fulfilled.
   - Ensures that a `404 Not Found` response is returned if the order ID does not exist.

---

## Test Cases

The following test cases have been implemented in the unit test project:

### 1. **CreateOrderAsync Test**
   - **Description**: Verifies that a valid order is created successfully.
   - **Test Scenario**:
     - When a customer with valid product IDs requests an order, the order is created with the correct details.
   - **Expected Outcome**:
     - The order is created successfully.
     - The HTTP status code returned is `201 Created`.
     - The order total price is calculated correctly.

### 2. **GetOrderByIdAsync Test**
   - **Description**: Verifies that the order details are retrieved correctly by ID.
   - **Test Scenario**:
     - When an existing order ID is provided, the system returns the correct order details.
     - When an invalid order ID is provided, the system returns a `404 Not Found` status.
   - **Expected Outcome**:
     - The correct order details are returned.
     - A `404 Not Found` status is returned when the order does not exist.

### 3. **FulfillOrderAsync Test**
   - **Description**: Verifies that an order can be fulfilled successfully.
   - **Test Scenario**:
     - When a valid order ID is provided, the order is marked as fulfilled.
     - When an invalid order ID is provided, the system returns a `404 Not Found` status.
   - **Expected Outcome**:
     - The order is marked as fulfilled successfully.
     - A `404 Not Found` status is returned if the order does not exist.

### 4. **Edge Cases and Validation Tests**
   - **Invalid Product IDs**: Tests that an invalid product ID array results in a `400 Bad Request`.
   - **Missing Order Data**: Tests that a missing or empty request body results in a `400 Bad Request`.

---

## Running the Tests

Follow these steps to run the unit tests in **Visual Studio**:

1. **Open the Solution**: Open the **OrderProcessSystem.sln** file in **Visual Studio**.
   
2. **Restore NuGet Packages**: Ensure that all required NuGet packages are restored by using the NuGet Package Manager or Package Manager Console:
   
   ```bash
   Install-Package xUnit -Version 2.4.2
   Install-Package xunit.runner.visualstudio -Version 2.4.3
   Install-Package Moq -Version 4.20.72
   Install-Package Microsoft.NET.Test.Sdk -Version 17.3.2
