using System.Threading.Tasks;

namespace Pure.NetCoreExtensions
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string[] to, string[] cc, string[] bcc, string subject, string message, params Attachment[] Attachments);

        Task SendEmailAsync(string to, string cc, string bcc, string subject, string message, params Attachment[] Attachments);

        Task SendEmailAsync(string to, string subject, string message, params Attachment[] Attachments);

        Task PostEmailAsync(string name, string[] to, string[] cc, string[] bcc, string subject, string message, params Attachment[] Attachments);

        Task PostEmailAsync(string name, string to, string cc, string bcc, string subject, string message, params Attachment[] Attachments);

        Task PostEmailAsync(string name, string to, string subject, string message, params Attachment[] Attachments);
    }
}
