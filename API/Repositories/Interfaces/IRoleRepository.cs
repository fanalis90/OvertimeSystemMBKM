using API.Models;

namespace API.Repositories.Interfaces;

public interface IRoleRepository : IRepository<Role> {
    Task<Role> GetIdByNameAsync(string name);
}
