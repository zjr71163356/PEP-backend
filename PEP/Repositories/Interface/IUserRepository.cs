using PEP.Models.Domain;
using PEP.Models.DTO.User;

namespace PEP.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<bool> RegisterUserAsync(User user);

        Task<bool> IsUsernameTakenAsync(string username);

        Task<bool> IsUserAccountTakenAsync(string userAccount);

        Task<User?> LoginUserAsync(User user);
    }
}
