using Microsoft.AspNetCore.Identity;
using SEGES.Shared.DTOs;
using SEGES.Shared.Entities;
using SEGES.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace SEGES.Backend.UnitsOfWork.Interfaces
{
    public interface IUsersUnitOfWork
    {
        Task<string> GeneratePasswordResetTokenAsync(UserApp user);
        Task<IdentityResult> ResetPasswordAsync(UserApp user, string token, string password);
        Task<string> GenerateEmailConfirmationTokenAsync(UserApp user);

        Task<IdentityResult> ConfirmEmailAsync(UserApp user, string token);
        Task<UserApp> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(UserApp user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(UserApp user, string roleName);

        Task<bool> IsUserInRoleAsync(UserApp user, string roleName);

        Task<SignInResult> LoginAsync(LoginDTO model);

        Task LogoutAsync();

        Task<UserApp> GetUserAsync(Guid userId);

        Task<IdentityResult> ChangePasswordAsync(UserApp user, string currentPassword, string newPassword);

        Task<IdentityResult> UpdateUserAsync(UserApp user);

        Task<IEnumerable<UserApp>> GetComboAsync();


    }
}