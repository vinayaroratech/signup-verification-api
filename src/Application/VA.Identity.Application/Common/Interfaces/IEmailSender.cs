using System.Threading.Tasks;

namespace VA.Identity.Application.Common.Interfaces
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(
            string email,
            string subject,
            string plainTextMessage,
            string htmlMessage);
    }
}
