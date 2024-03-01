using API.DTOs.Utilities;

namespace API.Utilities.Handlers.Interfaces
{
    public interface IEmailHandler
    {
        Task SendEmailAsync(EmailDto emailDto);
    }
}
