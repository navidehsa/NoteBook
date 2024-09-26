using Moq;
using Note.Function.Entities;
using Note.Function.Repository;
using Note.Function.Services;

namespace Note.Function.Tests;

public class SendEmailFunctionTests
{
    [Fact]
    public async Task SendDailySummary_Should_Send_Email()
    {
        // Arrange
        var mockEmailService = new Mock<IEmailService>();
        var mockNoteRepository = new Mock<INoteRepository>();
        mockNoteRepository.Setup(repo => repo.GetAsync())
            .ReturnsAsync(new List<PersonalNote>
            {
                new PersonalNote()
                {
                    Content = "Test Content",
                    CreatedAt = DateTime.UtcNow,
                    Id = "1", 
                    ReminderTime = DateTime.UtcNow,
                    Title = "Test Title"
                }
            });

        var timerFunction = new SendEmailFunction(mockNoteRepository.Object,mockEmailService.Object);



        // Act
        await timerFunction.SendEmail(null);

        // Assert
        mockEmailService.Verify(service => service.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
}