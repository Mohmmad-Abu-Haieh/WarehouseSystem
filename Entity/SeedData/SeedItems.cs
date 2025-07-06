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
    public static class SeedItems
    {
        public static void Seed(AppDbContext appDbContext)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "SeedData", "items.json");

            if (!File.Exists(path))
            {
                Console.WriteLine("ملف role.json غير موجود.");
                return;
            }

            var jsonText = File.ReadAllText(path);
            var seedItems = JsonConvert.DeserializeObject<List<WarehouseItem>>(jsonText);

            foreach (var item in seedItems)
            {
                var exists = appDbContext.WarehouseItems.Any(u => u.ItemName == item.ItemName);
                if (exists)
                    continue;
                item.CreatedBy = "Seed";
                item.CreatedOn = DateTime.UtcNow;

                appDbContext.WarehouseItems.Add(item);
            }
            appDbContext.SaveChanges();
        }
    }
}
