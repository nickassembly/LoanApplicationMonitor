using AutoMapper;
using LoanApplicationMonitor.API.Dtos;
using LoanApplicationMonitor.Core.Entities;

namespace LoanApplicationMonitor.API.Mappers
{
    public class HealthMonitoringMessageMapperProfile : Profile
    {
        public HealthMonitoringMessageMapperProfile()
        {
            CreateMap<HealthMonitoringMessage, HealthMonitoringMessageReadDto>();
        }
    }
}
