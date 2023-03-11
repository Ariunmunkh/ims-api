using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using Connection.Model;
using System.Net;

namespace LConnection
{
    public class EmailSender
    {
        public static MResult EmailSending(string to, string subject, string body)
        {
            try
            {
                string senderemail = "is11d011@gmail.com";
                string password = "yosrlferubyjeppv";

                MailMessage newMail = new MailMessage();
                // use the Gmail SMTP Host
                using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
                {
                    ServicePointManager.Expect100Continue = false;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => { return true; };

                    // Follow the RFS 5321 Email Standard
                    newMail.From = new MailAddress(senderemail, "dms system");

                    newMail.To.Add(to);// declare the email subject

                    newMail.Subject = subject; // use HTML for the email body

                    newMail.IsBodyHtml = true;

                    newMail.Body = body;

                    // enable SSL for encryption across channels
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    // Port 465 for SSL communication
                    client.Port = 587;

                    // Provide authentication information with Gmail SMTP server to authenticate your sender account
                    client.Credentials = new System.Net.NetworkCredential(senderemail, password);

                    client.Send(newMail); // Send the constructed mail
                }
                return new MResult { retmsg = "Email Sent" };
            }
            catch (Exception ex)
            {
                return new MResult { rettype = 1, retmsg = ex.Message };
            }
        }

    }
}
