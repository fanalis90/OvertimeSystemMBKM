using System.Security.Claims;

namespace API.Utilities.Handlers.Interfaces
{
    public interface IJWTHandler
    {
        string Generate(IEnumerable<Claim> claims);
    }
}
