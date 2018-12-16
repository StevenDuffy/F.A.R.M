using System;
using FarmControl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JustRipeFarm.UnitTests
{
    [TestClass]
    public class dbTests
    {
        [TestMethod]
        public void DbConnectionOpens()
        {
            DatabaseConnection.DataConn.Open();
            Assert.AreEqual(DatabaseConnection.DataConn.isOpen, true);
        }

        [TestMethod]
        public void DbConnectionCloses()
        {
            DatabaseConnection.DataConn.Open();
            Assert.AreEqual(DatabaseConnection.DataConn.isOpen, true);
            DatabaseConnection.DataConn.Close();
            Assert.AreEqual(DatabaseConnection.DataConn.isOpen, false);
        }
    }
}
