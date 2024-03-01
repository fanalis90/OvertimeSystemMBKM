using API.DTOs.Roles;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services;

public class RoleService : IRoleService
{
    private readonly IMapper _mapper;
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoleResponseDto>?> GetAllAsync()
    {
        try
        {
            var data = await _roleRepository.GetAllAsync();

            var dataMap = _mapper.Map<IEnumerable<RoleResponseDto>>(data);

            return dataMap; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message,
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }


    public async Task<RoleResponseDto?> GetByIdAsync(Guid id)
    {
        try
        {
            var data = await _roleRepository.GetByIdAsync(id);

            var dataMap = _mapper.Map<RoleResponseDto>(data);

            return dataMap; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message,
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<int> CreateAsync(RoleRequestDto roleRequestDto)
    {
        try
        {
            var role = _mapper.Map<Role>(roleRequestDto);

            await _roleRepository.CreateAsync(role);

            return 1; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message,
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<int> UpdateAsync(Guid id, RoleRequestDto roleRequestDto)
    {
        try
        {
            var data = await _roleRepository.GetByIdAsync(id);

            if (data == null) return 0; // not found

            var role = _mapper.Map(roleRequestDto, data);

            role.Id = id;
            await _roleRepository.UpdateAsync(role);

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
            var data = await _roleRepository.GetByIdAsync(id);

            if (data == null) return 0; // not found

            await _roleRepository.DeleteAsync(data);

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
