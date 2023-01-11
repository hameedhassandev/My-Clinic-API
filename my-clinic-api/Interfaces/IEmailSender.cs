using my_clinic_api.Models.MailConfirmation;

namespace my_clinic_api.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(Messages message);
    }
}
