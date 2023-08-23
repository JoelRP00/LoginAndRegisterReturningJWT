using RegisterAndLoginWithJwt.DTO;
using RegisterAndLoginWithJwt.Model;

namespace RegisterAndLoginWithJwt.Services.Interface
{
    public interface IUserServices
    {
        public Task<User> Register(UserDTO userDTO);
        public Task<string> Login(UserDTO userDTO);
    }
}
