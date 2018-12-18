using Dapper;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class AdministrationDataMapper : IDataMapper<Administration>
    {
        #region SqlConnection
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-INVSUP5\SQLEXPRESS; initial Catalog=CustomerTrace;Integrated Security = True;");
        #endregion

        public bool Delete(Administration item)
        {
            try
            {
                sqlOpen();

                sqlCon.Query<Administration>(@"DELETE FROM [dbo].[Administrations] WHERE Id = @Id", item);

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("Delete Hatası 001 " + ex.Message.ToString());
            }
            finally
            {
                sqlClose();
            }
        }

        

        public List<Administration> GetAll()
        {

            try
            {
                sqlOpen();

                List<Administration> AdministrationList = sqlCon.Query<Administration>("SELECT * FROM [dbo].[Administrations]").ToList();

                return AdministrationList;
            }
            catch (Exception ex)
            {

                throw new Exception("GetAll Hatası 001  " + ex.Message.ToString());
            }
            finally
            {
                sqlClose();
            }
        }

        public Administration GetById(Administration item)
        {
            try
            {
                sqlOpen();

                Administration AdministrationGetById = sqlCon.Query<Administration>(@"SELECT * FROM [dbo].[Administrations] WHERE Id = @Id", item).FirstOrDefault();

                return AdministrationGetById;
            }
            catch (Exception ex)
            {

                throw new Exception("GetById Hatası 001 " + ex.Message.ToString());
            }
            finally
            {
                sqlClose();
            }
        }

        public bool Insert(Administration item)
        {
            try
            {
                sqlOpen();

                sqlCon.Query<Administration>(@"INSERT INTO [dbo].[Administrations]
                                           ([Username]
                                           ,[Password])
                                     VALUES
                                           (@Username, 
                                           @Password) ", item);

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("Insert Hatası 001  " + ex.Message.ToString());
            }
            finally
            {
                sqlClose();
            }
        }

        public bool Update(Administration item)
        {
            try
            {
                sqlOpen();

                sqlCon.Query<Administration>(@"UPDATE [dbo].[Administrations]
                                       SET [Username] = @Username
                                          ,[Password] = @Password
                                             WHERE Id = @Id", item);

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("Update Hatası 001  " + ex.Message.ToString());
            }
            finally
            {
                sqlClose();
            }
        }

        private void sqlClose()
        {

            if (sqlCon.State == System.Data.ConnectionState.Closed)
                sqlCon.Open();
        }

        private void sqlOpen()
        {
            if (sqlCon.State == System.Data.ConnectionState.Open)
                sqlCon.Close();
        }
    }
}
