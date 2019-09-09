// ***********************************************************************
// <copyright file="ICryptoService.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Syngenta.SIP.Interface.Service
{
    /// <summary>
    /// Defines the <see cref="ICryptoService" />
    /// </summary>
    public interface ICryptoService
    {
        /// <summary>
        /// Generates the key.
        /// </summary>
        /// <returns>
        /// The <see cref="string" />
        /// </returns>
        string GenerateKey();

        /// <summary>
        /// Encrypts the specified text to encrypt.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="textToEncrypt">The text to encrypt.</param>
        /// <returns>
        /// Encrypted string
        /// </returns>
        string Encrypt(string key, string textToEncrypt);

        /// <summary>
        /// Decrypts the specified text to decrypt.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="textToDecrypt">The text to decrypt.</param>
        /// <returns>
        /// Decrypted string
        /// </returns>
        string Decrypt(string key, string textToDecrypt);

        /// <summary>
        /// Encrypts the specified bytes to encrypt.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="bytesToEncrypt">The bytes to encrypt.</param>
        /// <returns>
        /// Encrypted bytes
        /// </returns>
        byte[] Encrypt(string key, byte[] bytesToEncrypt);

        /// <summary>
        /// Decrypts the specified bytes to decrypt.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="bytesToDecrypt">The bytes to decrypt.</param>
        /// <returns>
        /// Decrypted bytes
        /// </returns>
        byte[] Decrypt(string key, byte[] bytesToDecrypt);
    }
}
