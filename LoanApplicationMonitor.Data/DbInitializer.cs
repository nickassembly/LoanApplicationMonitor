using LoanApplicationMonitor.Core.Entities;

namespace LoanApplicationMonitor.Data
{
    public static class DbInitializer
    {
        public static void Seed(LoanApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Loans.Any())
            {
                var loans = GetSampleLoans();
                context.Loans.AddRange(loans);
                context.SaveChanges();
                Console.WriteLine($"Seeded {loans.Count} Loans.");
            }
            else
            {
                Console.WriteLine("Loans table already has data. Skipping seeding.");
            }
        }

        private static List<Loan> GetSampleLoans()
        {
            return new List<Loan>
            {
                new Loan { ApplicantFullName="Alice Johnson", LoanType="Home", LoanAmount=250000, CreditScore=720, LoanRequestReason="Buying first home" },
                new Loan { ApplicantFullName="Bob Smith", LoanType="Personal", LoanAmount=15000, CreditScore=680, LoanRequestReason="Medical expenses" },
                new Loan { ApplicantFullName="Carol Lee", LoanType="Auto", LoanAmount=30000, CreditScore=700, LoanRequestReason="New car purchase" },
                new Loan { ApplicantFullName="David Kim", LoanType="Student", LoanAmount=40000, CreditScore=710, LoanRequestReason="Graduate school tuition" },
                new Loan { ApplicantFullName="Eva Green", LoanType="Home", LoanAmount=320000, CreditScore=730, LoanRequestReason="Upgrading home" },
                new Loan { ApplicantFullName="Frank Miller", LoanType="Personal", LoanAmount=12000, CreditScore=660, LoanRequestReason="Wedding expenses" },
                new Loan { ApplicantFullName="Grace Park", LoanType="Auto", LoanAmount=25000, CreditScore=690, LoanRequestReason="Used car purchase" },
                new Loan { ApplicantFullName="Henry Wilson", LoanType="Student", LoanAmount=35000, CreditScore=700, LoanRequestReason="MBA program" },
                new Loan { ApplicantFullName="Ivy Chen", LoanType="Home", LoanAmount=280000, CreditScore=725, LoanRequestReason="First home purchase" },
                new Loan { ApplicantFullName="Jack Brown", LoanType="Personal", LoanAmount=8000, CreditScore=670, LoanRequestReason="Home renovation" },
                new Loan { ApplicantFullName="Karen Davis", LoanType="Auto", LoanAmount=18000, CreditScore=695, LoanRequestReason="Car replacement" },
                new Loan { ApplicantFullName="Leo Taylor", LoanType="Student", LoanAmount=30000, CreditScore=705, LoanRequestReason="College tuition" },
                new Loan { ApplicantFullName="Mia Lopez", LoanType="Home", LoanAmount=400000, CreditScore=740, LoanRequestReason="Buying a bigger house" },
                new Loan { ApplicantFullName="Nathan Scott", LoanType="Personal", LoanAmount=22000, CreditScore=680, LoanRequestReason="Debt consolidation" },
                new Loan { ApplicantFullName="Olivia Reed", LoanType="Auto", LoanAmount=27000, CreditScore=700, LoanRequestReason="New car for family" },
                new Loan { ApplicantFullName="Paul Adams", LoanType="Student", LoanAmount=45000, CreditScore=710, LoanRequestReason="Medical school tuition" },
                new Loan { ApplicantFullName="Quinn Harris", LoanType="Home", LoanAmount=310000, CreditScore=730, LoanRequestReason="Relocating to new city" },
                new Loan { ApplicantFullName="Rachel White", LoanType="Personal", LoanAmount=10000, CreditScore=665, LoanRequestReason="Vacation funding" },
                new Loan { ApplicantFullName="Sam Thompson", LoanType="Auto", LoanAmount=22000, CreditScore=690, LoanRequestReason="Car upgrade" },
                new Loan { ApplicantFullName="Tina Walker", LoanType="Student", LoanAmount=38000, CreditScore=715, LoanRequestReason="Graduate school" },
                new Loan { ApplicantFullName="Uma Patel", LoanType="Home", LoanAmount=290000, CreditScore=728, LoanRequestReason="First-time homebuyer" },
                new Loan { ApplicantFullName="Victor Evans", LoanType="Personal", LoanAmount=15000, CreditScore=675, LoanRequestReason="Home appliance purchase" },
                new Loan { ApplicantFullName="Wendy King", LoanType="Auto", LoanAmount=20000, CreditScore=695, LoanRequestReason="Family car" },
                new Loan { ApplicantFullName="Xander Lewis", LoanType="Student", LoanAmount=42000, CreditScore=710, LoanRequestReason="Law school tuition" },
                new Loan { ApplicantFullName="Yara Nelson", LoanType="Home", LoanAmount=350000, CreditScore=735, LoanRequestReason="Upgrading home with family" }
            };
        }
    }
}