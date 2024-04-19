namespace Vehicle_Rent.Services.EmailSender
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
