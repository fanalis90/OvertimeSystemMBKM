using API.Models;

namespace API.Repositories.Interfaces;

public interface IAccountRoleRepository : IRepository<AccountRole>
{
    Task<AccountRole?> GetDataByAccountIdAndRoleAsync(Guid accountId, Guid roleId);
    Task<IEnumerable<AccountRole?>> GetByAccountIdAsync(Guid accountId);
}
