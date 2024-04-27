using Stripe.Terminal;
using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Specific;

namespace Vehicle_Rent.Services.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;

        public ProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> GetCustomerByIdAsync(string id)
        {
            var customer = await _userRepository.GetEagerCustomerByIdAsync(id);
            return customer;    
        }
    }
}
