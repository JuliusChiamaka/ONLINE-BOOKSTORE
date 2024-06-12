using AutoMapper;
using OnlineBookstore.Application.Interfaces.Repository;
using OnlineBookstore.Domain.Dtos.Request;
using OnlineBookstore.Domain.Dtos.Response;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Application.Interfaces.Service;

namespace OnlineBookstore.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository; 
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserResponse>>(users);
        }

        public async Task<UserResponse> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserResponse>(user);
        }

        public async Task AddUserAsync(AddUserRequest request)
        {
            var user = _mapper.Map<AppUser>(request);
            await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(int id, UpdateUserRequest request)
        {
            var user = _mapper.Map<AppUser>(request);
            user.Id = id;
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }

}
