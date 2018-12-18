using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer
{
    public class Mail
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string ToAdd { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string SmtpServer { get; set; }
        public string ShopName { get; set; }
        public DateTime LastSendMailDate { get; set; }
    }
}
