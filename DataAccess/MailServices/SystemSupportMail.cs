using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MailServices
{
    class SystemSupportMail:MasterMailServer
    {
        public SystemSupportMail() 
        {
            senderMail = "Contraseña maestra";
            password = "63Y7L4djJINAaR9U";
            host = "smtp-relay.sendinblue.com";
            port = 587;
            ssl = true;
            initializeSmtpClient();
        }
    }
}
