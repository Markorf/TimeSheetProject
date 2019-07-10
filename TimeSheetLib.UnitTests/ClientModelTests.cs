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
            Client client = new Client(null, "FOO", "ZXC", "Futog", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "Client name can't be null or empty string")]
        public void Client_InitWithEmptyClientName_ReturnsArgumentException()
        {
            Client client = new Client("", null, "ZXC", "Futog", null);
        }

        [TestMethod]
        public void Client_InitWithNonEmptyClientName_ReturnsClientInstance()
        {
            Client client = new Client("Marko", "", "ZXC", "Futog", Guid.NewGuid());
            Assert.IsTrue(client != null);
        }
    }
}
