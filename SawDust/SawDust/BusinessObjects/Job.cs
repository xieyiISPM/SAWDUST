using SawDust.DataAccess;
using SawDust.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SawDust.BusinessObjects
{
    public class Job
    {
        private long _id;
        public long ID = -1;
        public long ClientId { get; set; }
        public string ClientName { get; set; }
        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public double SalesTax { get; set; }
        public double DefaultHeight { get; set; }
        public double MarkupPct { get; set; }

        public long InsertEtime { get; set; }

       
    }
}
