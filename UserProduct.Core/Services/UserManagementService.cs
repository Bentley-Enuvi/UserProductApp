using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserProduct.Core.Abstractions;
using UserProduct.Core.DTOs;
using UserProduct.Domain.Entities;

namespace UserProduct.Core.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IRepository _repository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementService(
            IRepository repository,
            IMapper mapper,
            IProductService productService,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _repository = repository;
            _mapper = mapper;
            _productService = productService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = (await _repository.GetAllAsync2<User>())
                .Where(user => user.DeletedAt == null);

            var userDtoList = new List<UserDto>();

            foreach (var user in users)
            {
                var userRole = await _userManager.GetRolesAsync(user);

                var userDto = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                };

                userDtoList.Add(userDto);
            }

            return userDtoList;
        }

        public async Task<UserDto> GetUserById(string userId)
        {
            var existingUser = await _repository.GetByIdAsync<User>(userId);

            if (existingUser == null || existingUser.DeletedAt != null)
                return null;

            var userDto = _mapper.Map<UserDto>(existingUser);

            return userDto;
        }

        public async Task<object> DeleteUser(string id)
        {
            var user = await _repository.GetByIdAsync<User>(id);

            if (user == null || !(user.DeletedAt == null))
                return false;

            user.DeletedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(user);
            return new { deletedAt = user.DeletedAt };
        }

        public async Task<UpdateUserRequestDto> UpdateUser(string id, UpdateUserRequestDto appUser)
        {
            var user = await _repository.GetByIdAsync<User>(id);

            if (user == null || user.DeletedAt != null)
                throw new Exception("User not found");

            user.FirstName = appUser.FirstName;
            user.LastName = appUser.LastName;
            user.Email = appUser.Email;
            user.PhoneNumber = appUser.PhoneNumber;

            await _repository.UpdateAsync<User>(user);

            var updatedUser = new UpdateUserRequestDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
            };

            return updatedUser;
        }

        public async Task<IEnumerable<GetAllProductDto>> GetProductsPurchasedByUser(string userId)
        {
            // Retrieve all Product entries for the given user
            var productPurchasedEntries = (await _repository.GetAllAsync<Product>())
                .Where(a => a.UserId == userId)
                .Include(a => a.ProductName)
                .Select(a => new GetAllProductDto
                {
                    Id = a.ProductName,
                    
                })
                .OrderByDescending(a => a.CreatedAt)
                .Distinct();

            return productPurchasedEntries;
        }

    }
}
