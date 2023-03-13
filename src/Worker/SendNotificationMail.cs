using MessageHandler.Quickstart.Contract;
using MessageHandler.Runtime.AtomicProcessing;

namespace Worker
{
    public class SendNotificationMail : IHandle<NotifyBuyer>
    {
        private readonly ILogger<SendNotificationMail> logger;
        private readonly ISendEmails emailSender;

        public SendNotificationMail(ISendEmails emailSender, ILogger<SendNotificationMail> logger = null!)
        {
            this.logger = logger;
            this.emailSender = emailSender;
        }

        public async Task Handle(NotifyBuyer message, IHandlerContext context)
        {
            logger?.LogInformation("Received NotifyBuyer command, notifying the buyer...");

            await emailSender.SendAsync(message.From, message.To, message.Subject, message.Body);

            logger?.LogInformation("Notification email sent");
        }
    }
}
