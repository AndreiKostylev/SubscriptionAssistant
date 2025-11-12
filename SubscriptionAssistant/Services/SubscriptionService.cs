using AutoMapper;
using SubscriptionAssistant.Models.DTO;
using SubscriptionAssistant.Models;
using SubscriptionAssistant.Repositories;

namespace SubscriptionAssistant.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public SubscriptionService(
            ISubscriptionRepository subscriptionRepository,
            IUserRepository userRepository,
            IServiceRepository serviceRepository,
            IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _userRepository = userRepository;
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все подписки пользователя
        /// </summary>
        public async Task<IEnumerable<SubscriptionDTO>> GetUserSubscriptionsAsync(int userId)
        {
            var subscriptions = await _subscriptionRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<SubscriptionDTO>>(subscriptions);
        }

        /// <summary>
        /// Получить подписку по ID
        /// </summary>
        public async Task<SubscriptionDTO?> GetSubscriptionByIdAsync(int id)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(id);
            return subscription == null ? null : _mapper.Map<SubscriptionDTO>(subscription);
        }

        /// <summary>
        /// Создать новую подписку
        /// </summary>
        public async Task<SubscriptionDTO> CreateSubscriptionAsync(CreateSubscriptionDTO subscriptionDto, int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) throw new ArgumentException("Пользователь не найден");

            var service = await _serviceRepository.GetByIdAsync(subscriptionDto.ServiceId);
            if (service == null) throw new ArgumentException("Сервис не найден");

            var subscription = _mapper.Map<Subscription>(subscriptionDto);
            subscription.UserId = userId;
            subscription.NextPaymentDate = CalculateNextPaymentDate(subscriptionDto.StartDate, subscriptionDto.BillingCycle);

            var createdSubscription = await _subscriptionRepository.CreateAsync(subscription);
            return _mapper.Map<SubscriptionDTO>(createdSubscription);
        }

        /// <summary>
        /// Деактивировать подписку
        /// </summary>
        public async Task<bool> DeactivateSubscriptionAsync(int id)
        {
            return await _subscriptionRepository.DeactivateSubscriptionAsync(id);
        }

        /// <summary>
        /// Получить подписки, у которых скоро закончится оплата
        /// </summary>
        public async Task<IEnumerable<SubscriptionDTO>> GetExpiringSubscriptionsAsync(int daysBeforeExpiry)
        {
            var subscriptions = await _subscriptionRepository.GetExpiringSubscriptionsAsync(daysBeforeExpiry);
            return _mapper.Map<IEnumerable<SubscriptionDTO>>(subscriptions);
        }

        private DateTime CalculateNextPaymentDate(DateTime startDate, string billingCycle)
        {
            return billingCycle.ToLower() switch
            {
                "yearly" => startDate.AddYears(1),
                _ => startDate.AddMonths(1)
            };
        }
    }
}
