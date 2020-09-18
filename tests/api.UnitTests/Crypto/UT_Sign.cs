using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoFS.API.v2.Crypto;
using System.Security.Cryptography;
using NeoFS.API.v2.Accounting;
using NeoFS.API.v2.Session;
using NeoFS.API.v2.Refs;

namespace NeoFS.API.v2.UnitTests.Crypto
{
    [TestClass]
    public class UT_Sign
    {
        [TestMethod]
        public void TestSign()
        {
            var key = "Kwk6k2eC3L3QuPvD8aiaNyoSXgQ2YL1bwS5CP1oKoA9waeAze97s".LoadWif();
            var req = new BalanceRequest()
            {
                Body = new BalanceRequest.Types.Body
                {
                    OwnerId = key.ToOwnerID()
                }
            };
            req.MetaHeader = new RequestMetaHeader();
            req.MetaHeader.Ttl = 1;

            req.SignRequest(key);

            Assert.IsTrue(req.VerifyRequest());
        }
    }
}