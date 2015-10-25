using System;
using eUni.DataAccess.eUniDbContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eUni.DataBaseTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            EUniDbContext context = new EUniDbContext();
            context.SaveChanges();
        }
    }
}
