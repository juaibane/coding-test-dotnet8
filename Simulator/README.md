# Simulator

This is a console application that simulates sending concurrent POST and GET requests to the Customer API.

---

## Configuration

You can configure the following settings in the `appsettings.json` file:

- `ApiBaseUrl`: Base URL of the Customer API.
- `ParallelTasks`: Number of parallel tasks to execute.
- `MaxCustomersPerPostRequest`: Maximum number of customers per POST request.

---

## Simulation Requirements

The simulator follows these requirements for POST requests:

- Each request contains at least 2 different customers.
- Age is randomized between 10 and 90.
- IDs are assigned in increasing sequential order.
- First names and last names are randomly picked from the provided appendix list.

---

## Build and Run

To build and run the simulator:

```bash
# Navigate to the Simulator project
cd Simulator

# Restore dependencies
dotnet restore

# Build the app
dotnet build

# Run the simulator
dotnet run
