using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SawDust.BusinessObjects
{
    class Client
    {
        public string ClientCompanyName { get; set; }
        public string ClientContactName { get; set; }
        public string ClientContactPhone { get; set; }
        public string ClientContactEMail { get; set; }
        public string Status { get; set; }
    }

    /*class User
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Password { get; set; }
    }*/

    class QJob
    {
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
