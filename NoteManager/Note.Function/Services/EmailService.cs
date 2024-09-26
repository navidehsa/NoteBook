using System;
using System.Threading.Tasks;

namespace Note.Function.Services
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string to, string subject, string body)
        {
            //TODO : Implement email sending using fx :sendGrid
            Console.WriteLine($"Sending email to {to} with subject {subject} and body {body}");
            return Task.CompletedTask;
        }
    }
}
