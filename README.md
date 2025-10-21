## General Purpose
* This solution is designed as an N-tier application using multiple data sources, which are consumed by a front end.
* The contrived use cases include 2 separate APIs: 
  * Data Source 1 - Monitoring API: Displays a list of system health messages from various services
  * Data Source 2 - LoanApplication API: Composed of fields relevant to a loan application process.
       * Contains editable fields displayed together on a front end UI (Applicant Name, Type of Loan, Amount Requested, Credit Score and Time Stamp)
* Design is meant to allow projects consuming unrelated data sets to be hosted together while maintaining separation of concerns.

## Solution Components
              ┌───────────────────────────────┐
              │           UI Layer            │ **Note:** Razor pages used for simplicity & integration of this limited time and scoped project.
              │ LoanApplicationMonitor.WebApp │           For larger projects with more complex states,  I would have opted for a react front end UI.
              | Razor Pages consume APIs      |           
              | Maps to FE component          │
              └───────────────┬───────────────┘
                              │ HTTP/REST
                              ▼
              ┌───────────────────────────────┐
              │        API Layer              │
              │ LoanApplicationMonitor.API    │
              │ MonitoringApi                 │
              │ LoansApi                      │
              │                               │
              └───────────────┬───────────────┘
                              │ Uses Core Services
                              ▼
              ┌───────────────────────────────┐
              │        Core / Domain          |
              | LoanApplicationMonitor.Core   │
              │ Interfaces, DTOs, Domain Logic│
              │                               │
              └───────────────┬───────────────┘
                              │ Implements
                              ▼
              ┌───────────────────────────────┐
              │         Data Layer            |
              │ LoanApplicationMonitor.Data   │
              │ LoansDbContext                │
              | MonitoringDbContext           │ 
              │                               │
              └───────────────────────────────┘
                         Test Project
              ┌───────────────────────────────┐
              │                               | **Note:** Test project references Core, Data, and API projects but is not referenced by other projects.
              │ LoanApplicationMonitor.Test   │           Mocks are used here to test specific functionality in isolation.
              │                               │
              │                               │
              └───────────────────────────────┘