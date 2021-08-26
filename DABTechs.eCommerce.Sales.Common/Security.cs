using System;
using System.IO;
using System.Security.Cryptography;

namespace DABTechs.eCommerce.Sales.Common
{
    public class Security
    {
        private readonly Aes _aes;

        // Constructors
        public Security()
        {
            _aes = Aes.Create();

            Key = _aes.Key;
            IV = _aes.IV;
        }

        public Security(byte[] key, byte[] IV)
        {
            //Precondition checks
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));

            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException(nameof(IV));

            _aes = Aes.Create();
            Key = key;
            this.IV = IV;
        }

        public Security(byte[] key)
        {
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));

            _aes = Aes.Create();
            Key = key;
            IV = _aes.IV;
        }

        // Properties
        public byte[] Key { get; }

        public byte[] IV { get; }

        // Methods
        public byte[] Encrypt(string data)
        {
            //Precondition check
            if (data == null || data.Length <= 0)
                throw new ArgumentNullException(nameof(data));

            byte[] encryptedData;

            ICryptoTransform encryptor = _aes.CreateEncryptor(Key, IV);

            using (MemoryStream msEncryptor = new MemoryStream())
            {
                using CryptoStream csEncryptor = new CryptoStream(msEncryptor, encryptor, CryptoStreamMode.Write);
                using (StreamWriter swEncryptor = new StreamWriter(csEncryptor))
                {
                    swEncryptor.Write(data);
                }
                encryptedData = msEncryptor.ToArray();
            }
            return encryptedData;
        }

        public string Decrypt(byte[] data)
        {
            //Precondition check
            if (data == null || data.Length == 0)
                throw new ArgumentNullException(nameof(data));

            string decryptedData = null;

            ICryptoTransform decryptor = _aes.CreateDecryptor(Key, IV);

            using (StreamReader srDecryptor = new StreamReader(new CryptoStream(new MemoryStream(data), decryptor, CryptoStreamMode.Read)))
            {
                decryptedData = srDecryptor.ReadToEnd();
            }

            return decryptedData;
        }
    }
}