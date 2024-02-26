using API.Models;

namespace API.Services.Interfaces;

public interface IOvertimeService
{
    Task<IEnumerable<Overtime>?> GetAllAsync();
    Task<Overtime?> GetByIdAsync(Guid id);
    Task<int> CreateAsync(Overtime overtime);
    Task<int> UpdateAsync(Guid id, Overtime overtime);
    Task<int> DeleteAsync(Guid id);
}
