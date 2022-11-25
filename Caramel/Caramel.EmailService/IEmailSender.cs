namespace Caramel.EmailService
{
    public interface IEmailSender 
    {
      void SendEmail(Message message);
    }
}
