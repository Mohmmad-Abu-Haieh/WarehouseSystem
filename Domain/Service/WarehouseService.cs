using Domain.DTO.Item;
using Domain.DTO.Warehous;
using Domain.Interface;
using Domain.Repositories;
using Entity.Context;
using Entity.Entity;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Domain.Service
{
    public class WarehouseService : IWarehouseService
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Warehouse> _repository;

        public WarehouseService(AppDbContext context, IRepository<Warehouse> repository)
        {
            _context = context;
            _repository = repository;

        }
        public async Task<ServiceOperationResult<WarehousForm>> CreateWarehous(WarehousForm form)
        {
            try
            {
                var result = new ServiceOperationResult<WarehousForm>();
                result.IsSuccessfull = true;
                result.Result = new WarehousForm();
                var warehousExists = await _context.Warehouses.AnyAsync(u => u.Name == form.Name);
                if (warehousExists)
                {
                    result.IsSuccessfull = false;
                    result.ErrorCodes.Add(Errors.ItemNotFound);
                    return result;
                }
                var warehouse = new Warehouse
                {
                    Id = Guid.NewGuid(),
                    Name = form.Name,
                    Address = form.Address,
                    City = form.City,
                    CountryId = form.CountryId,
                    CreatedBy = "System",
                    CreatedOn = DateTime.UtcNow,
                    Active = true,
                };
                await _repository.AddAsync(warehouse);
                await _repository.SaveAsync();
                form.Id = warehouse.Id;
                result.Result = form;
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message); // أو سجلها بالـ logger
                throw;
            }
           
        }

        public async Task<ServiceOperationResult<WarehousForm>> GetWarehouseDetails(Guid Id)
        {
            var result = new ServiceOperationResult<WarehousForm>();
            result.IsSuccessfull = true;
            result.Result = new WarehousForm();
            var warehouse = await _context.Warehouses
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == Id);

            if (warehouse == null)
            {
                result.IsSuccessfull = false;
                result.ErrorCodes.Add(Errors.ItemNotFound);
                return result;
            }
            var form = new WarehousForm
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                Address = warehouse.Address,
                City = warehouse.City,
                CountryId = warehouse.CountryId,
            };
            result.Result = form;
            return result;
        }
        public async Task<DataTable<WarehouseList>> GetWarehouseDataTable(WarehouseFilter param)
        {
            bool fiteredByKeyword = !string.IsNullOrEmpty(param.Keyword);
            var query = _context.Warehouses
                                 .Where(item => item.Active == true 
                                        &&
                                         (fiteredByKeyword ?
                                         item.Name.Contains(param.Keyword)
                                        
                                    : true)
                                    )
                                  .OrderByDescending(item => item.CreatedOn);
            int count = await query.CountAsync();
            var data = await query
                            .Skip(param.PageIndex * param.PageSize)
                            .Take(param.PageSize)
                            .Select(item => new WarehouseList
                            {
                                Id = item.Id,
                                Name = item.Name,
                                Address = item.Address,
                                City = item.City,
                                Country = item.Country.Name
      
                            }).ToListAsync();
            var result = new DataTable<WarehouseList>
            {
                Data = data,
                Count = count
            };
            return result;
        }
        public async Task<DataTable<ItemList>> GetWarehouseItemsDataTable(WarehouseFilter param)
        {
            bool fiteredByKeyword = !string.IsNullOrEmpty(param.Keyword);
            var query = _context.WarehouseItems
                                 .Where(item => item.Active == true && item.WarehouseId == param.Id.Value
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
        public async Task<WarehousForm> GetWarehouseById(Guid id)
        {
            var warehous = await _context.Warehouses
                   .FirstOrDefaultAsync(u => u.Id == id);

            if (warehous == null) return null;

            return new WarehousForm
            {
                Id = warehous.Id,
                Name = warehous.Name,
            };
        }
        public async Task<WarehousForm> GetWarehouseByCode(string code)
        {
            var warehouse = await _context.Warehouses
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Name == code);

            if (warehouse == null)
                return null;

            var form = new WarehousForm
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
            };

            return form;
        }
        public async Task<ServiceOperationResult<WarehousForm>> UpdateWarehouse(WarehousForm form)
        {
            var result = new ServiceOperationResult<WarehousForm>();
            result.IsSuccessfull = true;
            var warehouse = await _repository.GetByIdAsync(form.Id.Value);
            if (warehouse == null)
            {
                result.IsSuccessfull = false;
                result.ErrorCodes.Add(Errors.ItemNotFound); 
                return result;
            }
            var warehouseTaken = await _context.Warehouses.AnyAsync(u => u.Name == form.Name && u.Id != form.Id);
            if (warehouseTaken)
            {
                result.IsSuccessfull = false;
                result.ErrorCodes.Add(Errors.ItemNotFound); 
                return result;
            }
            warehouse.Name = form.Name;
            warehouse.Address = form.Address;
            warehouse.City = form.City;
            warehouse.CountryId = form.CountryId;
            warehouse.ModifiedBy = "System";
            warehouse.ModifiedOn = DateTime.UtcNow;

            await _repository.SaveAsync();

            result.Result = form;
            return result;
        }

        public async Task<WarehouseFormData> GetWarehouseFormData()
        {
            WarehouseFormData result = new()
            {
                Countries = await _context.Countries
                    .Select(item => new Hook<Guid, string>
                    {
                        Id = item.Id,
                        Text = item.Name
                    }).Distinct().ToListAsync(),
            };
            return result;
        }

        public async Task<ServiceOperationResult> DeleteWarehouse(Guid id)
        {
            var result = new ServiceOperationResult();
            result.IsSuccessfull = true;
            var warehouse = await _repository.GetByIdAsync(id);

            if (warehouse == null)
            {
                result.IsSuccessfull = false;
                result.ErrorCodes.Add(Errors.ItemNotFound);
            }
            else
            {
                warehouse.Active = false;
                await _repository.SaveAsync();
            }
            return result;
        }
    }
}
