using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;

namespace API.Services;

public class OvertimeService : IOvertimeService
{
    private readonly IOvertimeRepository _overtimeRepository;
    
    public OvertimeService(IOvertimeRepository overtimeRepository)
    {
        _overtimeRepository = overtimeRepository;
    }
    
    public async Task<IEnumerable<Overtime>?> GetAllAsync()
    {
        try
        {
            var data = await _overtimeRepository.GetAllAsync();

            return data; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message, 
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<Overtime?> GetByIdAsync(Guid id)
    {
        try
        {
            var data = await _overtimeRepository.GetByIdAsync(id);

            return data; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message, 
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<int> CreateAsync(Overtime overtime)
    {
        try
        {
            await _overtimeRepository.CreateAsync(overtime);

            return 1; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message, 
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }    
    }

    public async Task<int> UpdateAsync(Guid id, Overtime overtime)
    {
        try
        {
            var data = await _overtimeRepository.GetByIdAsync(id);
            
            if (data == null)
            {
                return 0; // not found
            }
            
            overtime.Id = id;
            await _overtimeRepository.UpdateAsync(overtime);

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
            var data = await _overtimeRepository.GetByIdAsync(id);
            
            if (data == null)
            {
                return 0; // not found
            }
            
            await _overtimeRepository.DeleteAsync(data);

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
