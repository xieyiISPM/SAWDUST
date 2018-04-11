using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Data.Sqlite.Internal;

namespace SawDust.DataAccess
{
  
    public class SqliteDataAccess
    {
        public string databaseFileName = "data.db";
        public string dbFile;
        public SqliteDataAccess()
        {
            dbFile = AppGlobals.gLocalFolder.Path + Path.DirectorySeparatorChar + databaseFileName;
            if (!File.Exists(dbFile))
            {
                createDbFile();
            }   
        }
        private void createDbFile()
        {
            SqliteEngine.UseWinSqlite3(); //Configuring library to use SDK version of SQLite
            using (SqliteConnection db = new SqliteConnection("Filename="+dbFile))
            {
                db.Open();
                String tableCommand = "CREATE TABLE IF NOT EXISTS CLIENTS (ClientId	INTEGER PRIMARY KEY AUTOINCREMENT,	CompanyName	TEXT, " +
                    "ContactName	TEXT,	PhoneNumber	TEXT,	Email	TEXT,	Status	TEXT,	InsertEtime	INTEGER,	LastUpdatedEtime	INTEGER ) ";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                try
                {
                    createTable.ExecuteReader();
                }
                catch (SqliteException e)
                {
                    //Do nothing
                }
            }
        }
    }
}
