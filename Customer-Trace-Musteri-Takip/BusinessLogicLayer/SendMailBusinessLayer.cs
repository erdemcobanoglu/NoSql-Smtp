using DataAccessLayer;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class SendMailBusinessLayer :IBusinessLayer<Mail>
    {
        LiteDbSendMailDataMapper getDatalayer;

        public bool Delete(Mail item)
        {
            try
            {
                getDatalayer = new LiteDbSendMailDataMapper();

                if (item.Id > 0)
                    getDatalayer.Delete(item);

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }

        public List<Mail> GetAll()
        {
            try
            {
                getDatalayer = new LiteDbSendMailDataMapper();
                var data = getDatalayer.GetAll();

                return data;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }

        public Mail GetById(Mail item)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Mail item)
        {
            try
            {
                getDatalayer = new LiteDbSendMailDataMapper();
                if (item != null)
                    getDatalayer.Insert(item);

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }

        public bool Update(Mail item)
        {
            try
            {
                getDatalayer = new LiteDbSendMailDataMapper();
                if (item != null && item.Id > 0)
                    getDatalayer.Update(item);

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }

        public bool MailsendManuel(Mail model)
        {
            // Code Link  /*http://csharp.net-informations.com/communications/csharp-smtp-mail.htm*/
            // first step download ngut package smtp(Mail.dll  .NET IMAP, POP3vs.)
            // ve en önemlisi mailde ayarları yapmamız gerekiyor
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(model.SmtpServer); //server port
                #region server nested port
                // SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587); //server port 
                #endregion
                mail.From = new MailAddress(model.From);
                mail.To.Add(model.ToAdd);
                mail.Subject = (model.Subject);
                mail.Body = model.Body;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Port = model.Port;  // or alternative server port
                SmtpServer.Credentials = new System.Net.NetworkCredential(model.From, model.Password);

                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Mail Gönderilemedi {0}", ex.ToString());
            }

            return true;


        }


        public bool MailsendCustomer( Mail model, Customer _customerModel)
        {
            // Code Link  /*http://csharp.net-informations.com/communications/csharp-smtp-mail.htm*/
            // first step download ngut package smtp(Mail.dll  .NET IMAP, POP3vs.)
            // ve en önemlisi mailde ayarları yapmamız gerekiyor
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(model.SmtpServer); //server port
                #region server nested port
                // SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587); //server port 
                #endregion
                mail.From = new MailAddress(model.From);
                mail.To.Add(_customerModel.MailAddress);
                mail.Subject = (model.Subject);
                mail.Body = model.Body;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Port = model.Port;  
                SmtpServer.Credentials = new System.Net.NetworkCredential(model.From, model.Password);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

            }
            catch
            {
                Console.WriteLine($"Mail Gönderilemedi {_customerModel.FullName} Adlı kullanıcının mail bilgilerini kontrol ediniz bu alan boş geçilemez!");
            }

            return true;

            
        }

       
    }
}
