using EntitiesLayer;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class LiteDbAdministrationDataMapper : IDataMapper<Administration>
    {
        private static string DbName = "Fitnesshall.db";
        private static string ListName = "administration";
        public bool Delete(Administration item)
        {
            try
            {
                using (var db = new LiteDatabase(DbName))
                {
                    var administrations = db.GetCollection<Administration>(ListName);
                    administrations.Delete(x => x.Id == item.Id);
                }
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("Delete Hatası 001 " + ex.Message.ToString());
            }
        }

        public List<Administration> GetAll()
        {
            try
            {
                List<Administration> adminList;
                using (var db = new LiteDatabase(DbName))
                {

                    var administration = db.GetCollection<Administration>(ListName);

                    #region ilk Giriş için
                    //Administration item = new Administration()
                    //{
                    //    Username = "admin",
                    //    Password = "admin"
                    //};

                    //administration.Insert(item);
                    #endregion

                    var rawList = administration.FindAll();
                    adminList = new List<Administration>();

                    foreach (var adminData in rawList)
                    {
                        adminList.Add(adminData);
                    }
                }
                return adminList;
            }
            catch (Exception ex)
            {

                throw new Exception("GetAll Hatası 001  " + ex.Message.ToString());
            }
        }

        public List<Administration> GetById(Administration item)
        {
            try
            {
                List<Administration> dataList = new List<Administration>();
                return dataList;
            }
            catch (Exception ex)
            {

                throw new Exception("GetById Hatası 001 " + ex.Message.ToString());
            }
        }

        public bool Insert(Administration item)
        {
            try
            {
                using (var db = new LiteDatabase(DbName))
                {
                    var administration = db.GetCollection<Administration>(ListName);
                    administration.Insert(item);
                }
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("Insert Hatası 001  " + ex.Message.ToString());
            }
        }

        public bool Update(Administration item)
        {
            try
            {
                using (var db = new LiteDatabase(DbName))
                {
                    var administration = db.GetCollection<Administration>(ListName);
                    administration.Update(item);
                }
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("Update Hatası 001  " + ex.Message.ToString());
            }
        }

        public List<Administration> Search(Administration item)
        {
            try
            {
                List<Administration> adminSearchList;
                using (var db = new LiteDatabase(DbName))
                {
                    var administration = db.GetCollection<Administration>(ListName);
                    adminSearchList = new List<Administration>();
                    var rawList = administration.Find(x => x.Username.StartsWith(item.Username));
                }
                return adminSearchList;
            }
            catch (Exception ex)
            {
                throw new Exception("Search Hatası 001  " + ex.Message.ToString());
            }
        }

        Administration IDataMapper<Administration>.GetById(Administration item)
        {
            throw new NotImplementedException();
        }
    }
}
