using DataAccessLayer;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class AdministrationBusinessLayer : IBusinessLayer<Administration>
    {
        LiteDbAdministrationDataMapper getDatalayer;

        public bool Delete(Administration item)
        {
            try
            {
                getDatalayer = new LiteDbAdministrationDataMapper();

                if (item.Id > 0)
                    getDatalayer.Delete(item);

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }

        public List<Administration> GetAll()
        {
            try
            {
                getDatalayer = new LiteDbAdministrationDataMapper();
                var data = getDatalayer.GetAll();

                return data;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }

        public Administration GetById(Administration item)
        {
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

        public bool Insert(Administration item)
        {
            try
            {
                getDatalayer = new LiteDbAdministrationDataMapper();
                if (item != null)
                    getDatalayer.Insert(item);

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }

        public bool Update(Administration item)
        {
            try
            {
                getDatalayer = new LiteDbAdministrationDataMapper();
                if (item != null && item.Id > 0)
                    getDatalayer.Update(item);

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
