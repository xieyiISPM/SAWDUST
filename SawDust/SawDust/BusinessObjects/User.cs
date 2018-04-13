using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SawDust.BusinessObjects
{
    public class User
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Password { get; set; }
        public long InsertEtime { get; set; }
    }
}
