using AutoMapper;
using LoanApplicationMonitor.API.Dtos;
using LoanApplicationMonitor.Core.Entities;

namespace LoanApplicationMonitor.API.Mappers
{
    public class LoanMapperProfile : Profile
    {
        public LoanMapperProfile()
        {
            CreateMap<Loan, LoanReadDto>();
            CreateMap<LoanCreateDto, Loan>().ReverseMap();
            CreateMap<LoanUpdateDto, Loan>();
        }
    }


}
