# CustomerApi

A REST API built with .NET 8 to manage customer records. Supports creation and retrieval of customers with validation and persistent storage.

## Features

- `POST /customers`: Accepts a list of customers, validates them, and inserts them in a sorted manner (by last name, then first name) **without using built-in sorting functions**.
- `GET /customers`: Returns the list of stored customers.
- Customers are persisted across server restarts.

## Validation Rules

- All required fields (`firstName`, `lastName`, `age`, `id`) must be present.
- Age must be **over 18**.
- The `id` must be integer and unique (not previously used).
- New customers are inserted sorted by last name and then first name maintaining order without using `.Sort()` or LINQ `OrderBy`.

## Build and Run

```bash
# Navigate to the CustomerApi project
cd CustomerApi

# Restore dependencies
dotnet restore

# Build the API
dotnet build

# Run the API
dotnet run
```

The API will be available by default at:

- http://localhost:5000

## Live Demo

The Customer API is deployed on Azure App Service and available at:

**[https://testcustomerapi.azurewebsites.net/api/customers](https://testcustomerapi.azurewebsites.net/api/customers)**

## API Reference

### GET /customers

**Description:** Returns the full list of stored customers.

* **Method:** `GET`
* **URL:** `/customers`
* **Response (200 OK):**

  ```json
  [
    {
      "firstName": "Leia",
      "lastName": "Chan",
      "age": 25,
      "id": 1
    },
    {
      "firstName": "Frank",
      "lastName": "Powell",
      "age": 30,
      "id": 2
    }
  ]
  ```

### POST /customers

**Description:** Accepts a list of customers, validates them, and inserts them in sorted order.

* **Method:** `POST`
* **URL:** `/customers`
* **Headers:** `Content-Type: application/json`
* **Request Body:**

  ```json
  [
    {
      "firstName": "Leia",
      "lastName": "Chan",
      "age": 25,
      "id": 1
    },
    {
      "firstName": "Frank",
      "lastName": "Powell",
      "age": 30,
      "id": 2
    }
  ]
  ```
* **Responses:**

  * **201 Created:** Customers successfully inserted.
  * **400 Bad Request:** Invalid data.

## Deployment (CI/CD)

A GitHub Actions workflow automatically builds, and deploys the API to Azure App Service when changes are pushed to the `main` branch.

