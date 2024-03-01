using API.DTOs.AccountRoles;
using API.DTOs.Accounts;
using API.DTOs.Utilities;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using API.Utilities.Handlers;
using API.Utilities.Handlers.Interfaces;
using API.Utilities.ViewModels;
using AutoMapper;
using static System.Net.WebRequestMethods;

namespace API.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;
    private readonly IRoleRepository _roleRepository;
    private readonly IEmailHandler _emailHandler;

    public AccountService(IAccountRepository accountRepository, IMapper mapper,
                          IAccountRoleRepository accountRoleRepository, IRoleRepository roleRepository, IEmployeeRepository employeeRepository, IEmailHandler emailHandler)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
        _accountRoleRepository = accountRoleRepository;
        _roleRepository = roleRepository;
        _employeeRepository = employeeRepository;
        _emailHandler = emailHandler;
    }

    public async Task<int> AddAccountRoleAsync(AddAccountRoleRequestDto addAccountRoleRequestDto)
    {
        try
        {
            var account = await _accountRepository.GetByIdAsync(addAccountRoleRequestDto.AccountId);

            if (account == null) return 0; // Account not found

            var role = await _roleRepository.GetByIdAsync(addAccountRoleRequestDto.RoleId);

            if (role == null) return -1; // Account not found

            var accountRole = _mapper.Map<AccountRole>(addAccountRoleRequestDto);

            await _accountRoleRepository.CreateAsync(accountRole);

            return 1; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message,
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<int> RemoveRoleAsync(RemoveAccountRoleRequestDto removeAccountRoleRequestDto)
    {
        try
        {
            var accountRole =
                await _accountRoleRepository.GetDataByAccountIdAndRoleAsync(removeAccountRoleRequestDto.AccountId,
                                                                            removeAccountRoleRequestDto.RoleId);

            if (accountRole == null) return 0; // Account or Role not found

            await _accountRoleRepository.DeleteAsync(accountRole);

            return 1; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message,
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<IEnumerable<AccountResponseDto>?> GetAllAsync()
    {
        try
        {
            var data = await _accountRepository.GetAllAsync();

            var dataMap = _mapper.Map<IEnumerable<AccountResponseDto>>(data);

            return dataMap; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message,
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<AccountResponseDto?> GetByIdAsync(Guid id)
    {
        try
        {
            var account = await _accountRepository.GetByIdAsync(id);

            if (account == null) return null; // not found

            var dataMap = _mapper.Map<AccountResponseDto>(account);

            return dataMap; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message,
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<int> CreateAsync(AccountRequestDto accountRequestDto)
    {
        try
        {
            var account = _mapper.Map<Account>(accountRequestDto);

            await _accountRepository.CreateAsync(account);

            return 1; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message,
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<int> UpdateAsync(Guid id, AccountRequestDto accountRequestDto)
    {
        try
        {
            var data = await _accountRepository.GetByIdAsync(id);

            if (data == null) return 0; // not found

            var account = _mapper.Map<Account>(accountRequestDto);

            account.Id = id;
            await _accountRepository.UpdateAsync(account);

            return 1; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message,
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        try
        {
            var data = await _accountRepository.GetByIdAsync(id);

            if (data == null) return 0; // not found

            await _accountRepository.DeleteAsync(data);

            return 1; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message,
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<int> RegisterAsync(RegisterDto registerDto)
    {
        await using var transaction = await _accountRepository.BeginTransactionAsync();
        if (registerDto.Password != registerDto.ConfirmPassword)
        {
            return -1; //password not match
        }
        var employeeMap = _mapper.Map<Employee>(registerDto);
        var employee = await _employeeRepository.CreateAsync(employeeMap);

        var accountVM = new AccountVM(employee, registerDto);

        var account = _mapper.Map<Account>(accountVM);

        await _accountRepository.CreateAsync(account);

        var role = await _roleRepository.GetIdByNameAsync("Employee");
        if(role == null)
        {
            return 0; //role not found
        }
        var AccountRoleVM = new AccountRoleVM(account, role);
        var accountRole = _mapper.Map<AccountRole>(AccountRoleVM);
        await _accountRoleRepository.CreateAsync(accountRole);
        await transaction.CommitAsync();

        return 1; //success

    }

    public async Task<int> LoginAsync(LoginDto loginDto)
    {
        var employee = await _employeeRepository.GetByEmailAsync(loginDto.Email);
        if(employee  == null) return 0; //email not match
        
        var account = await _accountRepository.GetByIdAsync(employee.Id);
        if (account == null) return 0; //akun not found
        var isValidPassword = BCryptHandler.VerifyPassword(loginDto.Password, account.Password);
        if (!isValidPassword) return 0; //pasword not match

        return 1;
    }

    public async Task<int> ChangePassword(ChangePasswordDto changePasswordDto)
    {
        return 1;
    }

    public async Task<int> ForgotPasswordAsync(ForgotPasswodDto forgotPasswodDto)
    {
        var employee = await _employeeRepository.GetByEmailAsync(forgotPasswodDto.Email);
        if (employee == null) return 0; //email not match

        var account = await _accountRepository.GetByIdAsync(employee.Id);
        if (account == null) return 0; //akun not found

        account.Otp = new Random().Next(100000, 1000000);
        account.Expired = DateTime.Now.AddMinutes(5);
        account.IsUsed = false;
        await _accountRepository.UpdateAsync(account);
        var message = $"Your OTP is {account.Otp}";

        var emailMap = new EmailDto(forgotPasswodDto.Email, "[Reset Password] - MBKM 6", message);
        await _emailHandler.SendEmailAsync(emailMap);
        return 1;
    }
}
