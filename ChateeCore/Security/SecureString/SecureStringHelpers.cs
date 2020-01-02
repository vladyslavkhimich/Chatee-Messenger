using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public static class SecureStringHelpers
    {
        public static string Unsecure(this SecureString secureString)
        {
            if (secureString == null)
                return string.Empty;
            var unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
        public static byte[] SecureStringToHash(SecureString secureString, int userID)
        {
            using (SHA256 hash = SHA256.Create())
            {
                string input = Unsecure(secureString) + userID.ToString();
                byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                return result;
            }
        }
    }
}
