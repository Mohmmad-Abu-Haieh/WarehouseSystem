using Domain.Interface;
using Domain.Repositories;
using DTO;
using Entity;
using Entity.Context;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Domain.Service;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IRepository<User> _repository;

    public UserService(AppDbContext context, IRepository<User> repository)
    {
        _context = context;
        _repository = repository;
    }
    public async Task<ServiceOperationResult<UserForm>> CreateAccount(UserForm form,string CurrentId)
    {
        var result = new ServiceOperationResult<UserForm>();
        result.IsSuccessfull = true;
        result.Result = new UserForm();
        var userExists = await _context.Users.AnyAsync(u => u.Email == form.Email);
        if (userExists)
        {
            result.IsSuccessfull = false;
            result.ErrorCodes.Add(Errors.ItemNotFound);
            return result;
        }
        byte[] passwordHash = null;
        byte[] passwordSalt = null;
        CreatePasswordHash(form.Password, out passwordHash, out passwordSalt);
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = form.FullName,
            Email = form.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Mobile = form.Mobile,
            RoleId = form.RoleId,
            CreatedBy = CurrentId,
            CreatedOn = DateTime.UtcNow
        };

        
        await _repository.AddAsync(user);
        await _repository.SaveAsync();

        form.Id = user.Id;
        result.Result = form;
        return result;
    }
    public async Task<ServiceOperationResult<UserForm>> GetUserDetails(Guid Id)
    {
        var result = new ServiceOperationResult<UserForm>();
        result.IsSuccessfull = true;
        result.Result = new UserForm();
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == Id);

        if (user == null)
        {
            result.IsSuccessfull = false;
            result.ErrorCodes.Add(Errors.ItemNotFound);
            return result;
        }
        var form = new UserForm
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Mobile = user.Mobile,
            RoleId = user.RoleId,
        };
        result.Result = form;
        return result;
    }
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
    public async Task<ServiceOperationResult<UserForm>> UpdateAccount(UserForm form)
    {
        var result = new ServiceOperationResult<UserForm>();
        result.IsSuccessfull = true;
        var user = await _repository.GetByIdAsync(form.Id.Value);

        if (user == null)
        {
            result.IsSuccessfull = false;
            result.ErrorCodes.Add(Errors.ItemNotFound);
            return result;
        }
        var usernameTaken = await _context.Users.AnyAsync(u => u.Email == form.Email && u.Id != form.Id);
        if (usernameTaken)
        {
            result.IsSuccessfull = false;
            result.ErrorCodes.Add(Errors.ItemNotFound);
            return result;
        }
        user.FullName = form.FullName;
        user.Email = form.Email;
        user.Mobile = form.Mobile;
        user.RoleId = form.RoleId;
        user.ModifiedBy = "System";
        user.ModifiedOn = DateTime.UtcNow;
        await _repository.SaveAsync();
        result.Result = form;
        return result;
    }
    public async Task<DataTable<UserList>> GetUsersDataTable(UserFilter param)
    {
        bool fiteredByKeyword = !string.IsNullOrEmpty(param.Keyword);
        var query = _context.Users
                             .Where(item => item.Active
                                    &&
                                     (fiteredByKeyword ?
                                       item.FullName.Contains(param.Keyword)
                                    || item.Email.Contains(param.Keyword)
                                    || item.Mobile.Contains(param.Keyword)
                                : true)
                                )
                              .OrderByDescending(item => item.CreatedOn);

        int count = await query.CountAsync();
        var data = await query
                        .Skip(param.PageIndex * param.PageSize)
                        .Take(param.PageSize)
                        .Select(item => new UserList
                        {
                            Id = item.Id,
                            Email = item.Email,
                            Mobile = item.Mobile,
                            FullName = item.FullName,
                            RoleName = item.RoleId.HasValue ? item.Role.Name : null
                        }).ToListAsync();
        var result = new DataTable<UserList>
        {
            Data = data,
            Count = count
        };
        return result;
    }
    public async Task<ServiceOperationResult> DeleteUser(Guid id)
    {
        var result = new ServiceOperationResult();
        result.IsSuccessfull = true;
        var user = await _repository.GetByIdAsync(id);

        if (user == null)
        {
            result.IsSuccessfull = false;
            result.ErrorCodes.Add(Errors.ItemNotFound);
        }
        else
        {
            user.Active = false;
            await _repository.SaveAsync();
        }
        return result;
    }
    public async Task<User> GetUserById(Guid id)
    {
        var user = await _context.Users
               .Include(u => u.Role)
               .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null) return null;

        return user;
    }
    public async Task<UserForm> GetUserByUsername(string username)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == username);
        if (user == null)
            return null;

        var form = new UserForm
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Mobile = user.Mobile,
            RoleId = user.RoleId,
        };
        return form;
    }
    public async Task<UserFormData> GetUsersFormData()
    {
        UserFormData result = new()
        {
            Roles = await _context.Roles
                .Select(item => new Hook<Guid, string>
                {
                    Id = item.Id,
                    Text = item.Name
                }).Distinct().ToListAsync(),
        };
        return result;
    }
}
