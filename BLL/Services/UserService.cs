using AutoMapper;
using BLL.Models.User;
using BLL.Services.Interfaces;
using DAL.Data;
using DAL.Exceptions;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace BLL.Services
{
    public class UserService : IUserService
    {   
        private readonly AppDbContext _dbContext;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(
            AppDbContext dbContext, 
            IUserRepository userRepository,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserModel>>(users);
        }
        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException($"Invalid id value: {id}");
            }

            var user = await _userRepository.GetUserByIdAsync(id)
                ?? throw new NotFoundException($"User with id {id} was not found");

            return _mapper.Map<UserModel>(user);
        }
        public async Task<UserModel> GetUserByNameAsync(string name)
        {
            var user = await _userRepository.GetUserByNameAsync(name)
                ?? throw new NotFoundException($"User with Nickname {name} was not found");

            return _mapper.Map<UserModel>(user);
        }
        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email)
                ?? throw new NotFoundException($"User with Email {email} was not found");

            return _mapper.Map<UserModel>(user);
        }
        public async Task<UserModel> CreateUserAsync(CreateUserModel createUserModel)
        {
            var user = _mapper.Map<User>(createUserModel);

            var checkName = await _userRepository.GetUserByNameAsync(createUserModel.Nickname);

            if (checkName != null)
            {
                throw new NotFoundException($"User with nickname \"{createUserModel.Nickname}\" already exists!");
            }

            var checkEmail = await _userRepository.GetUserByEmailAsync(createUserModel.Email);

            if (checkEmail != null)
            {
                throw new NotFoundException($"User with email \"{createUserModel.Email}\" already exists!");
            }

            await _userRepository.CreateAsync(user);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<UserModel>(user);
        }
        public async Task UpdateAsync(int id, UpdateUserModel updateUserModel)
        {
            if(id <= 0)
            {
                throw new BadRequestException($"Invalid id value: {id}");
            }

            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                throw new NotFoundException($"User with id {id} was not found!");
            }

            var checkEmail = await _userRepository.GetUserByEmailAsync(updateUserModel.Email);

            if (checkEmail.Id != id)
            {
                throw new NotFoundException($"User with email \"{updateUserModel.Email}\" already exists! Can't be changed!");
            }

            if (!string.IsNullOrWhiteSpace(updateUserModel.Email))
            {
                user.Email = updateUserModel.Email;
            }
            if (!string.IsNullOrWhiteSpace(updateUserModel.Password))
            {
                user.Password = updateUserModel.Password;
            }

            _userRepository.Update(user);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            if(id <= 0)
            {
                throw new BadRequestException($"Invalid id value: {id}");
            }

            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                throw new NotFoundException($"User with id {id} was not found!");
            }

            _userRepository.Delete(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
