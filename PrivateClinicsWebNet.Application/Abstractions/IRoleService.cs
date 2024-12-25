namespace PrivateClinicsWebNet.Application.Abstractions
{
    public interface IRoleService
    {
        Task CreateRoleAsync(string roleName);
    }
}