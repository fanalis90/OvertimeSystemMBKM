namespace API.DTOs.Overtimes;

public class OvertimeDetailResponseDto
{
    public Guid Id { get; init; }
    public DateTime Date { get; init; }
    public string Reason { get; init; }
    public int TotalHours { get; init; }
    public string Status { get; init; }
    public IEnumerable<OvertimeRequestResponseDto> Requests { get; init; }
}
