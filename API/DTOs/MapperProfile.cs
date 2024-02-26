using System.Collections;
using API.DTOs.AccountRoles;
using API.DTOs.Accounts;
using API.DTOs.Employees;
using API.DTOs.Overtimes;
using API.DTOs.Roles;
using API.Models;
using AutoMapper;

namespace API.DTOs;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // For Employees
        CreateMap<EmployeeRequestDto, Employee>()
           .ForMember(dest => dest.JoinedDate,
                      opt => opt.MapFrom(src => new DateTime()));

        CreateMap<Employee, EmployeeResponseDto>();

        // For Accounts
        CreateMap<AccountRequestDto, Account>()
           .ForMember(dest => dest.Otp,
                      opt => opt.MapFrom(src => 0))
           .ForMember(dest => dest.Expired,
                      opt => opt.MapFrom(src => new DateTime()))
           .ForMember(dest => dest.IsUsed,
                      opt => opt.MapFrom(src => true))
           .ForMember(dest => dest.IsActive,
                      opt => opt.MapFrom(src => true));
        CreateMap<Account, AccountResponseDto>()
           .ForMember(dest => dest.Roles,
                      opt => opt.MapFrom(src => src.AccountRoles.Select(ar => ar.Role.Name)));
        
        // For AccountRoles
        CreateMap<AddAccountRoleRequestDto, AccountRole>();
        CreateMap<RemoveAccountRoleRequestDto, AccountRole>();
        CreateMap<AccountRole, AccountRoleResponseDto>();
        
        // For Roles
        CreateMap<RoleRequestDto, Role>();
        CreateMap<Role, RoleResponseDto>();
        
        // For Overtimes and OverTimeRequests
        CreateMap<OvertimeRequestDto, Overtime>()
           .ForMember(dest => dest.Status,
                      opt => opt.MapFrom(src => "Requested"));
        CreateMap<OvertimeRequestDto, OvertimeRequest>();
        
    }
}
