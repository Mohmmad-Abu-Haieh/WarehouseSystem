using Entity.Context;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Entity.SeedData
{
    public static class SeedUser
    {
        public static void Seed(AppDbContext appDbContext)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "SeedData", "users.json");

            if (!File.Exists(path))
            {
                Console.WriteLine("users.json not found");
                return;
            }

            var jsonText = File.ReadAllText(path);
            var seedUsers = JsonConvert.DeserializeObject<List<User>>(jsonText);

            foreach (User item in seedUsers)
            {
                CreatePasswordHash("P@ssw0rd", out byte[] passwordHash, out byte[] passwordSalt);
                item.PasswordSalt = passwordSalt;
                item.PasswordHash = passwordHash;
                item.CreatedBy = "Seed";
                item.CreatedOn = DateTime.UtcNow;

                appDbContext.Users.Add(item);
            }

            appDbContext.SaveChanges();
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
