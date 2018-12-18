using DataAccessLayer;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class CustomerBusinessLayer : IBusinessLayer<Customer>
    {
        LiteDbCustomerDataMapper getDatalayer;

        public bool Delete(Customer item)
        {
            try
            {
                getDatalayer = new LiteDbCustomerDataMapper();

                if (item.Id > 0)
                    getDatalayer.Delete(item);

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }

        public List<Customer> GetAll()
        {
            try
            {
                getDatalayer = new LiteDbCustomerDataMapper();
                var data = getDatalayer.GetAll();

                return data;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }

        // ToDo
        public Customer GetById(Customer item)
        {
            // ToDo
            try
            {
                if (item.Id > 0)
                    getDatalayer.GetById(item);

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Insert(Customer item)
        {
            try
            {
                getDatalayer = new LiteDbCustomerDataMapper();
                if (item != null)
                    getDatalayer.Insert(item);

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }

        public bool Update(Customer item)
        {
            try
            {
                getDatalayer = new LiteDbCustomerDataMapper();
                if (item != null && item.Id > 0)
                    getDatalayer.Update(item);

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }

        }

        public List<Customer> Search(Customer item)
        {
            try
            {
                getDatalayer = new LiteDbCustomerDataMapper();
                List<Customer> searchlist = new List<Customer>();

                if (!string.IsNullOrWhiteSpace(item.searchText) || item.Id > -1)
                    searchlist = getDatalayer.Search(item);


                return searchlist;
                
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
           
        }
    }
}
