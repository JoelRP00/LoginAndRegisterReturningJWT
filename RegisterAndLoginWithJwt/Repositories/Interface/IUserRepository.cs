using RegisterAndLoginWithJwt.Model;

namespace RegisterAndLoginWithJwt.Repositories.Interface
{
    public interface IUserRepository
    {
        public Task<User> CreateAsync(User user);
        public Task<User> GetAsync(string username);
        public bool IsUsernameExistent(string username);
    }
}
