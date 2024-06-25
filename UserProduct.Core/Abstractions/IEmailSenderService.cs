namespace UserProduct.Core.Abstractions
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string to, string subject, string content);
    }
}
