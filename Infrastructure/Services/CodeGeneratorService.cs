using Core.Services;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services
{
    public class CodeGeneratorService : ICodeGeneratorService
    {
        //public string GenerateRandomString(int string_length)
        //{
        //    RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
        //    byte[] randomBytes = new byte[string_length];
        //    rngCryptoServiceProvider.GetBytes(randomBytes);
        //    return Convert.ToBase64String(randomBytes);

        //}

        public string GenerateRandomString(int length)
        {
            const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string s = "";
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                while (s.Length != length)
                {
                    byte[] oneByte = new byte[1];
                    provider.GetBytes(oneByte);
                    char character = (char)oneByte[0];
                    if (valid.Contains(character))
                    {
                        s += character;
                    }
                }
            }
            return s;
            //StringBuilder res = new StringBuilder();
            //using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            //{
            //    byte[] uintBuffer = new byte[sizeof(uint)];

            //    while (string_length-- > 0)
            //    {
            //        rng.GetBytes(uintBuffer);
            //        uint num = BitConverter.ToUInt32(uintBuffer, 0);
            //        res.Append(valid[(int)(num % (uint)valid.Length)]);
            //    }
            //}

            //return res.ToString();
        }
    }
}
