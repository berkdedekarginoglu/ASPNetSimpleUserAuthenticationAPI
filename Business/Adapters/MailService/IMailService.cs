using Business.Adapters.MailService;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Mail
{
    public interface IMailService
    {
        Task<bool> SendMail(string mailTo, MailBody mailBody);
    }
}
