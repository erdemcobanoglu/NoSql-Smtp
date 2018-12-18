using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BusinessLogicLayer;
using EntitiesLayer;

namespace WinUı
{
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        AdministrationBusinessLayer getBusiness;
        Administration getAdministrationData;

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.ToString();
            string password = txtPassword.Text.ToString();

            Login(username, password);
        }

        private void Login(string _username, string _password)
        {
           
                getBusiness = new AdministrationBusinessLayer();

                var userList = getBusiness.GetAll();
            bool alert = false;
            foreach (var item in userList)
             {

                if (item.Username == _username && item.Password == _password)
                {
                    alert = true;
                }
                        
             }
            if (alert)
            {
                CustomerListForm openCustomer = new CustomerListForm();
                openCustomer.Show(); this.Hide();
            }


            if (!alert)
                MessageBox.Show("Hatalı Veri Bilgilerinizi Kontrol Ediniz!");

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            Firstlogin();
            //IsMdiContainer = true;
            txtPassword.Properties.PasswordChar =  '*';
        }

        private void Firstlogin()
        {
            try
            {
                getAdministrationData = new Administration()
                {
                    Username = "admin",
                    Password = "admin"
                };

                SaveAdministration(getAdministrationData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void SaveAdministration(Administration model)
        {
            try
            {
                getBusiness = new AdministrationBusinessLayer();
                var list = getBusiness.GetAll();
                int counter = list.Count();

                if (counter < 1)
                {
                    getBusiness = new AdministrationBusinessLayer();
                    getBusiness.Insert(model);
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}