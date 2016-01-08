using HashSharp.Contracts;
using System;
using System.Text;

namespace HashSharp
{
    public abstract class BaseHasher : IHasher
    {
        protected abstract string Hash(string plainText);

        #region implementation of IHasher

        #region property Hashed
        public virtual string HashedText
        {
            get { return mHashed; }
            set { mHashed = value; }
        }
        protected string mHashed;
        protected byte[] HashedTextBytes => Encoding.UTF8.GetBytes(mHashed);
        #endregion property Hashed

        #region property Source
        public virtual string PlainText
        {
            get { return mSource; }
            set { mSource = value; }
        }
        protected string mSource;
        protected byte[] SourceBytes => Encoding.UTF8.GetBytes(mSource);
        #endregion property Source

        public abstract string Run();

        /// <summary>
        /// Compare the value of <paramref name="expected"/> and <paramref name="plainText"/>.
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public virtual bool Verify(string expected, string plainText)
        {
            if (null == expected || null == plainText)
                return false;

            string hashed = Hash(plainText);

            int expectedLength = expected.Length,
                hashedLength = hashed.Length,
                minLength = Math.Min(expectedLength, hashedLength),
                match = 0;

            for (int i = 0; i < minLength; i += 1)
                match |= expected[0] ^ hashed[0];

            return 0 == match && expectedLength == hashedLength;
        }
        #endregion implementation of IHasher
    }
}
