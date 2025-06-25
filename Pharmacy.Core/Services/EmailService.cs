//using MailKit.Net.Smtp;
//using MimeKit;
//using Microsoft.Extensions.Options;
//using System.Threading.Tasks;
//using System.Net.Mail;
//using Pharmacy.Core.IServiceContracts;
//using Pharmacy.Core.DTO;
//using MailKit.Security;

//public class EmailService : IEmailService
//{
//    private readonly EmailSettings _emailSettings;

//    public EmailService(IOptions<EmailSettings> emailSettings)
//    {
//        _emailSettings = emailSettings.Value;
//    }

//    public async Task SendEmailAsync(string toEmail, string subject, string body)
//    {
//        var email = new MimeMessage();
//        email.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
//        email.To.Add(MailboxAddress.Parse(toEmail));
//        email.Subject = subject;

//        var builder = new BodyBuilder
//        {
//            HtmlBody = body
//        };
//        email.Body = builder.ToMessageBody();

//        var smtp = new MailKit.Net.Smtp.SmtpClient(); // ✅ استخدام MailKit
//        await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.StartTls);
//        await smtp.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
//        await smtp.SendAsync(email); // يجب أن يكون email من نوع MimeMessage
//        await smtp.DisconnectAsync(true);
//    }
//}
