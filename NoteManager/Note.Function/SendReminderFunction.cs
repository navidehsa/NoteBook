using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Note.Function.Repository;
using Note.Function.Services;
using System;
using System.Linq;

namespace Note.Function
{
    public class SendReminderFunction
    {

        private readonly INoteRepository _noteRepository;
        private readonly IEmailService _emailService;

        public SendReminderFunction(INoteRepository noteRepository, IEmailService emailService)
        {
            _noteRepository = noteRepository;
            _emailService = emailService;
        }

        [FunctionName("SendReminder")]
        public async Task SendReminder([TimerTrigger("1.00:00:00")] TimerInfo timer, ILogger log)
        {
            log.LogInformation($"Sending daily summary at: {DateTime.Now}");

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
