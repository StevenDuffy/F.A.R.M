using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FarmControl;

namespace JustRipeFarm.UnitTests
{
    [TestClass]
    public class LoginVerifierTests
    {
        [TestMethod]
        public void GetPasswordHashValue_PasswordMatch_ReturnsTrue()
        {
            string[] hashes = new string[] {
            "185f8db32271fe25f561a6fc938b2e264306ec304eda518007d1764826381969",
            "33b93476cf597a3330653b66a658983d892ac264b5d6029a2dc642b9b1f30870",
            "c67fc8b89a652cf6abf258f08ea0269132422c17fb80f7c47cbe05dfa18e643c"
            };
            string[] nonHashes = new string[] { "Hello", "Time", "Dumbledore" };

            for (int i = 0; i < hashes.Length; i++)
            {
                Assert.IsTrue(LoginVerifier.Verifier.GetPasswordHashValue(nonHashes[i]) == hashes[i]);
            }
        }

        [TestMethod]
        public void GetPasswordHashValue_PasswordNoMatch_ReturnsFalse()
        {
            string[] hashes = new string[] {
            "185f8db32271fe25f561a6fc938b2e264306ec304eda518007d1764826381969",
            "33b93476cf597a3330653b66a658983d892ac264b5d6029a2dc642b9b1f30870",
            "c67fc8b89a652cf6abf258f08ea0269132422c17fb80f7c47cbe05dfa18e643c"
            };
            string[] nonHashes = new string[] { "hello", "Timing", "Dumbledore  " };

            for (int i = 0; i < hashes.Length; i++)
            {
                Assert.IsFalse(LoginVerifier.Verifier.GetPasswordHashValue(nonHashes[i]) == hashes[i]);
            }
        }
    }
}

