using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Note.Function.Repository;
using Note.Function.Services;
using System;
using System.Linq;

namespace Note.Function
{
    public class SendEmailFunction
    {
        private readonly INoteRepository _noteRepository;
        private readonly IEmailService _emailService;

        public SendEmailFunction(INoteRepository noteRepository, IEmailService emailService)
        {
            _noteRepository = noteRepository;
            _emailService = emailService;
        }

        [FunctionName("SendEmail")]
        public async Task SendEmail([TimerTrigger("0 0 4 * * *")] TimerInfo timer)
        {

            var notes = await _noteRepository.GetAsync();
            var lastFiveNotes = notes.OrderByDescending(n => n.CreatedAt).Take(5).ToList();

            if (lastFiveNotes.Any())
            {
                var emailBody = "EMAIL BODY";
                await _emailService.SendEmailAsync("test@test.com", "Daily Summary of Your Notes", emailBody);
            }
        }
    }
}
