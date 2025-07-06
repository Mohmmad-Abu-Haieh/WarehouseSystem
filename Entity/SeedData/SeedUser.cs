using Entity.Context;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.SeedData
{
    public static class SeedUser
    {
        public static void Seed(AppDbContext appDbContext)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "SeedData", "users.json");

            if (!File.Exists(path))
            {
                Console.WriteLine("ملف users.json غير موجود.");
                return;
            }

            var jsonText = File.ReadAllText(path);
            var seedUsers = JsonConvert.DeserializeObject<List<User>>(jsonText);

            foreach (var user in seedUsers)
            {
                var exists = appDbContext.Users.Any(u => u.Id == user.Id);
                if (exists)
                    continue;

                user.PasswordSalt = new byte[] { 32, 20, 152, 90, 26, 153, 249, 3, 219, 40, 229, 63, 40, 72, 223, 158, 141, 21, 78, 94, 35, 193, 227, 51, 191, 146, 127, 64, 37, 114, 222, 68, 196, 203, 170, 36, 216, 97, 4, 19, 9, 79, 110, 76, 73, 248, 90, 85, 117, 140, 157, 54, 163, 86, 253, 208, 26, 183, 33, 132, 193, 178, 0, 134, 15, 254, 60, 115, 120, 255, 225, 36, 94, 251, 240, 4, 197, 117, 203, 166, 241, 84, 35, 29, 162, 232, 194, 185, 121, 241, 162, 255, 110, 161, 0, 110, 83, 247, 201, 100, 18, 205, 108, 239, 119, 189, 9, 62, 220, 106, 20, 135, 178, 138, 209, 106, 148, 117, 36, 101, 122, 159, 22, 98, 88, 91, 37, 124 };
                user.PasswordHash = new byte[] { 222, 212, 74, 147, 102, 148, 188, 54, 153, 34, 221, 89, 12, 144, 231, 175, 114, 124, 74, 18, 57, 147, 237, 62, 128, 164, 41, 154, 0, 154, 229, 1, 243, 127, 131, 218, 197, 186, 80, 136, 16, 105, 58, 44, 100, 90, 202, 166, 56, 78, 11, 60, 61, 97, 159, 149, 30, 32, 66, 15, 209, 81, 123, 234 };
                user.CreatedBy = "Seed";
                user.CreatedOn = DateTime.UtcNow;

                appDbContext.Users.Add(user);
            }

            appDbContext.SaveChanges();
        }
    }
}
