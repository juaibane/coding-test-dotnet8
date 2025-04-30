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

The Customer API is deployed and available at:

**https://testcustomerapi.azurewebsites.net/api/customers**


## Example POST Request

`POST http://localhost:5000/customers`

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

