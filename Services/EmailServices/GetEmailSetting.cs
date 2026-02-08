namespace ZekrHakeem_v11.Services.EmailServices
{
    public class GetEmailSetting
    {
        public string SecretKey { get; set; }
        public string From { get; set; }
        public string Pass { get; set; }

        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
    }
}
