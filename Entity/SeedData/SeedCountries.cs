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
    public static class SeedCountries
    {
        public static void Seed(AppDbContext appDbContext)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "SeedData", "countries.json");

            if (!File.Exists(path))
            {
                Console.WriteLine("ملف role.json غير موجود.");
                return;
            }

            var jsonText = File.ReadAllText(path);
            var seedCountries = JsonConvert.DeserializeObject<List<Country>>(jsonText);

            foreach (var country in seedCountries)
            {
                var exists = appDbContext.Countries.Any(u => u.Name == country.Name);
                if (exists)
                    continue;
                country.CreatedBy = "Seed";
                country.CreatedOn = DateTime.UtcNow;

                appDbContext.Countries.Add(country);
            }
            appDbContext.SaveChanges();
        }
    }
}
