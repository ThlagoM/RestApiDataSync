# REST API Data Sync

A C#/.NET 8 application that synchronizes product data from an external REST API into a local SQLite database using Entity Framework Core.

## Features

- Fetches product data from a public REST API.
- Deserializes JSON responses into C# objects.
- Stores products in SQLite using Entity Framework Core.
- Inserts new products and updates existing records.
- Detects unchanged records to avoid unnecessary database updates.
- Prevents duplicate product IDs using a primary key.
- Records synchronization results in a local log file.
- Handles API, database and file operation errors.

## Technologies

- C# / .NET 8
- REST API and HttpClient
- JSON serialization
- Entity Framework Core 8
- SQLite
- Async/await

## How It Works

1. Requests product data from the Fake Store API.
2. Converts the JSON response into product objects.
3. Creates the local SQLite database if it does not exist.
4. Compares incoming products with existing database records.
5. Inserts new products, updates changed products and skips unchanged records.
6. Displays synchronization statistics and writes a summary to `sync.log`.

## Getting Started

1. Clone the repository.
2. Open the solution in Visual Studio 2022.
3. Restore NuGet packages.
4. Build and run the application.
5. Review the synchronization results in the console.

An internet connection is required to access the external API. The SQLite database is created automatically.

## Example Output

```text
=== REST API Data Sync ===
SQLite database ready.
Products received: 20
Products inserted: 0
Products updated: 0
Products unchanged: 20
Synchronization logged successfully.
```

## Limitations

This is a portfolio demonstration, not a production-ready synchronization platform.

- Uses a public demonstration API.
- Runs manually rather than on a schedule.
- Uses a local SQLite database.
- Does not currently implement automatic retries, pagination or automated tests.

## Purpose

Demonstrates practical experience with REST API integrations, database persistence, asynchronous programming and data synchronization using C#/.NET.