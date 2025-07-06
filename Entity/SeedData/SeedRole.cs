using Entity.Context;
using Entity.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.SeedData
{
    public static class SeedRole
    {

        public static void Seed(AppDbContext appDbContext)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "SeedData", "role.json");

            if (!File.Exists(path))
            {
                Console.WriteLine("ملف role.json غير موجود.");
                return;
            }

            var jsonText = File.ReadAllText(path);
            var seedRole = JsonConvert.DeserializeObject<List<Role>>(jsonText);

            foreach (var role in seedRole)
            {
                var exists = appDbContext.Roles.Any(r => r.Id == role.Id);
                if (exists)
                    continue; 
                role.CreatedBy = "Seed";
                role.CreatedOn = DateTime.UtcNow;

                appDbContext.Roles.Add(role);
            }

            appDbContext.SaveChanges();
        }

    }
}
