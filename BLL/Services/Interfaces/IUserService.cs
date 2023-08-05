using BLL.Models.User;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetUsersAsync();
        Task<UserModel> GetUserByIdAsync(int id);
        Task<UserModel> GetUserByNameAsync(string name);
        Task<UserModel> GetUserByEmailAsync(string Email);
        Task<UserModel> CreateUserAsync(CreateUserModel user);
        Task UpdateAsync(int id, UpdateUserModel user);
        Task DeleteAsync(int id);

    }
}
