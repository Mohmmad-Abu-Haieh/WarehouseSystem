using Domain.DTO.Dashboard;
using Domain.Interface;
using Entity.Context;
using Microsoft.EntityFrameworkCore;

namespace Domain.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<WarehouseStatusDto>> GetWarehouseStatusesAsync()
        {
            return await _context.Warehouses
                .Select(w => new WarehouseStatusDto
                {
                    WarehouseName = w.Name,
                    InventoryCount = w.Items.Count()
                }).ToListAsync();
        }

        public async Task<List<ItemQuantityDto>> GetTopItemsAsync(bool high)
        {
            var query = _context.WarehouseItems
                .OrderByDescending(i => i.Qty);

            if (!high)
                query = _context.WarehouseItems.OrderBy(i => i.Qty);

            return await query.Take(10)
                .Select(i => new ItemQuantityDto
                {
                    ItemName = i.ItemName,
                    Quantity = i.Qty
                }).ToListAsync();
        }
    }
}
