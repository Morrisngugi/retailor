using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public interface IDataEncryptorService
    {
        string Encrypt(string text);
        string Decrypt(string text);
    }
}
