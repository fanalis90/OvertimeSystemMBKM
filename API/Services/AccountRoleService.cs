using API.DTOs.AccountRoles;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services;

public class AccountRoleService : IAccountRoleService
{
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IMapper _mapper;

    public AccountRoleService(IAccountRoleRepository accountRoleRepository, IMapper mapper)
    {
        _accountRoleRepository = accountRoleRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AccountRoleResponseDto>?> GetAllAsync()
    {
        try
        {
            var data = await _accountRoleRepository.GetAllAsync();

            var dataMap = _mapper.Map<IEnumerable<AccountRoleResponseDto>>(data);

            return dataMap; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message,
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<AccountRoleResponseDto?> GetByIdAsync(Guid id)
    {
        try
        {
            var data = await _accountRoleRepository.GetByIdAsync(id);

            var dataMap = _mapper.Map<AccountRoleResponseDto>(data);

            return dataMap; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message,
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<int> CreateAsync(AddAccountRoleRequestDto addAccountRoleRequestDto)
    {
        try
        {
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

    public async Task<int> UpdateAsync(Guid id, AddAccountRoleRequestDto addAccountRoleRequestDto)
    {
        try
        {
            var data = await _accountRoleRepository.GetByIdAsync(id);

            if (data == null)
            {
                return 0; // not found
            }

            var accountRole = _mapper.Map<AccountRole>(addAccountRoleRequestDto);

            accountRole.Id = id;
            await _accountRoleRepository.UpdateAsync(accountRole);

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
            var data = await _accountRoleRepository.GetByIdAsync(id);

            if (data == null)
            {
                return 0; // not found
            }

            await _accountRoleRepository.DeleteAsync(data);

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
