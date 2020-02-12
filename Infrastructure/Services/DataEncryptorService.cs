using Core.Models;
using Core.Services;
using Core.Services.Communications;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class DataEncryptorService : IDataEncryptorService
    {
        private readonly IOptions<AppConfig> _appConfig;
        public DataEncryptorService(IOptions<AppConfig> appConfig)
        {
            _appConfig = appConfig;
        }
        public string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            PemReader pr = new PemReader(
           (StreamReader)File.OpenText(_appConfig.Value.PublicKey)
            );
            RsaKeyParameters keys = (RsaKeyParameters)pr.ReadObject();


            // PKCS1 v1.5 paddings
            Pkcs1Encoding eng = new Pkcs1Encoding(new RsaEngine());

            // PKCS1 OAEP paddings
            //OaepEncoding eng = new OaepEncoding(new RsaEngine());
            //OaepEncoding eng = new OaepEncoding(new RsaEngine());
            eng.Init(true, keys);

            int length = plainTextBytes.Length;
            int blockSize = eng.GetInputBlockSize();
            List<byte> cipherTextBytes = new List<byte>();
            for (int chunkPosition = 0;
                chunkPosition < length;
                chunkPosition += blockSize)
            {
                int chunkSize = Math.Min(blockSize, length - chunkPosition);
                cipherTextBytes.AddRange(eng.ProcessBlock(
                    plainTextBytes, chunkPosition, chunkSize
                ));
            }

            //var enc = cipherTextBytes.ToArray();
            //return enc.ToString();

            return Convert.ToBase64String(cipherTextBytes.ToArray());
        }

        public string Decrypt(string cipherText)
        {
            StringBuilder finalResult = new StringBuilder();

            string[] cipherTextBlocks = cipherText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var cipherTextBlock in cipherTextBlocks)
            {
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

                PemReader pr = new PemReader(
                   (StreamReader)File.OpenText(_appConfig.Value.PrivateKey)
                );
                RsaKeyParameters keys = (RsaKeyParameters)pr.ReadObject();

                // PKCS1 v1.5 paddings
                Pkcs1Encoding eng = new Pkcs1Encoding(new RsaEngine());

                // PKCS1 OAEP paddings
                //OaepEncoding eng = new OaepEncoding(new RsaEngine());
                eng.Init(false, keys);

                int length = cipherTextBytes.Length;
                int blockSize = eng.GetInputBlockSize();
                List<byte> plainTextBytes = new List<byte>();
                for (int chunkPosition = 0;
                    chunkPosition < length;
                    chunkPosition += blockSize)
                {
                    int chunkSize = Math.Min(blockSize, length - chunkPosition);
                    plainTextBytes.AddRange(eng.ProcessBlock(
                        cipherTextBytes, chunkPosition, chunkSize
                    ));
                }
                finalResult.Append(Encoding.UTF8.GetString(plainTextBytes.ToArray()));
            }
            //string result = Encoding.UTF8.GetString(plainTextBytes.ToArray());
            return finalResult.ToString();
        }

        private AsymmetricKeyParameter ReadAsymmetricKeyParameter(string pemFilename)
        {
            var fileStream = File.OpenText(pemFilename);
            var pemReader = new PemReader(fileStream);
            var KeyParameter = (AsymmetricKeyParameter)pemReader.ReadObject();
            return KeyParameter;
        }
        
    }
}
