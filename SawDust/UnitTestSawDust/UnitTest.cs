
using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}
