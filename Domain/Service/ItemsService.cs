using Domain.DTO.Item;
using Domain.DTO.Warehous;
using Domain.Interface;
using Domain.Repositories;
using Entity.Context;
using Entity.Entity;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service
{
    public class ItemsService : IItemService
    {
        private readonly AppDbContext _context;
        private readonly IRepository<WarehouseItem> _repository;

        public ItemsService(AppDbContext context, IRepository<WarehouseItem> repository)
        {
            _context = context;
            _repository = repository;
        }
        public async Task<DataTable<ItemList>> GetItemsDataTable(ItemFilter param)
        {
            bool fiteredByKeyword = !string.IsNullOrEmpty(param.Keyword);
            var query = _context.WarehouseItems
                                 .Where(item => item.Active == true
                                        &&
                                         (fiteredByKeyword ?
                                         item.ItemName.Contains(param.Keyword)

                                    : true)
                                    )
                                  .OrderByDescending(item => item.CreatedOn);
            int count = await query.CountAsync();
            var data = await query
                            .Skip(param.PageIndex * param.PageSize)
                            .Take(param.PageSize)
                            .Select(item => new ItemList
                            {
                                Id = item.Id,
                                ItemName = item.ItemName,
                                CostPrice = item.CostPrice,
                                MsrpPrice = item.MsrpPrice,
                                Qty = item.Qty,
                                SkuCode = item.SkuCode,
                                Warehouse = item.Warehouse.Name,

                            }).ToListAsync();
            var result = new DataTable<ItemList>
            {
                Data = data,
                Count = count
            };
            return result;
        }
        public async Task<ServiceOperationResult> CreateItems(ItemForm form)
        {
            try
            {
                var result = new ServiceOperationResult();
                result.IsSuccessfull = true;
                var itemExists = await _context.WarehouseItems.AnyAsync(u => u.ItemName == form.ItemName);
                if (itemExists)
                {
                    result.IsSuccessfull = false;
                    result.ErrorCodes.Add(Errors.ItemNotFound);
                    return result;
                }
                var item = new WarehouseItem
                {
                    Id = Guid.NewGuid(),
                    ItemName = form.ItemName,
                    SkuCode = form.SkuCode,
                    MsrpPrice = form.MsrpPrice,
                    Qty = form.Qty,
                    CostPrice = form.CostPrice,
                    WarehouseId = form.WarehouseId,
                    CreatedBy = "System",
                    CreatedOn = DateTime.UtcNow,
                    Active = true,
                };
                _context.WarehouseItems.Add(item);
                await _context.SaveChangesAsync();
                form.Id = item.Id;
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                throw;
            }
        }
        public async Task<ItemFormData> GetItemsFormData()
        {
            ItemFormData result = new()
            {
                Warehouses = await _context.Warehouses
                    .Select(item => new Hook<Guid, string>
                    {
                        Id = item.Id,
                        Text = item.Name
                    }).Distinct().ToListAsync(),
            };
            return result;
        }

        public async Task<ServiceOperationResult<ItemForm>> GetItemDetails(Guid Id)
        {
            var result = new ServiceOperationResult<ItemForm>();
            result.IsSuccessfull = true;
            result.Result = new ItemForm();
            var item = await _context.WarehouseItems
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == Id);

            if (item == null)
            {
                result.IsSuccessfull = false;
                result.ErrorCodes.Add(Errors.ItemNotFound);
                return result;
            }
            var form = new ItemForm
            {
                Id = item.Id,
                ItemName = item.ItemName,
                SkuCode = item.SkuCode,
                CostPrice = item.CostPrice,
                MsrpPrice = item.MsrpPrice,
                Qty = item.Qty,
                WarehouseId = item.WarehouseId,
            };
            result.Result = form;
            return result;
        }
        public async Task<ServiceOperationResult> UpdateItem(ItemForm form)
        {
            var result = new ServiceOperationResult();
            result.IsSuccessfull = true;

            var item = await _context.WarehouseItems.FindAsync(form.Id);
            if (item == null)
            {
                result.IsSuccessfull = false;
                result.ErrorCodes.Add(Errors.ItemNotFound);
                return result;
            }
            var itemTaken = await _context.WarehouseItems.AnyAsync(u => u.ItemName == form.ItemName && u.Id != form.Id);
            if (itemTaken)
            {
                result.IsSuccessfull = false;
                result.ErrorCodes.Add(Errors.NameFieldAlreadyExist);
                return result;
            }
            item.ItemName = form.ItemName;
            item.SkuCode = form.SkuCode;
            item.CostPrice = form.CostPrice;
            item.MsrpPrice = form.MsrpPrice;
            item.Qty = form.Qty;
            item.WarehouseId = item.WarehouseId;
            item.ModifiedBy = "System";
            item.ModifiedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<ServiceOperationResult> DeleteItem(Guid id)
        {
            var result = new ServiceOperationResult();
            result.IsSuccessfull = true;
            var item = await _repository.GetByIdAsync(id);

            if (item == null)
            {
                result.IsSuccessfull = false;
                result.ErrorCodes.Add(Errors.ItemNotFound);
            }
            else
            {
                item.Active = false;
                await _repository.SaveAsync();
            }
            return result;
        }
    }
}
