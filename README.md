## Setup Instructions
1 - Install npm package `azurite v3` package from project root folder
  [Azurite](https://www.npmjs.com/package/azurite) is an open source emulator used to simulate azure storage with minimal dependencies

```
# Visual Studio: tools - command line - open developer command prompt (or separate bash terminal)
# navigate to ../ApplicationMonitor (root of project)

npm install --save-dev azurite
```

[Azurite emulator for development](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite)
  Azurite v3 requires [Node](https://nodejs.org/en) version 18 (or higher)

```
# verify version
node -v 
```
2 - Run the command to start azurite in the project directory

```
azurite --location ./.azurite

```

3 - Set Visual Studio to Run API and WebApp Project on Start up
     * right-click `LoanApplicationMonitor` solution --> properties 
     * multiple start-up projects `LoanApplicationMonitor.Api set to Start` `LoanApplicationMonitor.WebApp set to Start` click apply / ok
          
4 - Start Project (F5) to start API and WebApp together
     * Initial Page may be empty during data seeding, once sample data has been seeded API will display and can be accessed through Swagger Docs
     * Tabbing away from Swagger UI Page will bring you back to WebApp UI with updated sample data.
     * LoanDemoDB is spun up locally as part of startup process and can be inspected to view sample data as well as schema info.

## Features
* Navigate between System Health Monitoring Messages Page (Dashboard Read-Only) and Loan Application Page (CRUD app) via top left navigation bar
* System Message Page - View displays system health message status
     * **Pagination** - click bottom page navigation to view additional messages
* Loan Application Page - View displays loan application status
     * `Add New` - click to open form to add a new loan application, save to save record, cancel closes without saving
     * `Edit` - opens form with existing data populated for that record, changes can be made with `Save`, or `Cancel` without saving
     * `Edit` - `Copy As New` - copies existing data (without applicant name) to a new form which can then be saved with a new applicant name to add as a new record
     * Error Messages - when attempting to add or update any records with invalid data (i.e. adding a name already in the database) an error message is shown
     * `Select` to compare 2 records - click on 2 records to have highlights added to matching fields 
          * *selecting more than 2 records will only compare the latest 2 records selected*

## Notes on Solution Architecture
* This solution is designed as an N-tier application using multiple data sources, which are consumed by a front end.
* The contrived use cases include 2 separate Data sources backed by APIs: 
  * Azure Blob JSON file - Monitoring API: Displays a list of system health messages from various services
  * Azure SQL Db - LoanApplication API: Composed of fields relevant to a loan application process.

**UI Layer** - Razor pages used in this case due to limited state changes and ease of demo, however, a separate SPA project (i.e. React) could be 
               created as part of a future iteration which could consume the same APIs without needing to update the back end.
**Test Layer** - Separate xUnit Test project included which references .Core and .API and holds basic unit tests

```
   ┌───────────────────────────────┐
   │           UI Layer            │
   │ LoanApplicationMonitor.WebApp │
   │ Razor Pages consume APIs      │
   └───────────────┬───────────────┘
                   | HTTP/REST
                   ▼
   ┌───────────────────────────────┐
   │        API Layer              │
   │ LoanApplicationMonitor.API    │
   │ LoansApi (reads from SQL Db)  │
   │ MonitorApi (reads from Blob)  │
   └───────────────┬───────────────┘
                   | Uses Core Services
                   ▼
   ┌───────────────────────────────┐
   │        Core / Domain          │
   │ LoanApplicationMonitor.Core   │
   │ Interfaces, DTOs, Domain Logic│
   └───────────────┬───────────────┘
                   | Implements
                   ▼
   ┌───────────────────────────────┐
   │          Data Layer           │
   │ LoanApplicationMonitor.Data   │
   │ - LoansDbContext (EF Core)    │
   │ - LoansRepo                   │
   │ - HealthMonitoringRepo        │
   └───────────────────────────────┘
   ```
