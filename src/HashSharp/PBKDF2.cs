using HashSharp.Contracts;
using HashSharp.Util;
using System;
using System.Security.Cryptography;
using System.Text;

namespace HashSharp
{
    public class PBKDF2 : BaseHasher, ISaltedHasher
    {
        static readonly int SaltSize = 36;
        static readonly int HashIteration = 60000;
        static readonly int KeySize = 64;

        void EnsureSalt()
        {
            if (null != Salt && !NeedRehash(Salt))
                return;

            Salt = string.Format("${0}${1}${2}", SaltSize, HashIteration, Convert.ToBase64String(Rand.Bytes(SaltSize)));
        }

        #region implementation of BaseHasher
        protected override string Hash(string plainText)
        {
            string hashed;

            using (Rfc2898DeriveBytes hashAdapter = new Rfc2898DeriveBytes(plainText, SaltBytes, HashIteration))
                hashed = Convert.ToBase64String(hashAdapter.GetBytes(KeySize));

            return hashed;
        }
        #endregion implementation of BaseHasher

        #region implementation of IHasher
        public override string Run()
        {
            EnsureSalt();

            mHashed = Hash(PlainText);

            return mHashed;
        }
        #endregion implementation of IHasher

        #region implementation ISaltedHasher

        #region property Salt
        public string Salt
        {
            get { return mSalt; }
            set { mSalt = value; }
        }
        private string mSalt;
        private byte[] SaltBytes => Encoding.UTF8.GetBytes(mSalt);
        #endregion property Salt

        public bool NeedRehash(string oldSalt)
        {
            if (null == oldSalt)
                return true;

            string[] saltSplit = oldSalt.Split('$');

            return saltSplit.Length != 4
                || saltSplit[1] != SaltSize.ToString()
                || saltSplit[2] != HashIteration.ToString();
        }
        #endregion implementation ISaltedHasher
    }
}
