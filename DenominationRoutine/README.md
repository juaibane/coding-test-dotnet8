# DenominationRoutine

A .NET 8 console application that calculates all possible combinations for dispensing specified amounts from an ATM with three bill denominations: 10€, 50€, and 100€.

## Features

- Computes combinations for a predefined set of payout amounts:
  - 30 EUR
  - 50 EUR
  - 60 EUR
  - 80 EUR
  - 140 EUR
  - 230 EUR
  - 370 EUR
  - 610 EUR
  - 980 EUR
- Uses an algorithm to generate combinations.
- Outputs each payout amount followed by its possible bill combinations.

## How It Works

1. Defines the available denominations: 10, 50, 100.
2. Iterates through each target amount and builds valid combinations.
3. Displays results grouped by payout amount.

## Build and Run

```bash
# Navigate to the project folder
dotnet restore

dotnet build

dotnet run
```

The console output will list each target amount and all valid combinations, for example:

```
Payout: 100 EUR
- 10 x 10
- 1 x 50 + 5 x 10
- 2 x 50
- 1 x 100
```



