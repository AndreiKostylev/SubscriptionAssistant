using SubscriptionAssistant.Models;
using SubscriptionAssistant.Models.DTO;
using SubscriptionAssistant.Models.SubscriptionManager.Models;
using AutoMapper;

namespace SubscriptionAssistant.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
            CreateMap<User, UserDTO>();
            CreateMap<CreateUserDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

          
            CreateMap<Subscription, SubscriptionDTO>();
            CreateMap<CreateSubscriptionDTO, Subscription>();

           
            CreateMap<Category, CategoryDTO>();
            CreateMap<CreateCategoryDTO, Category>();

            
            CreateMap<Service, ServiceDTO>();
            CreateMap<CreateServiceDTO, Service>();

            
            CreateMap<Payment, PaymentDTO>();
            CreateMap<CreatePaymentDTO, Payment>();
        }
    }
}
