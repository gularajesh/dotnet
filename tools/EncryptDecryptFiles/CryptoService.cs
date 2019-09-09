// ***********************************************************************
// <copyright file="CryptoService.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Syngenta.SIP.Implementation.Service
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Xml.Linq;

    /// <summary>
    /// Defines the <see cref="CryptoService" />
    /// </summary>
    public class CryptoService 
    {
        
        /// <summary>
        /// Generates the key.
        /// </summary>
        /// <returns>
        /// The <see cref="string" />
        /// </returns>
        public string GenerateKey()
        {
            RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider();
            var result = rsaCryptoServiceProvider.ToXmlString(true);
            XElement element = XElement.Parse(result);

            using (RNGCryptoServiceProvider random = new RNGCryptoServiceProvider())
            {
                byte[] encryptionKey = new byte[16];
                random.GetBytes(encryptionKey);

                byte[] encryptionIV = new byte[16];
                random.GetBytes(encryptionIV);
                var aesKeyValue = new XElement("AESKeyValue", new XElement("Key", Convert.ToBase64String(encryptionKey)), new XElement("IV", Convert.ToBase64String(encryptionIV)));
                element.Add(aesKeyValue);
            }

            return element.ToString();
        }

        /// <summary>
        /// Encrypts the specified text to encrypt.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="textToEncrypt">The text to encrypt.</param>
        /// <returns>
        /// Encrypted string
        /// </returns>
        public string Encrypt(string key, string textToEncrypt)
        {
            RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider();
            rsaCryptoServiceProvider.FromXmlString(key);
            byte[] encoded = rsaCryptoServiceProvider.Encrypt(UTF8Encoding.UTF8.GetBytes(textToEncrypt), true);
            return Convert.ToBase64String(encoded);
        }

        /// <summary>
        /// Decrypts the specified text to decrypt.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="textToDecrypt">The text to decrypt.</param>
        /// <returns>
        /// Decrypted string
        /// </returns>
        public string Decrypt(string key, string textToDecrypt)
        {
            RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider();
            rsaCryptoServiceProvider.FromXmlString(key);
            byte[] decoded = rsaCryptoServiceProvider.Decrypt(Convert.FromBase64String(textToDecrypt), true);
            return UTF8Encoding.UTF8.GetString(decoded);
        }

        /// <summary>
        /// Encrypts the specified bytes to encrypt.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="bytesToEncrypt">The bytes to encrypt.</param>
        /// <returns>
        /// Encrypted bytes
        /// </returns>
        public byte[] Encrypt(string key, byte[] bytesToEncrypt)
        {
            XElement element = XElement.Parse(key);
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.KeySize = 128;
                aes.Key = Convert.FromBase64String(element.Element("AESKeyValue").Element("Key").Value);
                aes.IV = Convert.FromBase64String(element.Element("AESKeyValue").Element("IV").Value);
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (var encrypted = new MemoryStream())
                {
                    using (var plain = new MemoryStream(bytesToEncrypt))
                    {
                        using (CryptoStream cs = new CryptoStream(encrypted, encryptor, CryptoStreamMode.Write))
                        {
                            plain.CopyTo(cs);
                        }
                    }

                    return encrypted.ToArray();
                }
            }
        }

        /// <summary>
        /// Decrypts the specified bytes to decrypt.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="bytesToDecrypt">The bytes to decrypt.</param>
        /// <returns>
        /// Decrypted bytes
        /// </returns>
        public byte[] Decrypt(string key, byte[] bytesToDecrypt)
        {
            XElement element = XElement.Parse(key);
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.KeySize = 128;
                aes.Key = Convert.FromBase64String(element.Element("AESKeyValue").Element("Key").Value);
                aes.IV = Convert.FromBase64String(element.Element("AESKeyValue").Element("IV").Value);
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (var encrypted = new MemoryStream(bytesToDecrypt))
                {
                    using (var plain = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(plain, decryptor, CryptoStreamMode.Write))
                        {
                            encrypted.CopyTo(cs);
                        }

                        return plain.ToArray();
                    }
                }
            }
        }
    }
}
