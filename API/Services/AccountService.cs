using API.DTOs.AccountRoles;
using API.DTOs.Accounts;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IMapper _mapper;
    private readonly IRoleRepository _roleRepository;

    public AccountService(IAccountRepository accountRepository, IMapper mapper,
                          IAccountRoleRepository accountRoleRepository, IRoleRepository roleRepository)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
        _accountRoleRepository = accountRoleRepository;
        _roleRepository = roleRepository;
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
}
