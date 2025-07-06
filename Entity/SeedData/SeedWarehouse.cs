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
    public static class SeedWarehouse
    {
        public static void Seed(AppDbContext appDbContext)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "SeedData", "warehouses.json");

            if (!File.Exists(path))
            {
                Console.WriteLine("ملف role.json غير موجود.");
                return;
            }

            var jsonText = File.ReadAllText(path);
            var seedWarehouses = JsonConvert.DeserializeObject<List<Warehouse>>(jsonText);

            foreach (var warehouse in seedWarehouses)
            {
                var exists = appDbContext.Warehouses.Any(u => u.Id == warehouse.Id && u.Name == warehouse.Name);
                if (exists)
                    continue;
                warehouse.CreatedBy = "Seed";
                warehouse.CreatedOn = DateTime.UtcNow;

                appDbContext.Warehouses.Add(warehouse);
            }
            appDbContext.SaveChanges();
        }
    }
}
