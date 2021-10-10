using VA.Identity.Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace VA.Identity.Infrastructure.Services
{
    public class SendGridEmailSender : IEmailSender
    {
        public SendGridEmailSender(IOptions<SendGridEmailSenderOptions> options)
        {
            Options = options.Value;
        }

        public SendGridEmailSenderOptions Options { get; set; }

        public async Task<bool> SendEmailAsync(
            string email,
            string subject,
            string plainTextMessage,
            string htmlMessage)
        {
            var response = await Execute(Options.ApiKey, subject, plainTextMessage, htmlMessage, email);
            return response.IsSuccessStatusCode;
        }

        private async Task<Response> Execute(
            string apiKey,
            string subject,
            string plainTextMessage,
            string htmlMessage,
            string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Options.SenderEmail, Options.SenderName),
                Subject = subject,
                PlainTextContent = plainTextMessage,
                HtmlContent = htmlMessage
            };
            msg.AddTo(new EmailAddress(email));

            msg.SetClickTracking(false, false);
            msg.SetOpenTracking(false);
            msg.SetGoogleAnalytics(false);
            msg.SetSubscriptionTracking(false);

            return await client.SendEmailAsync(msg);
        }
    }
}