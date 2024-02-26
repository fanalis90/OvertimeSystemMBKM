using API.Models;

namespace API.Repositories.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<Employee?> GetByNikAsync(string nik);
}
