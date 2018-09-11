
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SawDust.BusinessObjects;
using SawDust.DataAccess;

namespace UnitTestSawDust
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSqliteDaConstructor()
        {
            SqliteDataAccess da = new SqliteDataAccess();
            Assert.IsFalse(String.IsNullOrEmpty(da.dbFile));
            Assert.IsTrue(File.Exists(da.dbFile));
        }
        [TestMethod]
        public void TestSqliteDaAddUser()
        {
            SqliteDataAccess da = new SqliteDataAccess();
            Assert.IsFalse(String.IsNullOrEmpty(da.dbFile));
            Assert.IsTrue(File.Exists(da.dbFile));
            User user = new User() {
                ID = "testId"
                ,Name = "Carpenter Bob"
                ,Password = "what is it"
            };
            bool inserted = da.Add(user);
            Assert.IsTrue(inserted);
        }
        [TestMethod]
        public void TestSqliteDaGetUser()
        {
            SqliteDataAccess da = new SqliteDataAccess();
            Assert.IsFalse(String.IsNullOrEmpty(da.dbFile));
            Assert.IsTrue(File.Exists(da.dbFile));
            User user = new User()
            {
                ID = "testId",                
                Name = "Carpenter Bob",
                Password = "what is it"
            };
            bool inserted = da.Add(user);
            Assert.IsTrue(inserted);

            Assert.AreEqual(1, da.getAllUsers().Count);
        }
        [TestMethod]
        public void TestSqliteDaDeleteUser()
        {
            SqliteDataAccess da = new SqliteDataAccess();
            Assert.IsFalse(String.IsNullOrEmpty(da.dbFile));
            Assert.IsTrue(File.Exists(da.dbFile));
            User user = new User()
            {
                ID = "testId",
                Name = "Carpenter Bob",
                Password = "what is it"
            };
            bool inserted = da.Add(user);
            Assert.IsTrue(inserted);

            Assert.AreEqual(1, da.getAllUsers().Count);
            da.Delete(user);
            Assert.AreEqual(0, da.getAllUsers().Count);
        }

        [TestMethod]
        public void TestSqliteDaUpdateUser()
        {
            SqliteDataAccess da = new SqliteDataAccess();
            Assert.IsFalse(String.IsNullOrEmpty(da.dbFile));
            Assert.IsTrue(File.Exists(da.dbFile));
            User user = new User()
            {
                ID = "testId",
                Name = "Carpenter Bob",
                Password = "what is it"
            };
            bool inserted = da.Add(user);
            Assert.IsTrue(inserted);
            user.Password = "now I remember";
            inserted = da.Update(user);
            Assert.IsTrue(inserted);
            user = da.getUserById(user.ID);
            Assert.AreEqual("now I remember", user.Password);
        }
        #region jobs

        [TestMethod]
        public void TestAddJob()
        {
            SqliteDataAccess da = new SqliteDataAccess();
            Assert.IsFalse(String.IsNullOrEmpty(da.dbFile));
            Assert.IsTrue(File.Exists(da.dbFile));
            clearAllJobs();
            Job job = new Job()
            {                
                JobName = "job1",
                JobDescription = "the first job"
            };
            bool inserted = da.Add(job);
            Assert.IsTrue(inserted);
        }

        [TestMethod]
        public void TestGetAllJobs()
        {
            SqliteDataAccess da = new SqliteDataAccess();
            Assert.IsNotNull(da);
            clearAllJobs();
            TestAddJob();
            List<Job> jobs = da.GetAllJobs();
            Assert.IsTrue(jobs.Count > 0);
            Console.WriteLine("total jobs: " + jobs.Count);
        }

        [TestMethod]
        public void TestGetAllJobsByClientID()
        {
            SqliteDataAccess da = new SqliteDataAccess();
            Assert.IsNotNull(da);
            clearAllJobs();
            TestAddJob();

            List<Job> jobs = da.GetAllJobsByClient(new Client { ID = 1L });
            Assert.IsTrue(jobs.Count == 0);
            Job job = new Job { ClientId = 1L, JobName = "client1job", JobDescription = "client1's job" };
            da.Add(job);
            jobs = da.GetAllJobsByClient(new Client { ID = 1L });
            Assert.IsTrue(jobs.Count == 1);
        }

        [TestMethod]
        public void TestUpdateJob()
        {
            SqliteDataAccess da = new SqliteDataAccess();
            Assert.IsNotNull(da);

            clearAllJobs();
            TestAddJob();
            List<Job> jobs = da.GetAllJobs();


            Assert.IsTrue(jobs.Count == 1);
            jobs[0].JobName = "updated";
            jobs[0].JobDescription = "updated";
            jobs[0].MarkupPct = 90210;
            jobs[0].SalesTax = 90210.0;
            da.Update(jobs[0]);

            jobs = da.GetAllJobs();
            Assert.IsTrue(jobs.Count == 1);
            Assert.IsTrue(jobs[0].JobName == "updated");
            Assert.IsTrue(jobs[0].JobDescription == "updated");
            Assert.IsTrue(jobs[0].MarkupPct == 90210);
            Assert.IsTrue(jobs[0].SalesTax == 90210.0);
        }

        private void clearAllJobs()
        {
            SqliteDataAccess da = new SqliteDataAccess();
            Assert.IsNotNull(da);

            List<Job> jobs = da.GetAllJobs();
            Assert.IsNotNull(jobs);
            foreach (Job job in jobs) { da.Delete(job); }

        }
        #endregion
    }
}
