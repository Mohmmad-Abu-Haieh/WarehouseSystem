using Domain.DTO.Item;
using Domain.DTO.Warehous;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IItemService
    {
        Task<DataTable<ItemList>> GetItemsDataTable(ItemFilter param);
        Task<ServiceOperationResult> CreateItems(ItemForm form);
        Task<ServiceOperationResult<ItemForm>> GetItemDetails(Guid Id);
        Task<ItemFormData> GetItemsFormData();
        Task<ServiceOperationResult> UpdateItem(ItemForm form);
        Task<ServiceOperationResult> DeleteItem(Guid id);
    }
}
