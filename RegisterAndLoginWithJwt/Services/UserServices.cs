using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using RegisterAndLoginWithJwt.DTO;
using RegisterAndLoginWithJwt.Model;
using RegisterAndLoginWithJwt.Repositories.Interface;
using RegisterAndLoginWithJwt.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace RegisterAndLoginWithJwt.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserServices(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            
        }
        public async Task<string> Login(UserDTO userDTO)
        {
            var user = await _userRepository.GetAsync(userDTO.Username);
            if (VerifyPassword(userDTO.Password, user.PasswordHash, user.PasswordSalt))
            {
                var token = CreateToken(user);
                return token;

            }
            else
            return "senha incorreta";
        }

        public async Task<User> Register(UserDTO userDTO)
        {
            if(_userRepository.IsUsernameExistent(userDTO.Username))
            {
                return null;
            }
            else
            {
                CreatePassword(userDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
                var user = new User();
                user.Username = userDTO.Username;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                return await _userRepository.CreateAsync(user);

            }
                
            
            
        }

        private void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                
                
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        
    }
}
