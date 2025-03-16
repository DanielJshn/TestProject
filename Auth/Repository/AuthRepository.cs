using apief;
using Microsoft.EntityFrameworkCore;

namespace apief
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContextEF _dataContext;

        public AuthRepository(DataContextEF dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(u => u.email == email);
        }


        public async Task AddUserAsync(User user)
        {
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
        }


        public async Task DeleteData(Guid userId)
        {
            var user = await _dataContext.Users
                 .FirstOrDefaultAsync(p => p.id == userId);
            if (user != null)
            {
                _dataContext.Users.Remove(user);
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}