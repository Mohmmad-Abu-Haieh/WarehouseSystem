using DTO;
using Entity.Context;
using Entity;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Domain.DTO.Warehous;
using Entity.Entity;

namespace Domain.Service
{
    public class WarehouseService : IWarehouseService
    {
        private readonly AppDbContext _context;

        public WarehouseService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceOperationResult<WarehousForm>> CreateWarehous(WarehousForm form)
        {
            var result = new ServiceOperationResult<WarehousForm>();
            result.IsSuccessfull = true;
            result.Result = new WarehousForm();
            var userExists = await _context.Warehouses.AnyAsync(u => u.Name == form.Code);
            if (userExists)
            {
                result.IsSuccessfull = false;
                result.ErrorCodes.Add(Errors.ItemNotFound);
                return result;
            }
            var warehouse = new Warehouse
            {
                Id = Guid.NewGuid(),
                Name = form.Code,
                CreatedBy = "System",
                CreatedOn = DateTime.UtcNow,
                Active = true,

            };
            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
            form.Id = warehouse.Id;

            result.Result = form;
            return result;
        }
        public async Task<DataTable<WarehousForm>> GetWarehouseDataTable(WarehouseFilter param)
        {
            bool fiteredByKeyword = !string.IsNullOrEmpty(param.Keyword);
            var query = _context.Warehouses
                                 .Where(item =>
                                         (fiteredByKeyword ?
                                         item.Name.Contains(param.Keyword)
                                        
                                    : true)
                                    )
                                  .OrderByDescending(item => item.CreatedOn);
            int count = await query.CountAsync();
            var data = await query
                            .Skip(param.PageIndex * param.PageSize)
                            .Take(param.PageSize)
                            .Select(item => new WarehousForm
                            {
                                Id = item.Id,
                                Code = item.Name,
      
                            }).ToListAsync();
            var result = new DataTable<WarehousForm>
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
                Code = warehous.Name,
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
                Code = warehouse.Name,
            };

            return form;
        }
        public async Task<ServiceOperationResult<WarehousForm>> UpdateWarehouse(WarehousForm form)
        {
            var result = new ServiceOperationResult<WarehousForm>();
            result.IsSuccessfull = true;

            var warehouse = await _context.Warehouses.FindAsync(form.Id);
            if (warehouse == null)
            {
                result.IsSuccessfull = false;
                result.ErrorCodes.Add(Errors.ItemNotFound); 
                return result;
            }
            var warehouseTaken = await _context.Warehouses.AnyAsync(u => u.Name == form.Code && u.Id != form.Id);
            if (warehouseTaken)
            {
                result.IsSuccessfull = false;
                result.ErrorCodes.Add(Errors.ItemNotFound); 
                return result;
            }
            warehouse.Name = form.Code;
            warehouse.ModifiedBy = "System";
            warehouse.ModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            result.Result = form;
            return result;
        }
    }
}
