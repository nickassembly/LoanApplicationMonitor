using LoanApplicationMonitor.Core.Entities;

namespace LoanApplicationMonitor.Data
{
    public static class HealthMonitoringMessageFactory
    {
        public static List<HealthMonitoringMessage> GetSampleMessages()
        {
            return new List<HealthMonitoringMessage>
            {
                new HealthMonitoringMessage { Id = 1, SystemName = "AuthenticationService", StatusValue = StatusValue.pass, SystemMessage = "All endpoints healthy", TestCompleted = DateTime.UtcNow.AddMinutes(-10) },
                new HealthMonitoringMessage { Id = 2, SystemName = "PaymentGateway", StatusValue = StatusValue.fail, SystemMessage = "Timeout connecting to provider", TestCompleted = DateTime.UtcNow.AddMinutes(-15) },
                new HealthMonitoringMessage { Id = 3, SystemName = "NotificationService", StatusValue = StatusValue.warning, SystemMessage = "Email queue delay", TestCompleted = DateTime.UtcNow.AddMinutes(-20) },
                new HealthMonitoringMessage { Id = 4, SystemName = "UserProfileService", StatusValue = StatusValue.pass, SystemMessage = "Profile retrieval OK", TestCompleted = DateTime.UtcNow.AddMinutes(-25) },
                new HealthMonitoringMessage { Id = 5, SystemName = "LoanProcessingService", StatusValue = StatusValue.pass, SystemMessage = "Batch job completed", TestCompleted = DateTime.UtcNow.AddMinutes(-30) },
                new HealthMonitoringMessage { Id = 6, SystemName = "ReportingService", StatusValue = StatusValue.fail, SystemMessage = "Database query failed", TestCompleted = DateTime.UtcNow.AddMinutes(-35) },
                new HealthMonitoringMessage { Id = 7, SystemName = "AnalyticsService", StatusValue = StatusValue.warning, SystemMessage = "Slow response times", TestCompleted = DateTime.UtcNow.AddMinutes(-40) },
                new HealthMonitoringMessage { Id = 8, SystemName = "AuthService", StatusValue = StatusValue.pass, SystemMessage = "Token generation OK", TestCompleted = DateTime.UtcNow.AddMinutes(-45) },
                new HealthMonitoringMessage { Id = 9, SystemName = "CustomerService", StatusValue = StatusValue.pass, SystemMessage = "API endpoints responsive", TestCompleted = DateTime.UtcNow.AddMinutes(-50) },
                new HealthMonitoringMessage { Id = 10, SystemName = "SchedulerService", StatusValue = StatusValue.warning, SystemMessage = "Delayed task execution", TestCompleted = DateTime.UtcNow.AddMinutes(-55) },
                new HealthMonitoringMessage { Id = 11, SystemName = "EmailService", StatusValue = StatusValue.pass, SystemMessage = "SMTP OK", TestCompleted = DateTime.UtcNow.AddMinutes(-60) },
                new HealthMonitoringMessage { Id = 12, SystemName = "SMSService", StatusValue = StatusValue.fail, SystemMessage = "SMS gateway unreachable", TestCompleted = DateTime.UtcNow.AddMinutes(-65) },
                new HealthMonitoringMessage { Id = 13, SystemName = "NotificationService", StatusValue = StatusValue.pass, SystemMessage = "Push notifications delivered", TestCompleted = DateTime.UtcNow.AddMinutes(-70) },
                new HealthMonitoringMessage { Id = 14, SystemName = "DatabaseService", StatusValue = StatusValue.pass, SystemMessage = "Read/write latency normal", TestCompleted = DateTime.UtcNow.AddMinutes(-75) },
                new HealthMonitoringMessage { Id = 15, SystemName = "CacheService", StatusValue = StatusValue.warning, SystemMessage = "High memory usage", TestCompleted = DateTime.UtcNow.AddMinutes(-80) },
                new HealthMonitoringMessage { Id = 16, SystemName = "FileStorageService", StatusValue = StatusValue.pass, SystemMessage = "Blob storage accessible", TestCompleted = DateTime.UtcNow.AddMinutes(-85) },
                new HealthMonitoringMessage { Id = 17, SystemName = "PaymentGateway", StatusValue = StatusValue.pass, SystemMessage = "Transactions processing normally", TestCompleted = DateTime.UtcNow.AddMinutes(-90) },
                new HealthMonitoringMessage { Id = 18, SystemName = "LoanProcessingService", StatusValue = StatusValue.warning, SystemMessage = "Job queue length high", TestCompleted = DateTime.UtcNow.AddMinutes(-95) },
                new HealthMonitoringMessage { Id = 19, SystemName = "ReportingService", StatusValue = StatusValue.pass, SystemMessage = "Reports generated successfully", TestCompleted = DateTime.UtcNow.AddMinutes(-100) },
                new HealthMonitoringMessage { Id = 20, SystemName = "AuthService", StatusValue = StatusValue.fail, SystemMessage = "OAuth endpoint error", TestCompleted = DateTime.UtcNow.AddMinutes(-105) },
                new HealthMonitoringMessage { Id = 21, SystemName = "SchedulerService", StatusValue = StatusValue.pass, SystemMessage = "Scheduled tasks running", TestCompleted = DateTime.UtcNow.AddMinutes(-110) },
                new HealthMonitoringMessage { Id = 22, SystemName = "EmailService", StatusValue = StatusValue.warning, SystemMessage = "Some emails delayed", TestCompleted = DateTime.UtcNow.AddMinutes(-115) },
                new HealthMonitoringMessage { Id = 23, SystemName = "SMSService", StatusValue = StatusValue.pass, SystemMessage = "Messages sent successfully", TestCompleted = DateTime.UtcNow.AddMinutes(-120) },
                new HealthMonitoringMessage { Id = 24, SystemName = "CacheService", StatusValue = StatusValue.pass, SystemMessage = "Cache hit ratio normal", TestCompleted = DateTime.UtcNow.AddMinutes(-125) },
                new HealthMonitoringMessage { Id = 25, SystemName = "FileStorageService", StatusValue = StatusValue.warning, SystemMessage = "Some file uploads delayed", TestCompleted = DateTime.UtcNow.AddMinutes(-130) },new HealthMonitoringMessage { Id = 10, SystemName = "SchedulerService", StatusValue = StatusValue.warning, SystemMessage = "Delayed task execution", TestCompleted = DateTime.UtcNow.AddMinutes(-55) },
                new HealthMonitoringMessage { Id = 26, SystemName = "EmailServiceMessanger", StatusValue = StatusValue.warning, SystemMessage = "SMTP Warning", TestCompleted = DateTime.UtcNow.AddMinutes(-60) },
                new HealthMonitoringMessage { Id = 27, SystemName = "SMSServiceAsync", StatusValue = StatusValue.pass, SystemMessage = "SMS gateway reachable", TestCompleted = DateTime.UtcNow.AddMinutes(-65) },
                new HealthMonitoringMessage { Id = 28, SystemName = "NotificationService", StatusValue = StatusValue.pass, SystemMessage = "Push notifications delivered", TestCompleted = DateTime.UtcNow.AddMinutes(-70) },
                new HealthMonitoringMessage { Id = 29, SystemName = "DatabaseService", StatusValue = StatusValue.pass, SystemMessage = "Read/write latency normal", TestCompleted = DateTime.UtcNow.AddMinutes(-75) },
                new HealthMonitoringMessage { Id = 30, SystemName = "CacheServiceDistributed", StatusValue = StatusValue.warning, SystemMessage = "High memory usage", TestCompleted = DateTime.UtcNow.AddMinutes(-80) },
                new HealthMonitoringMessage { Id = 31, SystemName = "FileStorageService", StatusValue = StatusValue.pass, SystemMessage = "Blob storage accessible", TestCompleted = DateTime.UtcNow.AddMinutes(-85) },
                new HealthMonitoringMessage { Id = 32, SystemName = "PaymentGateway-New", StatusValue = StatusValue.warning, SystemMessage = "Transactions waiting", TestCompleted = DateTime.UtcNow.AddMinutes(-90) },
                new HealthMonitoringMessage { Id = 33, SystemName = "LoanProcessingService", StatusValue = StatusValue.warning, SystemMessage = "Job queue length high", TestCompleted = DateTime.UtcNow.AddMinutes(-95) },
                new HealthMonitoringMessage { Id = 34, SystemName = "ReportingService", StatusValue = StatusValue.pass, SystemMessage = "Reports generated successfully", TestCompleted = DateTime.UtcNow.AddMinutes(-100) },
                new HealthMonitoringMessage { Id = 35, SystemName = "AuthServiceDistributed", StatusValue = StatusValue.warning, SystemMessage = "OAuth endpoint pending", TestCompleted = DateTime.UtcNow.AddMinutes(-105) },
                new HealthMonitoringMessage { Id = 36, SystemName = "SchedulerService", StatusValue = StatusValue.pass, SystemMessage = "Scheduled tasks running", TestCompleted = DateTime.UtcNow.AddMinutes(-110) },
                new HealthMonitoringMessage { Id = 37, SystemName = "EmailService", StatusValue = StatusValue.warning, SystemMessage = "Some emails delayed", TestCompleted = DateTime.UtcNow.AddMinutes(-115) },
            };
        }
    }
}
