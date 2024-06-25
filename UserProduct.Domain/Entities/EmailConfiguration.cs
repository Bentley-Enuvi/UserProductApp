namespace UserProduct.Domain.Entities
{
    public class EmailConfiguration
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; } = "bentleyenuvi@gmail.com";
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

