using AutoMapper;
using Moq;
using OnlineBookstore.Application.Interfaces.Repository;
using OnlineBookstore.Application.Services;
using OnlineBookstore.Domain.Dtos.Request;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Infrastructure.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Test
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = config.CreateMapper();

            _userService = new UserService(_mockUserRepository.Object, _mapper);
        }

        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnAllUsers()
        {
            // Arrange
            var users = TestData.GetUsers();
            _mockUserRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _userService.GetAllUsersAsync();

            // Assert
            Assert.Equal(users.Count, result.Count());
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var user = TestData.GetUsers().First();
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(user.Id)).ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserByIdAsync(user.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = 999;
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((AppUser)null);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddUserAsync_ShouldAddUser()
        {
            // Arrange
            var userRequest = new AddUserRequest
            {
                Username = "NewUser",
                Email = "newuser@example.com",
                PasswordHash = "hashedpassword",
                FirstName = "New",
                LastName = "User"
            };
            var user = _mapper.Map<AppUser>(userRequest);

            _mockUserRepository.Setup(repo => repo.AddAsync(It.IsAny<AppUser>())).Returns(Task.CompletedTask);

            // Act
            await _userService.AddUserAsync(userRequest);

            // Assert
            _mockUserRepository.Verify(repo => repo.AddAsync(It.Is<AppUser>(u => u.Username == user.Username && u.Email == user.Email)), Times.Once);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldUpdateUser()
        {
            // Arrange
            var userRequest = new UpdateUserRequest
            {
                Username = "UpdatedUser",
                Email = "updateduser@example.com",
                PasswordHash = "updatedpassword",
                FirstName = "Updated",
                LastName = "User"
            };
            var userId = 1;
            var user = _mapper.Map<AppUser>(userRequest);
            user.Id = userId;

            _mockUserRepository.Setup(repo => repo.UpdateAsync(It.IsAny<AppUser>())).Returns(Task.CompletedTask);

            // Act
            await _userService.UpdateUserAsync(userId, userRequest);

            // Assert
            _mockUserRepository.Verify(repo => repo.UpdateAsync(It.Is<AppUser>(u => u.Id == userId && u.Username == user.Username && u.Email == user.Email)), Times.Once);
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldDeleteUser()
        {
            // Arrange
            var userId = 1;

            _mockUserRepository.Setup(repo => repo.DeleteAsync(userId)).Returns(Task.CompletedTask);

            // Act
            await _userService.DeleteUserAsync(userId);

            // Assert
            _mockUserRepository.Verify(repo => repo.DeleteAsync(userId), Times.Once);
        }
    }

}
