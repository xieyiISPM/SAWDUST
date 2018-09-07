using System;
using System.Collections.Generic;
using System.Data;
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
            using (SqliteConnection db = new SqliteConnection("Filename=" + dbFile))
            {
                db.Open();

                try
                {
                    SqliteCommand command = new SqliteCommand("INSERT into USERS (UserName, UserID, Password, InsertEtime) values (@param1, @param2, @param3, @param4)", db);
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
            bool retVal = true;
            using (SqliteConnection db = new SqliteConnection("Filename=" + dbFile))
            {
                db.Open();
                try
                {
                    SqliteCommand command = new SqliteCommand("INSERT into CLIENTS (CompanyName, ContactName, PhoneNumber, InsertEtime, Status, Email) values (@param1, @param2, @param3, @param4, @param5, @param6)", db);
                    command.Parameters.Add(new SqliteParameter("@param1", (null == client.ClientCompanyName) ? "" : client.ClientCompanyName));
                    command.Parameters.Add(new SqliteParameter("@param2", (null == client.ClientContactName) ? "" : client.ClientContactName));
                    command.Parameters.Add(new SqliteParameter("@param3", (null == client.ClientContactPhone) ? "" : client.ClientContactPhone));
                    command.Parameters.Add(new SqliteParameter("@param4", DateTimeOffset.Now.ToUnixTimeSeconds()));
                    command.Parameters.Add(new SqliteParameter("@param5", (null == client.Status) ? "" : client.Status));
                    command.Parameters.Add(new SqliteParameter("@param6", (null == client.ClientContactEMail) ? "" : client.ClientContactEMail));
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    retVal = false;
                }
            }
            return retVal;
        }

        public User getUserById(string iD)
        {
            User user = new User();
            using (SqliteConnection db = new SqliteConnection("Filename=" + dbFile))
            {
                db.Open();

                try
                {
                    SqliteCommand command = new SqliteCommand("select * from USERS where UserID=@param1", db);
                    command.Parameters.Add(new SqliteParameter("@param1", iD));
                    IDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        user = GetUserFromDataReader(dataReader);                        
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return user;
        }
        private User GetUserFromDataReader(IDataReader dataReader)
        {
            User user = new User();
            user.ID = (string)dataReader["UserID"];
            user.Name = (string)dataReader["UserName"];
            user.Password = (string)dataReader["Password"];
            // user.Type = (string)dataReader["UserType"];
            user.InsertEtime = (long)dataReader["InsertEtime"];
            return user;
        }


        #region jobs
        public bool Add(Job job)
        {
            bool retVal = true;
            using (SqliteConnection db = new SqliteConnection("Filename=" + dbFile))
            {
                db.Open();
                try
                {

                    SqliteCommand command = new SqliteCommand("INSERT into JOBS (ClientID, JobName, JobDescription, SalesTax, DefaultHeight, InsertEtime, MarkupPct, ClientName ) values (@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8)", db);
                    command.Parameters.Add(new SqliteParameter("@param1", job.ClientId));
                    command.Parameters.Add(new SqliteParameter("@param2", (null == job.JobName) ? "" : job.JobName));
                    command.Parameters.Add(new SqliteParameter("@param3", (null == job.JobDescription) ? "" : job.JobDescription));
                    command.Parameters.Add(new SqliteParameter("@param4", job.SalesTax));
                    command.Parameters.Add(new SqliteParameter("@param5", job.DefaultHeight));
                    command.Parameters.Add(new SqliteParameter("@param6", DateTimeOffset.Now.ToUnixTimeSeconds()));
                    command.Parameters.Add(new SqliteParameter("@param7", job.MarkupPct));
                    command.Parameters.Add(new SqliteParameter("@param8", job.ClientName));
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    retVal = false;
                }
            }
            return retVal;
        }

        public bool Delete(Job job)
        {
            bool retVal = true;
            using (SqliteConnection db = new SqliteConnection("Filename=" + dbFile))
            {
                db.Open();

                try
                {
                    SqliteCommand command = new SqliteCommand("DELETE from JOBS where JobID = @param1", db);
                    command.Parameters.Add(new SqliteParameter("@param1", job.ID));
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    retVal = false;
                }
            }
            return retVal;
        }
        public List<Job> GetAllJobs()
        {
            List<Job> jobs = new List<Job>();
            using (SqliteConnection db = new SqliteConnection("Filename=" + dbFile))
            {
                db.Open();

                try
                {
                    SqliteCommand command = new SqliteCommand("select * from JOBS", db);
                    IDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        /*
                         *  tableCommand = "CREATE TABLE IF NOT EXISTS CREATE TABLE JOBS 
                         *  (	JobID	INTEGER PRIMARY KEY AUTOINCREMENT, 	
                         *  ClientID	INTEGER, 
                         *  JobName	TEXT" +
                        ",	JobDescription	TEXT,	
                            SalesTax	REAL NOT NULL DEFAULT 6,	
                            DefaultHeight	REAL,	
                            InsertEtime	INTEGER,	
                            MarkupPct	REAL NOT NULL DEFAULT 30)";

                         */
                        Job job = new Job();
                        jobs.Add(job);
                        var id = dataReader["JobID"];
                        var clientID = dataReader["ClientID"];
                        var jobName = dataReader["JobName"];
                        var jobDesc = dataReader["JobDescription"];
                        var salesTax = dataReader["SalesTax"];
                        var defaultHeight = dataReader["DefaultHeight"];
                        var insertEtime = dataReader["InsertEtime"];
                        var markupPct = dataReader["MarkupPct"];
                        var clientName = dataReader["ClientName"];

                        job.ID = DBNull.Value == id ? -1L : (long)id;
                        job.ClientId = DBNull.Value == clientID ? -1L : (long)clientID;
                        job.JobName = DBNull.Value == jobName ? "" : jobName as string; 
                        job.JobDescription = DBNull.Value == jobDesc ? "" : jobDesc as string; 
                        job.SalesTax = DBNull.Value == salesTax ? 0.0 : (double)salesTax; 
                        job.DefaultHeight = DBNull.Value == defaultHeight ? 0.0 : (double)defaultHeight; 
                        job.InsertEtime = DBNull.Value == insertEtime ? 0L : (long)insertEtime; 
                        job.MarkupPct = DBNull.Value == markupPct ? 0.0 : (double)markupPct;
                        job.ClientName = DBNull.Value == clientName ? null : clientName as string;

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return jobs;
            }
        }

        public List<Job> GetAllJobsByClient(Client client)
        {
            List<Job> jobs = new List<Job>();
            using (SqliteConnection db = new SqliteConnection("Filename=" + dbFile))
            {
                db.Open();

                try
                {
                    SqliteCommand command = new SqliteCommand("select * from JOBS where ClientID = @param", db);
                    command.Parameters.Add(new SqliteParameter("@param", client.ID));
                    IDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                       
                        Job job = new Job();
                        jobs.Add(job);
                        var id = dataReader["JobID"];
                        var clientID = dataReader["ClientID"];
                        var jobName = dataReader["JobName"];
                        var jobDesc = dataReader["JobDescription"];
                        var salesTax = dataReader["SalesTax"];
                        var defaultHeight = dataReader["DefaultHeight"];
                        var insertEtime = dataReader["InsertEtime"];
                        var markupPct = dataReader["MarkupPct"];

                        job.ID = DBNull.Value == id ? -1L : (long)id;
                        job.ClientId = DBNull.Value == clientID ? -1L : (long)clientID;
                        job.JobName = DBNull.Value == jobName ? "" : jobName as string;
                        job.JobDescription = DBNull.Value == jobDesc ? "" : jobDesc as string;
                        job.SalesTax = DBNull.Value == salesTax ? 0.0 : (double)salesTax;
                        job.DefaultHeight = DBNull.Value == defaultHeight ? 0.0 : (double)defaultHeight;
                        job.InsertEtime = DBNull.Value == insertEtime ? 0L : (long)insertEtime;
                        job.MarkupPct = DBNull.Value == markupPct ? 0.0 : (double)markupPct;

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return jobs;
            }
        }
        public bool Update(Job job)
        {
            bool retVal = true;
            using (SqliteConnection db = new SqliteConnection("Filename=" + dbFile))
            {
                db.Open();

                try
                {
                    SqliteCommand command = new SqliteCommand("UPDATE JOBS set JobName = @param1, JobDescription = @param2 " +
                        ", ClientID = @param4, SalesTax = @param5, DefaultHeight = @param6, MarkupPct = @param7 where JobID = @param3", db);
                    command.Parameters.Add(new SqliteParameter("@param1", job?.JobName));
                    command.Parameters.Add(new SqliteParameter("@param2", job?.JobDescription));
                    command.Parameters.Add(new SqliteParameter("@param3", job?.ID));
                    command.Parameters.Add(new SqliteParameter("@param4", job?.ClientId));
                    command.Parameters.Add(new SqliteParameter("@param5", job?.SalesTax));
                    command.Parameters.Add(new SqliteParameter("@param6", job?.DefaultHeight));
                    command.Parameters.Add(new SqliteParameter("@param7", job?.MarkupPct));
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    retVal = false;
                }
            }
            return retVal;
        }

        #endregion

        public bool Delete(User user)
        {
            bool retVal = true;
            using (SqliteConnection db = new SqliteConnection("Filename=" + dbFile))
            {
                db.Open();

                try
                {
                    SqliteCommand command = new SqliteCommand("DELETE from USERS where UserID = @param1", db);
                    command.Parameters.Add(new SqliteParameter("@param1", user.ID));
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    retVal = false;
                }
            }
            return retVal;
        }

        public bool Delete(Client client)
        {
            bool retVal = true;
            using (SqliteConnection db = new SqliteConnection("Filename=" + dbFile))
            {
                db.Open();

                try
                {
                    SqliteCommand command = new SqliteCommand("DELETE from CLIENTS where ClientID = @param1", db);
                    command.Parameters.Add(new SqliteParameter("@param1", client.ID));
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    retVal = false;
                }
            }
            return retVal;
        }

        
        public List<Client> GetAllClients()
        {
            List<Client> clients = new List<Client>();
            using (SqliteConnection db = new SqliteConnection("Filename=" + dbFile))
            {
                db.Open();

                try
                {
                    SqliteCommand command = new SqliteCommand("select * from CLIENTS", db);                    
                    IDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Client client = new Client();
                        clients.Add(client);
                        var id = dataReader["ClientID"];
                        var contact = dataReader["ContactName"];
                        var comp = dataReader["CompanyName"];
                        var phone = dataReader["PhoneNumber"];
                        var email = dataReader["Email"];
                        var status = dataReader["Status"];
                        var insert = dataReader["InsertEtime"];

                        client.ID = DBNull.Value == id ? -1L : (long)id;
                        client.ClientContactName = DBNull.Value == contact ? "" : contact as string;
                        client.ClientCompanyName = DBNull.Value == comp ? "" : comp as string; ;
                        client.ClientContactPhone = DBNull.Value == phone ? "" : phone as string; ;
                        client.ClientContactEMail = DBNull.Value == email ? "" : email as string; ;
                        client.Status = DBNull.Value == status ? "" : status as string; ;
                        client.InsertEtime = DBNull.Value == insert ? 0L : (long) insert; ;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return clients;
            }
        }

        
        public List<User> getAllUsers()
        {
            List<User> users = new List<User>();
            using (SqliteConnection db = new SqliteConnection("Filename=" + dbFile))
            {
                db.Open();

                try
                {
                    SqliteCommand command = new SqliteCommand("select * from USERS", db);
                    IDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        User user = GetUserFromDataReader(dataReader);
                        users.Add(user);                        
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return users;
            }
        }

        public bool Update(User user)
        {
            bool retVal = true;
            using (SqliteConnection db = new SqliteConnection("Filename=" + dbFile))
            {
                db.Open();

                try
                {
                    SqliteCommand command = new SqliteCommand("UPDATE USERS set UserName = @param1, Password = @param2 where UserId = @param3", db);
                    command.Parameters.Add(new SqliteParameter("@param1", user.Name));
                    command.Parameters.Add(new SqliteParameter("@param2", user.Password));
                    command.Parameters.Add(new SqliteParameter("@param3", user.ID));                    
                    command.ExecuteNonQuery();


                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    retVal = false;
                }
            }
            return retVal;
        }

        public bool Update(Client client)
        {
            bool retVal = true;
            using (SqliteConnection db = new SqliteConnection("Filename=" + dbFile))
            {
                db.Open();
                try
                {
                    SqliteCommand command = new SqliteCommand("UPDATE CLIENTS set CompanyName = @param1, ContactName = @param2, PhoneNumber = @param3 where ClientID = @param4", db);
                    command.Parameters.Add(new SqliteParameter("@param1", client.ClientCompanyName));
                    command.Parameters.Add(new SqliteParameter("@param2", client.ClientContactName));
                    command.Parameters.Add(new SqliteParameter("@param3", client.ClientContactPhone));
                    command.Parameters.Add(new SqliteParameter("@param4", client.ID));
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    retVal = false;
                }
            }
            return retVal;
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
                    String tableCommand = "CREATE TABLE IF NOT EXISTS CLIENTS (ClientID	INTEGER PRIMARY KEY AUTOINCREMENT,	CompanyName	TEXT, " +
                    "ContactName	TEXT,	PhoneNumber	TEXT,	Email	TEXT ,	Status	TEXT,	InsertEtime	INTEGER ) ";
                    SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                    createTable.ExecuteReader();

                    // User table
                    tableCommand = "CREATE TABLE IF NOT EXISTS USERS (UserName	TEXT, " +
                    "UserID TEXT PRIMARY KEY NOT NULL UNIQUE, Password	TEXT, UserType TEXT,	InsertEtime	INTEGER ) ";
                    createTable = new SqliteCommand(tableCommand, db);
                    createTable.ExecuteReader();

                    // Jobs table
                    tableCommand = "CREATE TABLE IF NOT EXISTS JOBS (	JobID	INTEGER PRIMARY KEY AUTOINCREMENT, 	ClientID	INTEGER, ClientName TEXT, JobName	TEXT" +
                        ",	JobDescription	TEXT,	SalesTax	REAL NOT NULL DEFAULT 6,	DefaultHeight	REAL,	InsertEtime	INTEGER,	MarkupPct	REAL NOT NULL DEFAULT 30)";

                    createTable = new SqliteCommand(tableCommand, db);
                    createTable.ExecuteReader();

                    tableCommand = "CREATE TABLE IF NOT EXISTS VERSION (Version	REAL)";
                    createTable.ExecuteReader();

                    String versionSql = "Insert into VERSION (Version) values (" + _schemaVersion + ")";

                }
                catch (SqliteException e)
                {
                    //TODO: logging
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    db.Close();
                }
            }
        }
    }
}
