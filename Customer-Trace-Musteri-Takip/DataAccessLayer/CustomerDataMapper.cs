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
    public class CustomerDataMapper : IDataMapper<Customer>
    {
        #region SqlConnection
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-INVSUP5\SQLEXPRESS; initial Catalog=CustomerTrace;Integrated Security = True;");
        #endregion

        public bool Delete(Customer item)
        {
            try
            {
                sqlOpen();

                sqlCon.Query<Customer>(@"DELETE FROM [dbo].[Customer] WHERE Id = @Id",item);

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("Delete Hatası 001 "+ex.Message.ToString());
            }
            finally
            {
                sqlClose();
            }
        }

        public List<Customer> GetAll()
        {
            try
            {
                sqlOpen();

                List<Customer> customerList = sqlCon.Query<Customer>("SELECT * FROM [dbo].[Customer]").ToList();

                return customerList;
            }
            catch (Exception ex)
            {

                throw new Exception("GetAll Hatası 001  "+ex.Message.ToString());
            }
            finally
            {
                sqlClose();
            }
        }

        public Customer GetById(Customer item)
        {
            try
            {
                sqlOpen();

                Customer customerGetById = sqlCon.Query<Customer>(@"SELECT * FROM [dbo].[Customer] WHERE Id = @Id",item).FirstOrDefault();

                return customerGetById;
            }
            catch (Exception ex)
            {

                throw new Exception("GetById Hatası 001 "+ex.Message.ToString());
            }
            finally
            {
                sqlClose();
            }
        }

        public bool Insert(Customer item)
        {
            try
            {
                sqlOpen();

                sqlCon.Query<Customer>(@"INSERT INTO [dbo].[Customer]
                                           ([Name]
                                           ,[Surname]
                                           ,[PhoneNumber]
                                           ,[PaymentDate]
                                           ,[Mount]
                                           ,[TotalPayment]
                                           ,[MemberRemove]
                                           ,[PassPaymentDate]
                                           ,[Fullname])
                                     VALUES
                                           (@Name, 
                                           @Surname, 
                                           @PhoneNumber, 
                                           @PaymentDate, 
                                           @Mount, 
                                           @TotalPayment, 
                                           @MemberRemove, 
                                           @PassPaymentDate, 
                                           @Fullname)", item);

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("Insert Hatası 001  "+ ex.Message.ToString());
            }
            finally
            {
                sqlClose();
            }
        }

        public bool Update(Customer item)
        {
            try
            {
                sqlOpen();

                sqlCon.Query<Customer>(@"UPDATE [dbo].[Customer]
                                           SET [Name] = @Name
                                              ,[Surname] = @Surname
                                              ,[PhoneNumber] = @PhoneNumber
                                              ,[PaymentDate] = @PaymentDate
                                              ,[Mount] = @Mount
                                              ,[TotalPayment] = @TotalPayment
                                              ,[MemberRemove] = @MemberRemove
                                              ,[PassPaymentDate] = @PassPaymentDate
                                              ,[Fullname] = @Fullname
                                             WHERE Id = @Id", item);

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("Update Hatası 001  "+ex.Message.ToString());
            }
            finally
            {
                sqlClose();
            }
        }

        public List<Customer> Search(Customer item)
        {
            try
            {
                sqlOpen();



                #region JustFirstnameSearch
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@SearchText", item.searchText);
                List<Customer> customerList = sqlCon.Query<Customer>("CustomerTraceSearch", parameter, commandType: System.Data.CommandType.StoredProcedure).ToList();
                #endregion



                return customerList;
            }
            catch (Exception ex)
            {

                throw new Exception("Search Hatası 001  " + ex.Message.ToString());
            }
            finally
            {
                sqlClose();
            }
        }

        #region Connection Open  Close Method
        void sqlOpen()
        {
            if (sqlCon.State == System.Data.ConnectionState.Closed)
                sqlCon.Open();
        }
        void sqlClose()
        {
            if (sqlCon.State == System.Data.ConnectionState.Open)
                sqlCon.Close();
        }
        #endregion
    }
}
