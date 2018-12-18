using BusinessLogicLayer;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinUı
{
    public partial class CustomerListForm : DevExpress.XtraEditors.XtraForm
    {
        public CustomerListForm()
        {
            InitializeComponent();
        }
        CustomerBusinessLayer getBusiness;
        Customer getData;
        AdministrationBusinessLayer getAdministrationBusiness;
        Administration getAdministrationData;
        SendMailBusinessLayer getSendMailBusiness;

        int _id = 0;
        int _adminId = 0;
       

        private void Delete(Customer model)
        {
            try
            {
                getBusiness = new CustomerBusinessLayer();
                getBusiness.Delete(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        private void DeleteAdministration(Administration model)
        {
            try
            {
                getAdministrationBusiness = new AdministrationBusinessLayer();
                getAdministrationBusiness.Delete(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                txtAdministrationName.Text = "";
                txtAdministrationPassword.Text = "";
                btnAdministrationDelete.Enabled = false;
            }
        }

        private void Save(Customer model)
        {
            try
            {
                if (_id == 0)
                {
                    getBusiness = new CustomerBusinessLayer();
                    getBusiness.Insert(model);
                }
                if(_id > 0)
                {
                    getBusiness = new CustomerBusinessLayer();
                    getBusiness.Update(model);
       
                }

                FillGrid();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        private void SaveAdministration(Administration model)
        {
            try
            {
                if (_id == 0)
                {
                    getAdministrationBusiness  = new AdministrationBusinessLayer();
                    getAdministrationBusiness.Insert(model);
                }
                if (_id > 0)
                {
                    getAdministrationBusiness = new AdministrationBusinessLayer();
                    getAdministrationBusiness.Update(model);
                }

                FillGrid();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                chckIsActive.EditValue = true;
                                getData = new Customer()
                                {
                                    Id = _id,
                                    Name = txtName.Text,
                                    Surname = txtSurname.Text,
                                    PhoneNumber = txtPhoneNumber.Text,
                                    PaymentDate = (DateTime)dtpPaymentDate.EditValue,
                                    TotalPayment = Convert.ToInt32(txtPriceAmount.Text),
                                    Mount = Convert.ToInt32(cmbMounth.Text),
                                    MemberRemove = (bool)chckIsActive.EditValue,
                                    PassPaymentDate = Convert.ToDateTime(dtpLastPaymentDay.EditValue),
                                    FullName = txtName.Text + " " + txtSurname.Text,
                                    MailAddress = txtMailAddress.Text
                                };
                                Save(getData);
                        FillGrid();
                }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

            }

        

        void FillGrid()
        {

           
            #region Fill Customer List
            getBusiness = new CustomerBusinessLayer();

            var list = getBusiness.GetAll();
            List<Customer> passPaymentDateList = new List<Customer>();
            List<Customer> _CustomerGridList = new List<Customer>();

            foreach (var item in list)
            {
                if (item.PassPaymentDate < DateTime.Now)
                {
                    passPaymentDateList.Add(item);
                }
                if (item.PassPaymentDate > DateTime.Now)
                {
                    _CustomerGridList.Add(item);
                }
            }



            lblActiveCustomer.Text = list.Count().ToString();
            lblPaymentCustomer.Text = _CustomerGridList.Count().ToString();
            lblPassPaymentCustomer.Text = passPaymentDateList.Count().ToString();
            #endregion


            #region FillDevCustomerGrid
            grdPaymentPassDate.DataSource = passPaymentDateList;

            grdAllCustomers.DataSource = _CustomerGridList;
            //grdPassPayDateMain.DataSource = passPaymentDateList;
            #endregion

            #region Fill Administration List
            getAdministrationBusiness = new AdministrationBusinessLayer();
            var adminList = getAdministrationBusiness.GetAll();
            grdAdministration.DataSource = adminList;
            #endregion
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region VisibleFindPanel
            //gridView1.ShowFindPanel();
            //gridView2.ShowFindPanel();
            gridView3.ShowFindPanel();
            gridView4.ShowFindPanel();
            #endregion


            txtAdministrationPassword.Properties.PasswordChar = '*';
            txtCheckPassword.Properties.PasswordChar = '*';
            
            txtMailPassword.Properties.PasswordChar = '*';

            
            grpCheckAdminPassAndName.Visible = true;

            try
            {
                FillGrid();
                DayOverSendMail();
                FillMailInformation();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri Tabanı Bağlantısı Kurulamadı " + ex.Message.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                getData = new Customer()
                {
                   Id = _id
                };

                Delete(getData);


                FillGrid();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            CleanTextbox();
        }

        void CleanTextbox()
        {
            _id = 0;
            txtName.Text = "";
            txtPhoneNumber.Text = "";
            txtPriceAmount.Text = "";
            txtSurname.Text = "";
            cmbMounth.EditValue = null;
            chckIsActive.EditValue = null;
            dtpPaymentDate.EditValue = null;
            dtpPaymentDate.EditValue = null;

           
            txtCheckPassword.Text = "";
        }

       

        

       void CalculateMounth(string mounthValue, DateTime startDate)
        {
            DateTime LastPaymentDay = new DateTime();
            int mounth = Convert.ToInt32(mounthValue);

            if (mounthValue != null && startDate != null)
            {
                LastPaymentDay = startDate.AddMonths(mounth);
            }

            //dtpLastPaymentDay.EditValue = LastPaymentDay.ToString("yyyy-MM-dd");
            dtpLastPaymentDay.EditValue = LastPaymentDay;
        }


        private void cmbMounth_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbMounth.EditValue != null)
                {
                    CalculateMounth(cmbMounth.EditValue.ToString(), (DateTime)dtpPaymentDate.EditValue);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Belirtilen Tüm Değerleri Girdiğinizden Emin Olunuz!");
            }
        }

      

        public void Search(Customer item)
        {
            try
            {
                getBusiness = new CustomerBusinessLayer();
                List<Customer> _searchData = new List<Customer>();

                _searchData = getBusiness.Search(item);

                List<Customer> passData = new List<Customer>();
                List<Customer> activeData = new List<Customer>();

                foreach (var data in _searchData)
                {
                    if (data.PassPaymentDate > DateTime.Now)
                    {
                        activeData.Add(data);
                    }
                    if (data.PassPaymentDate < DateTime.Now)
                    {
                        passData.Add(data);
                    }
                }

             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnListFill_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void btnAdministrationSave_Click(object sender, EventArgs e)
        {
            try
            {

                chckIsActive.EditValue = true;
                getAdministrationData = new Administration()
                {
                    Id = _id,
                    Username = txtAdministrationName.Text,
                    Password = txtAdministrationPassword.Text,
                };

                SaveAdministration(getAdministrationData);
                FillGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void grdAdministration_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (grdAdministration.CurrentRow.Index != -1)
                {
                    _adminId = Convert.ToInt16(grdAdministration.CurrentRow.Cells[0].Value.ToString());
                    txtAdministrationName.Text = grdAdministration.CurrentRow.Cells[1].Value.ToString();
                    txtAdministrationPassword.Text = grdAdministration.CurrentRow.Cells[2].Value.ToString();

                    btnAdministrationDelete.Enabled = true;
                    

                }
            }
            catch
            {
                MessageBox.Show("Bir Id değeri seçmelisiniz!");
            }
        }

        private void btnAdministrationDelete_Click(object sender, EventArgs e)
        {
            try
            {
                getAdministrationData = new Administration()
                {
                    Id = _adminId
                };

                DeleteAdministration(getAdministrationData);


                FillGrid();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
           
        }

        private bool Login(string _username, string _password)
        {

            getAdministrationBusiness = new AdministrationBusinessLayer();

            var userList = getAdministrationBusiness.GetAll();
            bool alert = false;
            foreach (var item in userList)
            {
                
                    if (item.Username == _username && item.Password == _password)
                    {
                    alert = true;
                    
                }

            }

            if (!alert)
                MessageBox.Show("Hatalı Veri Bilgilerinizi Kontrol Ediniz!");
            return alert;
        }

        private void btnCheckPassword_Click(object sender, EventArgs e)
        {
            string username = txtCheckUserName.Text.ToString();
            string password = txtCheckPassword.Text.ToString();

             bool returnCheck = Login(username, password);

            if (returnCheck)
                grpCheckAdminPassAndName.Hide();

        }


       
        

        private void btnfillOverDate_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            grpCheckAdminPassAndName.Visible = true;
            CleanTextbox();
        }

        private void btnMainPageLock_Click(object sender, EventArgs e)
        {
            CleanTextbox();
      
        }



        private void btnMainUnlockGrp_Click(object sender, EventArgs e)
        {
          
          
         
        }

        private void btnMailSettingsSave_Click(object sender, EventArgs e)
        {
            try
            {
                Mail data = new Mail()
                {
                    Id = Convert.ToInt16(lblMailId.Text),
                    From = txtMailAdress.Text.ToString(),
                    Password = txtMailPassword.Text.ToString(),
                    Port = Convert.ToInt32(txtMailSmtpPort.Text.ToString()),
                    Subject = txtMailSubject.Text.ToString(),
                    Body = txtMailContent.Text.ToString(),
                    ToAdd = txtMailRecever.Text.ToString(),
                    SmtpServer = txtSmtpServer.Text.ToString(),
                    ShopName =txtCompanyName.Text.ToString()
                };
                mailSave(data);
            }
            catch
            {
                MessageBox.Show("Hatralı Veri Girişi Lütfen Bilgilerinizi kontrol Ediniz!");
            }

        }


        private void mailSave(Mail model)
        {
            try
            {
                getSendMailBusiness = new SendMailBusinessLayer();
                var MailDataList = getSendMailBusiness.GetAll();


                if (model.Id == 0)
                {
                    getSendMailBusiness = new SendMailBusinessLayer();
                     getSendMailBusiness.Insert(model);
                }
                #region update gerek kalmadı!
                else
                {
                    getSendMailBusiness = new SendMailBusinessLayer();
                    getSendMailBusiness.Update(model);
                }
                #endregion

                FillGrid();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            
        }

       

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                Mail data = new Mail()
                {
                    From = txtMailAdress.Text.ToString(),
                    Password = txtMailPassword.Text.ToString(),
                    Port = Convert.ToInt32(txtMailSmtpPort.Text.ToString()),
                    Subject = txtMailSubject.Text.ToString(),
                    Body = txtMailContent.Text.ToString(),
                    ToAdd = txtMailRecever.Text.ToString(),
                    SmtpServer = txtSmtpServer.Text.ToString(),
                    ShopName = txtCompanyName.Text.ToString()
                };


                getSendMailBusiness.MailsendManuel(data);
            }
            catch
            {
                MessageBox.Show("Giriş Değerleri Hatalı Bilgilerinizi kontrol Edip Tekrar Giriniz!");
            }
        }
        private void FillMailInformation()
        {          
                getSendMailBusiness = new SendMailBusinessLayer();
                var data = getSendMailBusiness.GetAll();

            bool loopBreak = false;
            
                foreach (var item in data)
                {
                    if ((!string.IsNullOrWhiteSpace(item.Password)) && (!string.IsNullOrWhiteSpace(item.From) && (!string.IsNullOrWhiteSpace(item.Port.ToString()))) && (!string.IsNullOrWhiteSpace(item.SmtpServer)) && (!string.IsNullOrWhiteSpace(item.Body)))
                    {
                    lblMailId.Text = item.Id.ToString();
                        txtMailAdress.Text = item.From.ToString();
                        txtMailPassword.Text = item.Password;
                        txtMailSmtpPort.Text = item.Port.ToString();
                        txtMailSubject.Text = item.Subject;
                        txtMailContent.Text = item.Body;
                        txtMailRecever.Text = item.ToAdd;
                        txtSmtpServer.Text = item.SmtpServer;
                        txtCompanyName.Text = item.ShopName;
                      loopBreak = true;
                    }
                    if (loopBreak)
                        return;
                
                }    
        }

        private void DayOverSendMail()
        {
            try
            {
                getBusiness = new CustomerBusinessLayer();

                var list = getBusiness.GetAll();
                List<Customer> passPaymentDateList = new List<Customer>();
                List<Customer> _CustomerGridList = new List<Customer>();
                getSendMailBusiness = new SendMailBusinessLayer();


                //Mail data = new Mail()
                //{
                //    From = txtMailAdress.Text.ToString(),
                //    Password = txtMailPassword.Text.ToString(),
                //    Port = Convert.ToInt32(txtMailSmtpPort.Text.ToString()),
                //    Subject = txtMailSubject.Text.ToString(),
                //    Body = txtMailContent.Text.ToString(),
                //    ToAdd = txtMailRecever.Text.ToString(),
                //    SmtpServer = txtSmtpServer.Text.ToString(),
                //    ShopName = txtCompanyName.Text.ToString()
                //};

                getBusiness = new CustomerBusinessLayer();

                var customerList = getBusiness.GetAll();

                var mailInformation = getSendMailBusiness.GetAll();

                Mail mailData = null;
                foreach (var item in mailInformation)
                {
                    mailData = new Mail()
                    {
                        Body = item.Body,
                        From = item.From,
                        Password = item.Password,
                        ShopName = item.ShopName,
                        SmtpServer = item.SmtpServer,
                        Port = item.Port,
                        Subject = item.Subject
                    };
                }


                foreach (var item in customerList)
                {
                    // Aslında Aşağıdaki 2. Kod satırı kullanıcı insafiyetine bırakılabilir!!
                    if (item.PassPaymentDate < DateTime.Now && item.LastSendMailDate.AddDays(+7) < DateTime.Now)
                    {
                        getSendMailBusiness.MailsendCustomer(mailData, item);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Günü geçenlere otomatik mail gönderme hatası UI " + ex.Message);
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            string gridName = "gridView1";
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            DoRowDoubleClick(view, pt, gridName);
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            string gridName = "gridView2";
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            DoRowDoubleClick(view, pt, gridName);
        }

        private void DoRowDoubleClick(GridView view, Point pt, string GridName)
        {
            GridHitInfo info = view.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {
                //string colCaption = info.Column == null ? "N/A" : info.Column.GetCaption();
                //DialogResult dialog = MessageBox.Show("Seçili satır silinsin mi?", "Sil", MessageBoxButtons.YesNoCancel);

                #region GriView1
                if ( GridName == "gridView3")
                {
                    var row = gridView3.FocusedRowHandle;
                    var obj = gridView3.GetFocusedRow() as Customer;
                    if (obj != null)
                    {
                        try
                        {
                            //getData = new Customer()
                            //{
                            //    Id = obj.Id
                            //};

                            //Delete(getData);

                            try
                            {
                                
                                    _id = Convert.ToInt16(obj.Id.ToString());
                                    txtName.Text = obj.Name.ToString();
                                    txtSurname.Text = obj.Surname.ToString();
                                    txtPhoneNumber.Text = obj.PhoneNumber.ToString();
                                    dtpPaymentDate.EditValue = Convert.ToDateTime(obj.PaymentDate.ToString());
                                    cmbMounth.Text = (Convert.ToInt16(obj.Mount.ToString())).ToString();
                                    txtPriceAmount.Text = obj.TotalPayment.ToString();
                                    dtpLastPaymentDay.EditValue = Convert.ToDateTime(obj.PassPaymentDate.ToString());
                                txtMailAddress.Text = obj.MailAddress.ToString();
                                chckIsActive.EditValue = Convert.ToBoolean(obj.MemberRemove);

                                
                            }
                            catch
                            {
                                MessageBox.Show("Bir Id değeri seçmelisiniz!");
                            }


                            FillGrid();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message.ToString());
                        }

                    }
                }
                #endregion
                #region GriView4
                if ( GridName == "gridView4")
                {
                    var row = gridView4.FocusedRowHandle;
                    var obj = gridView4.GetFocusedRow() as Customer;
                    if (obj != null)
                    {
                        try
                        {
                     

                            try
                            {
                                
                                    _id = Convert.ToInt16(obj.Id.ToString());
                                    txtName.Text = obj.Name.ToString();
                                    txtSurname.Text = obj.Surname.ToString();
                                    txtPhoneNumber.Text = obj.PhoneNumber.ToString();
                                    dtpPaymentDate.EditValue = Convert.ToDateTime(obj.PaymentDate.ToString());
                                    cmbMounth.Text = (Convert.ToInt16(obj.Mount.ToString())).ToString();
                                    txtPriceAmount.Text = obj.TotalPayment.ToString();
                                    dtpLastPaymentDay.EditValue = Convert.ToDateTime(obj.PassPaymentDate.ToString());
                                chckIsActive.EditValue = Convert.ToBoolean(obj.MemberRemove);
                                txtMailAddress.Text = obj.MailAddress.ToString();
                               
                            }
                            catch
                            {
                                MessageBox.Show("Bir Id değeri seçmelisiniz!");
                            }

                            FillGrid();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message.ToString());
                        }

                    }
                }
                #endregion

            }
            else
                MessageBox.Show("Bir satır seçmelisiniz!");

        }


        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
                e.Appearance.BackColor = Color.LightGreen;

            #region grid color other ödemesi geçenler
            // 
            // string ProcessStatus = view.GetRowCellDisplayText(e.RowHandle, view.Columns["ColumnbaşlıkGelicek"])
            //if (ProcessStatus == "Cancel") ;  // tarih geçtiyse
            //e.Appearance.BackColor = Color.Salmon;
            #endregion
        }

        private void gridView2_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
                e.Appearance.BackColor = Color.Salmon;
        }

        private void gridView3_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
                e.Appearance.BackColor = Color.LightGreen;
        }

        private void gridView4_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
                e.Appearance.BackColor = Color.Salmon;
        }

        private void gridView3_DoubleClick(object sender, EventArgs e)
        {
            // active customer
            string gridName = "gridView3";
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            DoRowDoubleClick(view, pt, gridName);
        }

        private void gridView4_DoubleClick(object sender, EventArgs e)
        {
            // pass payment date
            string gridName = "gridView4";
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            DoRowDoubleClick(view, pt, gridName);
        }


      

        private void gridView3_RowUpdated_1(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var obj = e.Row as Customer;
            getBusiness = new CustomerBusinessLayer();

            try
            {
                if (obj != null)
                    getBusiness.Update(obj);
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Güncelleme sırasında hata oluştu tekrar deneyiniz!");
            }
            finally
            {
                FillGrid();
            }
        }

        private void gridView3_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            DevExpress.XtraGrid.Columns.GridColumn column = e.Column;
            GridView view = sender as GridView;

            if (column.Name == "btndeleteActiveCustomer")
            {

                object _name = view.GetRowCellValue(e.RowHandle, "Name");
                DialogResult dialog = XtraMessageBox.Show($"{_name} Seçili satır silinsin mi*", "Sil", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    try
                    {
                        object _Id = view.GetRowCellValue(e.RowHandle, "Id");
                       

                        Customer deleteModel = new Customer() { Id = Convert.ToInt32(_Id.ToString()) };

                        getBusiness = new CustomerBusinessLayer();
                        getBusiness.Delete(deleteModel);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("Veri silinemedi tekrar deneyiniz!");
                    }
                    finally
                    {
                        FillGrid();
                    }


                }
            }
        }

        private void gridView4_RowUpdated_1(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var obj = e.Row as Customer;
            getBusiness = new CustomerBusinessLayer();

            try
            {
                if (obj != null)
                    getBusiness.Update(obj);
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Güncelleme sırasında hata oluştu tekrar deneyiniz!");
            }
            finally
            {
                FillGrid();
            }
        }

        private void gridView4_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            DevExpress.XtraGrid.Columns.GridColumn column = e.Column;
            GridView view = sender as GridView;

            if (column.Name == "btnDeletePassDay")
            {
                object _name = view.GetRowCellValue(e.RowHandle, "Name");
                DialogResult dialog = XtraMessageBox.Show($"{_name} Seçili satır silinsin mi*", "Sil", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    try
                    {
                        object _Id = view.GetRowCellValue(e.RowHandle, "Id");

                        Customer deleteModel = new Customer() { Id = Convert.ToInt32(_Id.ToString()) };

                        getBusiness = new CustomerBusinessLayer();
                        getBusiness.Delete(deleteModel);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("Veri silinemedi tekrar deneyiniz!");
                    }
                    finally
                    {
                        FillGrid();
                    }


                }
            }
        }
    }
}
