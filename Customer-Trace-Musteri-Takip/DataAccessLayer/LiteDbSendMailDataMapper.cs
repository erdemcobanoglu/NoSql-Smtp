using EntitiesLayer;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class LiteDbSendMailDataMapper : IDataMapper<Mail>
    {
        private static string DbName = "Fitnesshall.db";
        private static string ListName = "administration";
        public bool Delete(Mail item)
        {
            try
            {
                using (var db = new LiteDatabase(DbName))
                {
                    var mail = db.GetCollection<Mail>(ListName);
                    mail.Delete(x => x.Id == item.Id);
                }
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("Delete Hatası 001 " + ex.Message.ToString());
            }
        }

        public List<Mail> GetAll()
        {
            try
            {
                List<Mail> mailList;
                using (var db = new LiteDatabase(DbName))
                {

                    var mail = db.GetCollection<Mail>(ListName);

                    var rawList = mail.FindAll();
                    mailList = new List<Mail>();

                    foreach (var mailData in rawList)
                    {
                        mailList.Add(mailData);
                    }
                }
                return mailList;
            }
            catch (Exception ex)
            {
                throw new Exception("GetAll Hatası 001  " + ex.Message.ToString());
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
                using (var db = new LiteDatabase(DbName))
                {
                    var mail = db.GetCollection<Mail>(ListName);
                    mail.Insert(item);
                }
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("Insert Hatası 001  " + ex.Message.ToString());
            }
        }

        public bool Update(Mail item)
        {
            try
            {
                using (var db = new LiteDatabase(DbName))
                {
                    var mail = db.GetCollection<Mail>(ListName);
                    mail.Update(item);
                }
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("Update Hatası 001  " + ex.Message.ToString());
            }
        }
    }
}
