# coding-test-dotnet8

A public repository containing 3 .NET 8 solutions for a coding test:

1. **DenominationRoutine**: Console app that computes all possible ATM payout combinations using 10€, 50€, and 100€ bills.
2. **CustomerApi**: A REST API with POST/GET endpoints for managing customers.
3. **Simulator**: Console app that sends randomized POST and GET requests in parallel to test the CustomerApi.

Each project includes its own detailed README.

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

---

## Projects

- **DenominationRoutine/**: see [DenominationRoutine/README.md](DenominationRoutine/README.md)
- **CustomerApi/**: see [CustomerApi/README.md](CustomerApi/README.md)
- **Simulator/**: see [Simulator/README.md](Simulator/README.md)

---

## Getting Started

To get started, clone the repository:

```bash
# Clone repository
git clone https://github.com/juaibane/coding-test-dotnet8.git


## Deployment

CustomerApi is hosted on Azure App Service and reachable at:

**https://testcustomerapi.azurewebsites.net/api/customers**

