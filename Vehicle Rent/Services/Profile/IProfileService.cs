using Vehicle_Rent.Models;

namespace Vehicle_Rent.Services.Profile
{
    public interface IProfileService
    {
        public Task<User> GetCustomerByIdAsync(string id);
    }
}
