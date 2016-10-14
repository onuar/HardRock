using NUnit.Framework;
using System;

namespace HardRock.Tests.Authentication
{
    [TestFixture]
    public class AuthenticationTests
    {
        [Test]
        public void Test()
        {
            Assert.Throws<ArgumentNullException>(() => { throw new ArgumentNullException(); });
        }
    }
}