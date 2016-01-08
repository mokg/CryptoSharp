using System;
using System.Security.Cryptography;

namespace HashSharp.Util
{
    public static class Rand
    {
        public static byte[] Bytes(int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(Properties.UtilStrings.LengthMustBeAPositiveInteger);

            byte[] bytes = new byte[length];

            using (RNGCryptoServiceProvider rngcsp = new RNGCryptoServiceProvider())
            {
                rngcsp.GetBytes(bytes);
            }

            return bytes;
        }
    }
}
