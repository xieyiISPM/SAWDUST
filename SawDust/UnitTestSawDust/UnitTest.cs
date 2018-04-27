
using System;
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
    }
}
