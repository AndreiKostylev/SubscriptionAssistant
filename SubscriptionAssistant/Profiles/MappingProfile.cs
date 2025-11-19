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
            // User mappings
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));

            CreateMap<CreateUserDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => 1)) // Default User role
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Subscriptions, opt => opt.Ignore());

            CreateMap<RegisterRequest, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => 1)) // Default User role
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Subscriptions, opt => opt.Ignore());

            // Остальные маппинги...
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
