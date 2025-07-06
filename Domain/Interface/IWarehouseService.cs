using Domain.DTO.Warehous;
using DTO;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IWarehouseService
    {
        Task<ServiceOperationResult<WarehousForm>> CreateWarehous(WarehousForm form);
        Task<ServiceOperationResult<WarehousForm>> UpdateWarehouse(WarehousForm form);
        Task<WarehousForm> GetWarehouseByCode(string code);
        Task<WarehousForm> GetWarehouseById(Guid id);
        Task<DataTable<WarehouseList>> GetWarehouseDataTable(WarehouseFilter filter);
        Task<ServiceOperationResult<WarehousForm>> GetWarehouseDetails(Guid Id);
        Task<ServiceOperationResult> DeleteWarehouse(Guid id);
        Task<WarehouseFormData> GetWarehouseFormData();
    }
}
