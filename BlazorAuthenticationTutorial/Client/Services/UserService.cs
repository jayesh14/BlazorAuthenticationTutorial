using BlazorAuthenticationTutorial.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorAuthenticationTutorial.Client.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public UserService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public List<SystemUser> SystemUsers { get; set; } = new List<SystemUser>();
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public async Task CreateUser(SystemUser systemUser)
        {
            var result = await _http.PostAsJsonAsync("api/user", systemUser);
            await SetUsers(result);
        }

        private async Task SetUsers(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<SystemUser>>();
            SystemUsers = response;
            _navigationManager.NavigateTo("users");
        }

        public async Task DeleteUser(int id)
        {
            var result = await _http.DeleteAsync($"api/user/{id}");
            await SetUsers(result);
        }

        public async Task GetRoles()
        {
            var result = await _http.GetFromJsonAsync<List<UserRole>>("api/user/roles");
            if (result != null)
                UserRoles = result;
        }

        public async Task<SystemUser> GetSingleUser(int id)
        {
            var result = await _http.GetFromJsonAsync<SystemUser>($"api/user/{id}");
            if (result != null)
                return result;
            throw new Exception("Hero not found!");
        }

        public async Task GetUsers()
        {
            var result = await _http.GetFromJsonAsync<List<SystemUser>>("api/user");
            if (result != null)
                SystemUsers = result;
        }

        public async Task UpdateUser(SystemUser systemUser)
        {
            var result = await _http.PutAsJsonAsync($"api/user/{systemUser.Id}", systemUser);
            await SetUsers(result);
        }
    }
}
