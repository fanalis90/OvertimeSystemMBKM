using API.DTOs.Employees;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;
    
    public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmployeeResponseDto>?> GetAllAsync()
    {
        try
        {
            var data = await _employeeRepository.GetAllAsync();

            var dataMap = _mapper.Map<IEnumerable<EmployeeResponseDto>>(data);
            
            return dataMap; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message, 
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<EmployeeResponseDto?> GetByIdAsync(Guid id)
    {
        try
        {
            var data = await _employeeRepository.GetByIdAsync(id);

            var dataMap = _mapper.Map<EmployeeResponseDto>(data);
            
            return dataMap; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message, 
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<int> CreateAsync(EmployeeRequestDto employeeRequestDto)
    {
        try
        {
            var employee = _mapper.Map<Employee>(employeeRequestDto);
            
            await _employeeRepository.CreateAsync(employee);

            return 1; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message, 
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }    
    }

    public async Task<int> UpdateAsync(Guid id, EmployeeRequestDto employeeRequestDto)
    {
        try
        {
            var data = await _employeeRepository.GetByIdAsync(id);
            
            if (data == null)
            {
                return 0; // not found
            }

            var employee = _mapper.Map<Employee>(employeeRequestDto);
            
            employee.Id = id;
            await _employeeRepository.UpdateAsync(employee);

            return 1; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message, 
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        try
        {
            var data = await _employeeRepository.GetByIdAsync(id);
            
            if (data == null)
            {
                return 0; // not found
            }
            
            await _employeeRepository.DeleteAsync(data);

            return 1; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message, 
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }
}
