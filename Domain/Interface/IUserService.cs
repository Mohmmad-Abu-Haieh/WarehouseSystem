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
        Task<DataTable<UserList>> GetUsersDataTable(UserFilter filter);
        Task<ServiceOperationResult> DeleteUser(Guid id);
        Task<ServiceOperationResult<UserForm>> GetUserDetails(Guid Id);
        Task<UserFormData> GetUsersFormData();
        Task<ServiceOperationResult> ChangePassword(ChangePassword model);
    }
}

