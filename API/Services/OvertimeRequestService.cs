using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;

namespace API.Services;

public class OvertimeRequestService : IOvertimeRequestService
{
    private readonly IOvertimeRequestRepository _overtimeRequestRepository;

    public OvertimeRequestService(IOvertimeRequestRepository overtimeRequestRepository)
    {
        _overtimeRequestRepository = overtimeRequestRepository;
    }

    public async Task<IEnumerable<OvertimeRequest>?> GetAllAsync()
    {
        try
        {
            var data = await _overtimeRequestRepository.GetAllAsync();

            return data; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message,
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<OvertimeRequest?> GetByIdAsync(Guid id)
    {
        try
        {
            var data = await _overtimeRequestRepository.GetByIdAsync(id);

            return data; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message,
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<int> CreateAsync(OvertimeRequest overtimeRequest)
    {
        try
        {
            await _overtimeRequestRepository.CreateAsync(overtimeRequest);

            return 1; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message,
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<int> UpdateAsync(Guid id, OvertimeRequest overtimeRequest)
    {
        try
        {
            var data = await _overtimeRequestRepository.GetByIdAsync(id);

            if (data == null) return 0; // not found

            overtimeRequest.Id = id;
            await _overtimeRequestRepository.UpdateAsync(overtimeRequest);

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
            var data = await _overtimeRequestRepository.GetByIdAsync(id);

            if (data == null) return 0; // not found

            await _overtimeRequestRepository.DeleteAsync(data);

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
