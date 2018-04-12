using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Data.Sqlite.Internal;
using SawDust.BusinessObjects;

namespace SawDust.DataAccess
{
  
    public class SqliteDataAccess : IDataAccess
    {        
        public string databaseFileName = "data.db";
        public string dbFile;
        private float _schemaVersion = 1.0f;

        public SqliteDataAccess()
        {
            dbFile = AppGlobals.gLocalFolder.Path + Path.DirectorySeparatorChar + databaseFileName;
            if (!File.Exists(dbFile))
            {
                CreateDbFile();
            }   
        }

        public bool Add(User user)
        {
            bool retVal = true;
            //UserName	TEXT, " +      "Password	TEXT, UserType TEXT,	InsertEtime
            String sql = String.Format("INSERT into USERS (UserName, ID, Password, InsertEtime) values (?,?,?,?)"
                , user.Name, user.ID, user.Password, DateTimeOffset.Now.ToUnixTimeSeconds());
            using (SqliteConnection db = new SqliteConnection("Filename=" + dbFile))
            {
                db.Open();

                try
                {
                    SqliteCommand command = new SqliteCommand("INSERT into USERS (UserName, ID, Password, InsertEtime) values (@param1, @param2, @param3, @param4)", db);
                    command.Parameters.Add(new SqliteParameter("@param1", user.Name));
                    command.Parameters.Add(new SqliteParameter("@param2", user.ID));
                    command.Parameters.Add(new SqliteParameter("@param3", user.Password));
                    command.Parameters.Add(new SqliteParameter("@param4", DateTimeOffset.Now.ToUnixTimeSeconds()));
                    command.ExecuteNonQuery();
                    

                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    retVal = false;
                }                
            }
            return retVal;
        }

        public bool Add(Client client)
        {
            throw new NotImplementedException();
        }

        public bool Add(Job job)
        {
            throw new NotImplementedException();
        }

        public bool Delete(User user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Client client)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Job job)
        {
            throw new NotImplementedException();
        }

        public List<Client> GetAllClients()
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllJobs()
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllJobsByClient(Client client)
        {
            throw new NotImplementedException();
        }

        public List<User> getAllUsers()
        {
            throw new NotImplementedException();
        }

        public bool Update(User user)
        {
            throw new NotImplementedException();
        }

        public bool Update(Client client)
        {
            throw new NotImplementedException();
        }

        public bool Update(Job job)
        {
            throw new NotImplementedException();
        }

        private void CreateDbFile()
        {
            SqliteEngine.UseWinSqlite3(); //Configuring library to use SDK version of SQLite
            using (SqliteConnection db = new SqliteConnection("Filename="+dbFile))
            {
                db.Open();

                try
                {
                    // Client table
                    String tableCommand = "CREATE TABLE IF NOT EXISTS CLIENTS (ClientId	INTEGER PRIMARY KEY AUTOINCREMENT,	CompanyName	TEXT, " +
                    "ContactName	TEXT,	PhoneNumber	TEXT,	Email	TEXT,	Status	TEXT,	InsertEtime	INTEGER ) ";
                    SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                    createTable.ExecuteReader();

                    // User table
                    tableCommand = "CREATE TABLE IF NOT EXISTS USERS (UserId	INTEGER PRIMARY KEY AUTOINCREMENT,	UserName	TEXT, " +
                    "ID TEXT, Password	TEXT, UserType TEXT,	InsertEtime	INTEGER ) ";
                    createTable = new SqliteCommand(tableCommand, db);
                    createTable.ExecuteReader();

                    // TODO: finish this once we know what columns need to be added
                    // Jobs table
                    tableCommand = "CREATE TABLE IF NOT EXISTS USERS (JobId	INTEGER PRIMARY KEY AUTOINCREMENT,	ClientId	INTEGER, " +
                    "Password	TEXT, UserType TEXT,	InsertEtime	INTEGER ) ";
                    createTable = new SqliteCommand(tableCommand, db);

                    tableCommand = "CREATE TABLE IF NOT EXISTS VERSION (Version	REAL)";
                    createTable.ExecuteReader();

                    String versionSql = "Insert into VERSION (Version) values (" + _schemaVersion + ")";

                }
                catch (SqliteException e)
                {
                    //Do nothing
                }
                finally
                {
                    db.Close();
                }
            }
        }
    }
}
