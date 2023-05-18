using BlazorAuthenticationTutorial.Shared;

namespace BlazorAuthenticationTutorial.Client.Services
{
    public interface IUserService
    {
        List<SystemUser> SystemUsers { get; set; }
        List<UserRole> UserRoles { get; set; }
        Task GetRoles();
        Task GetUsers();
        Task<SystemUser> GetSingleUser(int id);
        Task CreateUser(SystemUser systemUser);
        Task UpdateUser(SystemUser systemUser);
        Task DeleteUser(int id);
    }
}
