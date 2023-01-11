using MailKit.Net.Smtp;
using MimeKit;
using my_clinic_api.Interfaces;
using my_clinic_api.Models.MailConfirmation;


namespace my_clinic_api.Services
{
    
    public class EmailSender : IEmailSender
    {
        private readonly EmailCongiguration _emailCongiguration;

        public EmailSender(EmailCongiguration emailCongiguration) => _emailCongiguration = emailCongiguration;

        public void SendEmail(Messages message)
        {
            var emailMssage = CreateEmailMessage(message);
            Send(emailMssage);
        }

        private MimeMessage CreateEmailMessage(Messages message)
        {
            var emailMsg = new MimeMessage();
            emailMsg.From.Add(new MailboxAddress("email",_emailCongiguration.From));
            emailMsg.To.AddRange(message.To);
            emailMsg.Subject = message.Subject;
            emailMsg.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMsg;
        }

        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            
            try
            {
                client.Connect(_emailCongiguration.SmtpServer, _emailCongiguration.Port, true);
                //allows clients to send OAuth 2.0 access tokens to the server.
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailCongiguration.UserName, _emailCongiguration.Password);
                client.Send(mailMessage);
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
