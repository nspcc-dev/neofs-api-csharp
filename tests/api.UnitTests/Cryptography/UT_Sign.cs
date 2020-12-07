using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoFS.API.v2.Cryptography;
using System.Security.Cryptography;
using NeoFS.API.v2.Accounting;
using NeoFS.API.v2.Session;
using NeoFS.API.v2.Refs;
using Neo;

namespace NeoFS.API.v2.UnitTests.TestCryptography
{
    [TestClass]
    public class UT_Sign
    {
        [TestMethod]
        public void TestSignData1()
        {
            var key = "Kwk6k2eC3L3QuPvD8aiaNyoSXgQ2YL1bwS5CP1oKoA9waeAze97s".LoadWif();

            var sig = "024c7b7fb6c310fccf1ba33b082519d82964ea93868d676662d4a59ad548df0e7d".HexToBytes().SignData(key);
            Assert.IsTrue("024c7b7fb6c310fccf1ba33b082519d82964ea93868d676662d4a59ad548df0e7d".HexToBytes().VerifyData(sig, key));

            var key1 = key.PublicKey().LoadPublicKey();
            Assert.IsTrue("024c7b7fb6c310fccf1ba33b082519d82964ea93868d676662d4a59ad548df0e7d".HexToBytes().VerifyData(sig, key1));
        }

        [TestMethod]
        public void TestSignData2()
        {
            var key = "L4kWTNckyaWn2QdUrACCJR1qJNgFFGhTCy63ERk7ZK3NvBoXap6t".LoadWif();
            var sig = "01".HexToBytes().SignData(key);
            Assert.AreEqual("0429af7f4005b588180fbb03cee7c64b8db7d0fb74e81d5e9bbdb8e762e10dc38734ca1cd9b23d908566295ed4ffb2e9e2310b32bbb9cebc4d275f8329e674209e", sig.ToHexString());
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
