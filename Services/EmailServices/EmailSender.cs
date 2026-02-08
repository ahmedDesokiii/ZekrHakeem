
namespace ZekrHakeem_v11.Services.EmailServices
{


    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        //ugvb iluy vgec ykwg
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                GetEmailSetting getEmailSetting = new()
                {
                    SecretKey = _configuration.GetValue<string>("AppSettings:SecretKey"),
                    From = _configuration.GetValue<string>("AppSettings:MailSettings:From"),
                    Pass = _configuration.GetValue<string>("AppSettings:MailSettings:Pass"),
                    SmtpServer = _configuration.GetValue<string>("AppSettings:MailSettings:SmtpServer"),
                    Port = _configuration.GetValue<int>("AppSettings:MailSettings:Port"),
                    EnableSSL = _configuration.GetValue<bool>("AppSettings:MailSettings:EnableSSL"),
                };
                MailMessage mailMessage = new()
                {
                    From = new MailAddress(getEmailSetting.From),
                    Subject = subject,
                    Body = htmlMessage
                };
                mailMessage.To.Add(email);
                mailMessage.Body = $"<html><body>{htmlMessage}</body></html>";
                mailMessage.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient(getEmailSetting.SmtpServer)
                {
                    Port = getEmailSetting.Port,
                    Credentials = new NetworkCredential(getEmailSetting.From, getEmailSetting.Pass),
                    EnableSsl = getEmailSetting.EnableSSL
                };
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception)
            {

                throw;
            }


        }


    }
}