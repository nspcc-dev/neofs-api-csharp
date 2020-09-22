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
        public void TestSignData()
        {
            var key = "Kwk6k2eC3L3QuPvD8aiaNyoSXgQ2YL1bwS5CP1oKoA9waeAze97s".LoadWif();

            var sig = "024c7b7fb6c310fccf1ba33b082519d82964ea93868d676662d4a59ad548df0e7d".FromHex().SignData(key);
            Assert.IsTrue("024c7b7fb6c310fccf1ba33b082519d82964ea93868d676662d4a59ad548df0e7d".FromHex().VerifyData(sig, key));

            var key1 = key.PublicKey().LoadPublicKey();
            Assert.IsTrue("024c7b7fb6c310fccf1ba33b082519d82964ea93868d676662d4a59ad548df0e7d".FromHex().VerifyData(sig, key1));
        }

        [TestMethod]
        public void TestSignRequest()
        {
            var key = "Kwk6k2eC3L3QuPvD8aiaNyoSXgQ2YL1bwS5CP1oKoA9waeAze97s".LoadWif();
            var req = new BalanceResponse
            {
                Body = new BalanceResponse.Types.Body
                {
                    Balance = new Decimal
                    {
                        Value = 100
                    },
                }
            };
            req.MetaHeader = new ResponseMetaHeader()
            {
                Ttl = 1
            };
            req.SignResponse(key);

            Assert.IsTrue(req.VerifyResponse());
        }
    }
}