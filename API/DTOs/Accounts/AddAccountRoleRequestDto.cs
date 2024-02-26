namespace API.DTOs.AccountRoles;

public record AddAccountRoleRequestDto(Guid AccountId, Guid RoleId);
