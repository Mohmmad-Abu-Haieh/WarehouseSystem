using DTO;
using Entity;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IUserService
    {
        Task<ServiceOperationResult<UserForm>> CreateAccount(UserForm form ,string CurrentId);
        Task<ServiceOperationResult<UserForm>> UpdateAccount(UserForm form);
        Task<UserForm> GetUserByUsername(string username);
        Task<User> GetUserById(Guid id);
        Task<DataTable<UserForm>> GetUsersDataTable(UserFilter filter);
        Task<ServiceOperationResult> DeleteUser(Guid id);
    }
}

