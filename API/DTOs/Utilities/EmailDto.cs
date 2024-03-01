namespace API.DTOs.Utilities
{
    public record EmailDto(string To,
        string Subject, string Body)
    {
    }
}
