using System.Net.Mail;
using System.Net;

namespace Vehicle_Rent.Services.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly string? _smtpServer;
        private readonly int _smtpPort;
        private readonly string? _smtpUsername;
        private readonly string? _smtpPassword;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _smtpServer = _configuration["EmailSettings:SmtpServer"];
            _smtpPort = _configuration.GetValue<int>("EmailSettings:SmtpPort");
            _smtpUsername = _configuration["EmailSettings:SmtpUsername"];
            _smtpPassword = _configuration["EmailSettings:SmtpPassword"];
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            try
            {
                using (SmtpClient smtpClient = new SmtpClient(_smtpServer, _smtpPort))
                {

                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
                    smtpClient.EnableSsl = true;

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(_smtpUsername); // Use your email address here
                    mailMessage.To.Add(email);
                    mailMessage.Subject = subject;
                    mailMessage.Body = htmlMessage;
                    mailMessage.IsBodyHtml = true;

                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., logging)
                Console.WriteLine($"Failed to send email: {ex.Message}");
                throw;
            }
        }
    }
}