using Domain.DTO.Dashboard;

namespace Domain.Interface
{
    public interface IDashboardService
    {
        Task<List<WarehouseStatusDto>> GetWarehouseStatusesAsync();
        Task<List<ItemQuantityDto>> GetTopItemsAsync(bool high);
    }
}
