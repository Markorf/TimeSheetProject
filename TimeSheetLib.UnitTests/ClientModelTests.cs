using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeSheet.Shared.Models.Implementation;

namespace TimeSheet.Shared.Models.UnitTests
{
    [TestClass]
    public class ClientModelTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "Client name can't be null or empty string")]
        public void Client_InitWithNullClientName_ReturnsArgumentException()
        {
            Client client = new Client(null, null, "ZXC", "Futog", "21322");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "Client name can't be null or empty string")]
        public void Client_InitWithEmptyClientName_ReturnsArgumentException()
        {
            Client client = new Client("", null, "ZXC", "Futog", "21322");
        }

        [TestMethod]
        public void Client_InitWithNonEmptyClientName_ReturnsClientInstance()
        {
            Client client = new Client("Marko", null, "ZXC", "Futog", "21322");
            Assert.IsTrue(client != null);
        }
    }
}
