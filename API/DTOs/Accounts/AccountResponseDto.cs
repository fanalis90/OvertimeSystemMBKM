namespace API.DTOs.Accounts;

public class AccountResponseDto
{
    public Guid Id { get; init; }
    public string Password { get; init; }
    public int Otp { get; init; }
    public DateTime Expired { get; init; }
    public bool IsUsed { get; init; }
    public bool IsActive { get; init; }
    public IEnumerable<string> Roles { get; init; }
}
