using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODTGed_Uploader
{
    class Cryptography
    {
        public static String randomString(int size)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            StringBuilder b = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                b.Append(ch);
            }

            return b.ToString();
        }

        public static string encryptRJ256(string target, string key, string iv)
        {
            var rijndael = new System.Security.Cryptography.RijndaelManaged()
            {
                Padding = System.Security.Cryptography.PaddingMode.Zeros,
                Mode = System.Security.Cryptography.CipherMode.CBC,
                KeySize = 256,
                BlockSize = 256
            };

            var bytesKey = Encoding.ASCII.GetBytes(key);
            var bytesIv = Encoding.ASCII.GetBytes(iv);

            var encryptor = rijndael.CreateEncryptor(bytesKey, bytesIv);

            var msEncrypt = new System.IO.MemoryStream();
            var csEncrypt = new System.Security.Cryptography.CryptoStream(msEncrypt, encryptor, System.Security.Cryptography.CryptoStreamMode.Write);

            var toEncrypt = Encoding.ASCII.GetBytes(target);

            csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
            csEncrypt.FlushFinalBlock();

            var encrypted = msEncrypt.ToArray();
            return Convert.ToBase64String(encrypted);
        }

        public static string decryptRJ256(string target, string key, string iv)
        {
            var rijndael = new System.Security.Cryptography.RijndaelManaged()
            {
                Padding = System.Security.Cryptography.PaddingMode.Zeros,
                Mode = System.Security.Cryptography.CipherMode.CBC,
                KeySize = 256,
                BlockSize = 256
            };

            var keyBytes = Encoding.ASCII.GetBytes(key);
            var ivBytes = Encoding.ASCII.GetBytes(iv);

            var decryptor = rijndael.CreateDecryptor(keyBytes, ivBytes);
            var toDecrypt = Convert.FromBase64String(target);
            var fromEncrypt = new byte[toDecrypt.Length];

            var msDecrypt = new System.IO.MemoryStream(toDecrypt);
            var csDecrypt = new System.Security.Cryptography.CryptoStream(msDecrypt, decryptor, System.Security.Cryptography.CryptoStreamMode.Read);
            csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);
            
            string data = Encoding.ASCII.GetString(fromEncrypt);
            data = data.Replace("\0", "");

            return data;
        }
    }
}
