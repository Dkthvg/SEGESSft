using Microsoft.AspNetCore.Identity;
using SEGES.Backend.Repositories.Interfaces;
using SEGES.Backend.UnitsOfWork.Interfaces;
using SEGES.Shared.DTOs;
using SEGES.Shared.Entities;
using SEGES.Shared.Responses;
using System.Threading.Tasks;
using System;
using SEGES.Backend.Repositories.Implementations;

namespace SEGES.Backend.UnitsOfWork.Implementations
{
    public class UsersUnitOfWork : IUsersUnitOfWork
    {
        private readonly IUsersRepository _usersRepository;

        public UsersUnitOfWork(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<IdentityResult> AddUserAsync(UserApp user, string password) => await _usersRepository.AddUserAsync(user, password);

        public async Task AddUserToRoleAsync(UserApp user, string roleName) => await _usersRepository.AddUserToRoleAsync(user, roleName);

        public async Task CheckRoleAsync(string roleName) => await _usersRepository.CheckRoleAsync(roleName);

        public async Task<UserApp> GetUserAsync(string email) => await _usersRepository.GetUserAsync(email);

        
        public async Task<bool> IsUserInRoleAsync(UserApp user, string roleName) => await _usersRepository.IsUserInRoleAsync(user, roleName);

        public async Task<SignInResult> LoginAsync(LoginDTO model) => await _usersRepository.LoginAsync(model);

        public async Task LogoutAsync() => await _usersRepository.LogoutAsync();

        public async Task<UserApp> GetUserAsync(Guid userId) => await _usersRepository.GetUserAsync(userId);

        public async Task<IdentityResult> ChangePasswordAsync(UserApp user, string currentPassword, string newPassword) => await _usersRepository.ChangePasswordAsync(user, currentPassword, newPassword);

        public async Task<IdentityResult> UpdateUserAsync(UserApp user) => await _usersRepository.UpdateUserAsync(user);


        public async Task<string> GenerateEmailConfirmationTokenAsync(UserApp user) => await _usersRepository.GenerateEmailConfirmationTokenAsync(user);

        public async Task<IdentityResult> ConfirmEmailAsync(UserApp user, string token) => await _usersRepository.ConfirmEmailAsync(user, token);

        public async Task<string> GeneratePasswordResetTokenAsync(UserApp user) => await _usersRepository.GeneratePasswordResetTokenAsync(user);

        public async Task<IdentityResult> ResetPasswordAsync(UserApp user, string token, string password) => await _usersRepository.ResetPasswordAsync(user, token, password);

        public async Task<IEnumerable<UserApp>> GetComboAsync() => await _usersRepository.GetComboAsync();
    }
}