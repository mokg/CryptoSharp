using Xunit;

namespace HashSharp.Spec
{
    public class PBKDF2Spec
    {
        static readonly string mDefaultPassword = "password";

        PBKDF2 GetHasher()
        {
            PBKDF2 hasher = new PBKDF2
            {
                PlainText = mDefaultPassword
            };

            return hasher;
        }

        [Fact]
        public void Generate_Hash_With_Default_Config()
        {
            var hasher = GetHasher();

            hasher.Run();
            
            Assert.True(hasher.Verify(hasher.HashedText, mDefaultPassword));
        }

        [Fact]
        public void Should_Not_Rehash_Password_With_Correct_Salt()
        {
            var hasher = GetHasher();

            hasher.Run();

            Assert.False(hasher.NeedRehash(hasher.Salt));
        }

        [Fact]
        public void Should_Rehash_Password_With_Illegal_Salt()
        {
            var hasher = GetHasher();
            var randomSalt = "randomSalt";

            hasher.Run();

            Assert.True(hasher.NeedRehash(randomSalt));
        }

        [Fact]
        public void Should_Generate_New_Salt_If_Salt_Is_Not_Correct()
        {
            var hasher = GetHasher();
            var illegalSalt = "thisisnotlegal";

            hasher.Salt = illegalSalt;
            hasher.Run();

            Assert.NotEqual(hasher.Salt, illegalSalt);
        }
    }
}
