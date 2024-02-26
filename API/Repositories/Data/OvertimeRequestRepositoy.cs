using API.Data;
using API.Models;
using API.Repositories.Interfaces;

namespace API.Repositories.Data;

public class OvertimeRequestRepositoy : GeneralRepository<OvertimeRequest>, IOvertimeRequestRepository
{
    public OvertimeRequestRepositoy(OvertimeSystemDbContext context) : base(context) { }
}
