using ExpertalSystem.Authorization;
using NUnit.Framework;

namespace ExpertalSystem.Test.Authorization
{

    public class HasherTest
    {
        [Test]
        public void Hasher_HashPassword_ReturnsHashedPasswordString()
        {
            const string passwordToHash = "test";

            var hashedPassword = Hasher.HashPassword(passwordToHash);

            Assert.AreSame(hashedPassword.GetType(), typeof(string));
        }
        [Test]
        public void Hasher_Encrypt_EncryptsPassword()
        {
            const string passwordToHash = "test";

            const string hashedPassword = "07M/NOmKOZz6iXk0wwW9rnOKzuyyRQaVIo+5CsQStzk9cxY0";

            var result = Hasher.Encrypt(passwordToHash, hashedPassword);

            Assert.IsTrue(result);
        }
    }
}
