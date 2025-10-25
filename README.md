## Features
* Navigate between System Health Monitoring Messages Page and Loan Application Page via top left navigation bar
* System Message Page - View displays system health message status
     * Pagination - click bottom page navigation to view additional messages
* Loan Application Page - View displays loan application status
     * Add New Loan button - click to open form to add a new loan application, save to save record, cancel closes without saving
     * Edit - opens form with existing data populated for that record, changes can be made and saved, or cancelled without saving
     * Edit -> Copy As New - copies existing data (without applicant name) to a new form which can then be saved with a new applicant name to add as a new record
     * Error Messages - when attempting to add or update any records with invalid data (i.e. adding a name already in the database) an error message is shown
* Compare Field Contents - click on 2 records to have highlights added to matching fields, 
                           note: selecting more than 2 records will only compare the latest 2 records selected

## About this Demo
* This solution is designed as an N-tier application using multiple data sources, which are consumed by a front end.
* The contrived use cases include 2 separate APIs: 
  * Data Source 1 (Azure Blob JSON file) - Monitoring API: Displays a list of system health messages from various services
  * Data Source 2 (Azure SQL Db) - LoanApplication API: Composed of fields relevant to a loan application process.

## Solution Notes
**UI Layer** - Razor pages used in this case due to limited state changes and minimal business logic. 
         As complexity of state increases, a SPA framework such as React could be implemented and consume the same APIs

**Test Layer** - Separate xUnit Test project included which references .Core and .API and holds basic unit tests

## Solution Structure
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
