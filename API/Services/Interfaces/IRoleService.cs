using API.DTOs.Roles;

namespace API.Services.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<RoleResponseDto>?> GetAllAsync();
    Task<RoleResponseDto?> GetByIdAsync(Guid id);
    Task<int> CreateAsync(RoleRequestDto roleRequestDto);
    Task<int> UpdateAsync(Guid id, RoleRequestDto roleRequestDto);
    Task<int> DeleteAsync(Guid id);
   
}
