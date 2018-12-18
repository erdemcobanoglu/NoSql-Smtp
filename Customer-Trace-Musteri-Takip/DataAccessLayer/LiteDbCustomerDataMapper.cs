using EntitiesLayer;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class LiteDbCustomerDataMapper : IDataMapper<Customer>
    {
        private static string DbName = "Fitnesshall.db";
        private static string ListName = "customer";

        public bool Delete(Customer item)
        {
            try
            {
                using (var db = new LiteDatabase(DbName))
                {
                    var administrations = db.GetCollection<Customer>(ListName);
                    administrations.Delete(x => x.Id == item.Id);
                }
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("Delete Hatası 001 " + ex.Message.ToString());
            }
        }

        public List<Customer> GetAll()
        {
            try
            {
                List<Customer> customerList;
                using (var db = new LiteDatabase(DbName))
                {
                    var customer = db.GetCollection<Customer>(ListName);
                    var rawList = customer.FindAll();
                    customerList = new List<Customer>();

                    foreach (var adminData in rawList)
                    {
                        customerList.Add(adminData);
                    }
                }
                return customerList;
            }
            catch (Exception ex)
            {

                throw new Exception("GetAll Hatası 001  " + ex.Message.ToString());
            }
        }

        public Customer GetById(Customer item)
        {
            try
            {
                Customer dataList = new Customer();
                return dataList;
            }
            catch (Exception ex)
            {

                throw new Exception("GetById Hatası 001 " + ex.Message.ToString());
            }
        }

        public bool Insert(Customer item)
        {
            try
            {
                using (var db = new LiteDatabase(DbName))
                {
                    var customer = db.GetCollection<Customer>(ListName);
                    customer.Insert(item);
                }
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("Insert Hatası 001  " + ex.Message.ToString());
            }
        }

        public bool Update(Customer item)
        {
            try
            {
                using (var db = new LiteDatabase(DbName))
                {
                    var customer = db.GetCollection<Customer>(ListName);
                    customer.Update(item);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Update Hatası 001  " + ex.Message.ToString());
            }
        }

        public List<Customer> Search(Customer item)
        {
            try
            {
                List<Customer> customerSearchList;
                using (var db = new LiteDatabase(DbName))
                {
                    var administration = db.GetCollection<Customer>(ListName);
                    customerSearchList = new List<Customer>();
                    if(!string.IsNullOrWhiteSpace(item.searchText))
                    {
                        var rawList = administration.Find(x => x.FullName.StartsWith(item.searchText));
                        foreach (var data in rawList)
                        {
                            customerSearchList.Add(data);
                        }
                    }
                    if(item.Id > 0)
                    {
                        var rawList = administration.Find(x => x.Id ==item.Id);
                        foreach (var data in rawList)
                        {
                            customerSearchList.Add(data);
                        }
                    }
                  
                }
                return customerSearchList;
            }
            catch (Exception ex)
            {
                throw new Exception("Search Hatası 001  " + ex.Message.ToString());
            }
        }
    }
}
