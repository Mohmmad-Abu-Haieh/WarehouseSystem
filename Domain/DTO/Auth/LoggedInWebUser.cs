

using Entity;

namespace UserModule.WorkContext;

public class LoggedInWebUserc
{
    public Guid Id { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public Guid? RoleId { get; set; }
}
