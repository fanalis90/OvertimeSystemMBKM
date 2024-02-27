using API.DTOs.Overtimes;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using API.Utilities.Handlers;
using AutoMapper;

namespace API.Services;

public class OvertimeService : IOvertimeService
{
    private readonly IMapper _mapper;
    private readonly IOvertimeRepository _overtimeRepository;
    private readonly IOvertimeRequestRepository _overtimeRequestRepository;

    public OvertimeService(IOvertimeRepository overtimeRepository, IMapper mapper,
                           IOvertimeRequestRepository overtimeRequestRepository)
    {
        _overtimeRepository = overtimeRepository;
        _mapper = mapper;
        _overtimeRequestRepository = overtimeRequestRepository;
    }

    public async Task<OvertimeDownloadResponseDto> DownloadDocumentAsync(Guid overtimeId)
    {
        try
        {
            var overtime = await _overtimeRepository.GetByIdAsync(overtimeId);

            if (overtime is null)
                return new OvertimeDownloadResponseDto(BitConverter.GetBytes(0),
                                                       "0",
                                                       "0"); // id not found

            if (string.IsNullOrEmpty(overtime.Document))
                return new OvertimeDownloadResponseDto(BitConverter.GetBytes(-1),
                                                       "-1",
                                                       "-1"); // document not found

            byte[] document;
            try
            {
                document = await DocumentHandler.Download(overtime.Document);
            }
            catch
            {
                throw new Exception("File not exist in server");
            }

            return new OvertimeDownloadResponseDto(document, "application/octet-stream",
                                                   Path.GetFileName(overtime.Document)); // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message,
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public Task<int> ChangeRequestStatusAsync(OvertimeChangeRequestDto overtimeChangeRequestDto)
    {
        try
        {
            // TODO: Implement ChangeRequestStatusAsync
            throw new NotImplementedException();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message,
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<OvertimeDetailResponseDto> GetDetailByOvertimeIdAsync(Guid overtimeId)
    {
        try
        {
            var data = await _overtimeRepository.GetByIdAsync(overtimeId);

            var dataMap = _mapper.Map<OvertimeDetailResponseDto>(data);

            return dataMap; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message,
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<IEnumerable<OvertimeDetailResponseDto>?> GetDetailsAsync(Guid accountId)
    {
        try
        {
            var data = await _overtimeRepository.GetAllAsync();

            data = data.Where(x => x.OvertimeRequests.Any(or => or.AccountId == accountId));

            var dataMap = _mapper.Map<IEnumerable<OvertimeDetailResponseDto>>(data);

            return dataMap; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message,
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<int> RequestOvertimeAsync(IFormFile document, OvertimeRequestDto overtimeRequestDto)
    {
        try
        {
            if (document is null && document.Length == 0) return -1; // file not found

            var overtime = _mapper.Map<Overtime>(overtimeRequestDto);
            var upload = await DocumentHandler.Upload(document, overtime.Id);
            overtime.Document = upload;
            var data = await _overtimeRepository.CreateAsync(overtime);

            var overtimeRequset = _mapper.Map<OvertimeRequest>(data);
            overtimeRequset.AccountId = overtimeRequestDto.AccountId;
            await _overtimeRequestRepository.CreateAsync(overtimeRequset);

            return 1; // success
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message,
                              Console.ForegroundColor = ConsoleColor.Red);

            throw; // error
        }
    }

    public async Task<IEnumerable<OvertimeResponseDto>?> GetAllAsync()
    {
        try
        {
            var data = await _overtimeRepository.GetAllAsync();

            var dataMap = _mapper.Map<IEnumerable<OvertimeResponseDto>>(data);

            return dataMap; // success
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

            if (data == null) return 0; // not found

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

            if (data == null) return 0; // not found

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
