namespace API.DTOs.Overtimes;

public class OvertimeRequestDto
{
    public Guid AccountId { get; init; }
    public DateTime Date { get; init; }
    public string Reason { get; init; }
    public int TotalHours { get; init; }
}
