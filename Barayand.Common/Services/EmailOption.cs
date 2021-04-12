using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Barayand.Common.Services
{
    public class EmailOption
    {

        //public static string Email = "site@valhallaplanet.art";
        public static string Email = "zcpc.co@gmail.com";

        /// <summary>
        /// ارسال ایمیل
        /// </summary>
        /// <param name="body">محتوا</param>
        /// <param name="subject">موضوع</param>
        /// <param name="attachment">فایل</param>
        /// <param name="toMails">ایمیل ها</param>
        /// <returns></returns>
        public static async Task<bool> SendEmailAsyc(string body, string subject, byte[] buffer = null, params string[] toMails)
        {
            try
            {


                //string password = "7Jd4qf3@";
                string password = "Hosein1756";
                string sender = "Valhallaplanet - Invoice";
                //string Host = "webmail.valhallaplanet.art";
                string Host = "smtp.gmail.com";
                var mailMsg = new MailMessage();
                mailMsg.BodyEncoding = Encoding.UTF8;
                mailMsg.HeadersEncoding = Encoding.UTF8;
                mailMsg.SubjectEncoding = Encoding.UTF8;
                mailMsg.Priority = MailPriority.Normal;
                mailMsg.Subject = subject;
                mailMsg.Body = body;
                mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mailMsg.IsBodyHtml = true;
                mailMsg.From = new MailAddress(Email, sender, Encoding.UTF8);
                mailMsg.Sender = new MailAddress(Email, sender, Encoding.UTF8);
                 foreach (var mail in toMails)
                 {
                mailMsg.To.Add(new MailAddress(mail));
                   }
                if (buffer != null)
                    mailMsg.Attachments.Add(new Attachment(new MemoryStream(buffer),subject+".pdf"));

                var smtp = new SmtpClient(Host, 587);
                smtp.EnableSsl = true;
                smtp.Timeout = 10000;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(Email, password);
                await smtp.SendMailAsync(mailMsg);
                return true;
            }
            catch (Exception e)
            {
                //Tools.AlertBox(page, string.Format("خطا در ارسال ایمیل \n  شرح خطا: {0}", e.Message));
                return false;
            }




        }

        public static bool SendEmail(string body, string subject, string attachment = "", params string[] toMails)
        {
            try
            {


                string password = "A1373mDG2";
                string sender = " محمد دمیرچلی";
                string Host = "smtp.gmail.com";


                var mailMsg = new MailMessage();
                mailMsg.BodyEncoding = Encoding.UTF8;
                mailMsg.HeadersEncoding = Encoding.UTF8;
                mailMsg.SubjectEncoding = Encoding.UTF8;
                mailMsg.Priority = MailPriority.Normal;
                mailMsg.Subject = subject;
                mailMsg.Body = body;
                mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                mailMsg.IsBodyHtml = true;
                mailMsg.From = new MailAddress(Email, sender, Encoding.UTF8);
                mailMsg.Sender = new MailAddress(Email, sender, Encoding.UTF8);
                foreach (var mail in toMails)
                {
                    mailMsg.To.Add(new MailAddress(mail));
                }
                if (attachment != "")
                    mailMsg.Attachments.Add(new Attachment(attachment));

                var smtp = new SmtpClient(Host, 587);
                smtp.EnableSsl = true;
                smtp.Timeout = 10000;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential(Email, password);

                smtp.Send(mailMsg);
                return true;
            }
            catch (Exception e)
            {
                //Tools.AlertBox(page, string.Format("خطا در ارسال ایمیل \n  شرح خطا: {0}", e.Message));
                return false;
            }




        }


    }
}
