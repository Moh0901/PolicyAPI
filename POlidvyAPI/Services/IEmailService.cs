using POlidvyAPI.ViewModels;

namespace POlidvyAPI.Services
{
    public interface IEmailService
    {
        Task SendEmail(EmailViewModel mailRequest);

    }
}
