## Setup Instructions
* After cloning repo, you will need to run `azurite` in a terminal in the directory of the cloned project.  
  Azurite is an emulation service that allows for emulating Azure blob storage and will allow the program to seed test data in development

* Run the command below -- you should see a response similar to ... "Azure blob service is starting at http:127.0.0.1:10000"
```
azurite --location ./.azurite
```
* After azurite is running, you can run the cloned project in visual studio and it will start up the API and WebApp projects, as well as
  create seed data for both data sources, a local DB in MSSQLDB--LoanDemoDB for the Loan API and the Blob `.json` data for the HealthMonitoringMessage API.

## Features
* Navigate between System Health Monitoring Messages Page (Dashboard Read-Only) and Loan Application Page (CRUD app) via top left navigation bar
* System Message Page - View displays system health message status
     * Pagination - click bottom page navigation to view additional messages
* Loan Application Page - View displays loan application status
     * Add New Loan button - click to open form to add a new loan application, save to save record, cancel closes without saving
     * Edit - opens form with existing data populated for that record, changes can be made and saved, or cancelled without saving
     * Edit -> Copy As New - copies existing data (without applicant name) to a new form which can then be saved with a new applicant name to add as a new record
     * Error Messages - when attempting to add or update any records with invalid data (i.e. adding a name already in the database) an error message is shown
* Compare Field Contents - click on 2 records to have highlights added to matching fields, 
                           note: selecting more than 2 records will only compare the latest 2 records selected

## Notes on Solution Architecture
* This solution is designed as an N-tier application using multiple data sources, which are consumed by a front end.
* The contrived use cases include 2 separate Data sources backed by APIs: 
  * Azure Blob JSON file - Monitoring API: Displays a list of system health messages from various services
  * Azure SQL Db - LoanApplication API: Composed of fields relevant to a loan application process.

**UI Layer** - Razor pages used in this case due to limited state changes and ease of demo, however, a separate SPA project (i.e. React) could be 
               created as part of a future iteration which could consume the same APIs without needing to update the back end.

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
