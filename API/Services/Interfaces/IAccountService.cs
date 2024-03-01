using API.DTOs.AccountRoles;
using API.DTOs.Accounts;

namespace API.Services.Interfaces;

public interface IAccountService
{
    Task<int> AddAccountRoleAsync(AddAccountRoleRequestDto addAccountRoleRequestDto);
    Task<int> RemoveRoleAsync(RemoveAccountRoleRequestDto removeAccountRoleRequestDto);
    Task<IEnumerable<AccountResponseDto>?> GetAllAsync();
    Task<AccountResponseDto?> GetByIdAsync(Guid id);
    Task<int> CreateAsync(AccountRequestDto accountRequestDto);
    Task<int> UpdateAsync(Guid id, AccountRequestDto accountRequestDto);
    Task<int> DeleteAsync(Guid id);
    Task<int> RegisterAsync(RegisterDto registerDto);
    Task<int> LoginAsync(LoginDto loginDto);
    Task<int> ChangePassword(ChangePasswordDto changePasswordDto);
    Task<int> ForgotPasswordAsync(ForgotPasswodDto forgotPasswodDto);
}
