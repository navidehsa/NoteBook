using System.Threading.Tasks;

namespace Note.Function.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
