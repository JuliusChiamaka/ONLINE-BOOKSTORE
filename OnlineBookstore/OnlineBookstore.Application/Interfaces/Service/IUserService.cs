using OnlineBookstore.Domain.Dtos.Request;
using OnlineBookstore.Domain.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Application.Interfaces.Service
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponse>> GetAllUsersAsync();
        Task<UserResponse> GetUserByIdAsync(int id);
        Task AddUserAsync(AddUserRequest request);
        Task UpdateUserAsync(int id, UpdateUserRequest request);
        Task DeleteUserAsync(int id);
    }
}
