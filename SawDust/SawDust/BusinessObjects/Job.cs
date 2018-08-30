using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SawDust.BusinessObjects
{
    public class Job
    {
        public long ID = -1;
        public string ClientId { get; set; }
        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public double SalesTax { get; set; }
        public double DefaultHeight { get; set; }

        public long InsertEtime { get; set; }
        

/*	`JobId`	INTEGER PRIMARY KEY AUTOINCREMENT,
	`ClientID`	INTEGER,
	`JobName`	TEXT,
	`JobDescription`	TEXT,
	`SalesTax`	REAL NOT NULL DEFAULT 6,
	`DefalutHeight`	REAL,
	`InsertEtime`	INTEGER,
	`MarkupPct`	REAL NOT NULL DEFAULT 30
    */
);
    }
}
