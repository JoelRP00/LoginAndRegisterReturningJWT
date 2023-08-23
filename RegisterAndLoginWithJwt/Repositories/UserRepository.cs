using Microsoft.EntityFrameworkCore;
using RegisterAndLoginWithJwt.Data;
using RegisterAndLoginWithJwt.Model;
using RegisterAndLoginWithJwt.Repositories.Interface;

namespace RegisterAndLoginWithJwt.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<User> CreateAsync(User user)
        {
            await _db.AddAsync(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetAsync(string username)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public bool IsUsernameExistent(string username)
        {
            return _db.Users.Any(u => u.Username == username);
        }
    }
}
