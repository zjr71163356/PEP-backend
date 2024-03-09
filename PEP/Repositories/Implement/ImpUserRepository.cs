using Microsoft.EntityFrameworkCore;
using PEP.Data;
using PEP.Models.Domain;
using PEP.Models.DTO.User;
using PEP.Repositories.Interface;

namespace PEP.Repositories.Implement
{
    public class ImpUserRepository : IUserRepository
    {
        private readonly FinalDesignContext dbContext;

        public ImpUserRepository(FinalDesignContext dbContext)
        {

            this.dbContext = dbContext;
        }

        public async Task<bool> IsUserAccountTakenAsync(string userAccount)
        {
            return await dbContext.Users.AnyAsync(u => u.Account == userAccount);
        }

        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            return await dbContext.Users.AnyAsync(u => u.UserName == username);
        }

        public async Task<User?> LoginUserAsync(User user)
        {
            var loginResult = await dbContext.Users.FirstOrDefaultAsync(u => u.Account == user.Account && u.Password == user.Password);
            if (loginResult == null)
            {
                return null;
            }
            return loginResult;
        }

        public async Task<bool> RegisterUserAsync(User userRegister)
        {
            userRegister.Role = "User";
            await dbContext.Users.AddAsync(userRegister);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }

}
