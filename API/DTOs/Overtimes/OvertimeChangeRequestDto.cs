namespace API.DTOs.Overtimes;

public record OvertimeChangeRequestDto(
    Guid Id,
    string Status,
    string Comment);
