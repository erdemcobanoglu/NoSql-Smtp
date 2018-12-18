using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer
{
    public class Customer
    {
        [DisplayName("Müşteri Numarası")]
        public int Id { get; set; }
        [DisplayName("Adı")]
        public string Name { get; set; }
        [DisplayName("Soyadı")]
        public string Surname { get; set; }
        [DisplayName("Telefon")]
        public string PhoneNumber { get; set; }
        [DisplayName("İlk Ödeme")]
        public DateTime PaymentDate { get; set; }
        [DisplayName("Kaç Ay")]
        public int Mount { get; set; }
        [DisplayName("Ödeme Tutarı")]
        public int TotalPayment { get; set; }
        [DisplayName("Aktif Mi?")]
        public bool MemberRemove { get; set; } 
        [DisplayName("Aidat Günü")]
        public DateTime PassPaymentDate { get; set; }
        [DisplayName("Müşteri Mail Adressi")]
        public string MailAddress { get; set; }
        [DisplayName("Mail Gönderilme Tarihi")]
        public DateTime LastSendMailDate { get; set; }

        public string searchText { get; set; }
        public string FullName { get; set; }
        //public string searchTextUserName { get; set; }
        //public string searchTextSurname { get; set; }


    }
}
