using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Security.Cryptography.SHA256;
using static System.Text.Encoding;

namespace Letter.Domain.Helpers
{
    public class HelperHashCode
    {
        public static string HashString(string str)
        {
            using (SHA256 sha256 = Create())
            {
                var hashBytes = sha256.ComputeHash(UTF8.GetBytes(str));
                var hash = BitConverter.ToString(hashBytes).Replace("-","").ToLower();
                return hash;
            }
        }
    }
}
