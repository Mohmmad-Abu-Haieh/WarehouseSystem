using Domain.Interface;
using DTO;
using Entity;
using Entity.Context;
using Entity.WorkContext;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.WorkContext;

namespace Domain.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _efDbContext;
        private readonly IConfiguration _configuration;
        private readonly SessionProvider _sessionProvider;

        public AuthService(AppDbContext efDbContext,IConfiguration configuration, SessionProvider sessionProvider)
        {
          
            _efDbContext = efDbContext;
            _configuration = configuration;
            _sessionProvider = sessionProvider;
        }
        public async Task<LoggedInUser> LoginUser(LoginUser model)
        {
            var user = await GetUserByEmail(model.Email.ToLower());

            if (user == null)
            {
                return null;
            }
            var loggedInUser = new LoggedInUser
            {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    Mobile = user.Mobile,
                    RoleId = user.RoleId,
            };
            _sessionProvider.InitialiseCurrentUser(loggedInUser);

            if (!Verifypassword_hash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            return loggedInUser;
        }
        private static bool Verifypassword_hash(string password, byte[] password_hash, byte[] password_salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(password_salt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != password_hash[i]) return false;
                }
            }
            return true;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            var user = _efDbContext.Users.FirstOrDefault(x => x.Email == email);

            if ( user == null)
            {
                return null;
            }
            return user;
        }
    }
}
