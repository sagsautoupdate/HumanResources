using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Locations
{
    /// <summary>
    ///     This class uses a symmetric key algorithm (Rijndael/AES) to encrypt and
    ///     decrypt data. As long as encryption and decryption routines use the same
    ///     parameters to generate the keys, the keys are guaranteed to be the same.
    ///     The class uses static functions with duplicate code to make it easier to
    ///     demonstrate encryption and decryption logic. In a real-life application,
    ///     this may not be the most efficient way of handling encryption, so - as
    ///     soon as you feel comfortable with it - you may want to redesign this class.
    /// </summary>
    public class clsEncDec
    {
        private const string PASSPHRASE = "TVTG24061991";
        private const string SALTVALUE = "T12@42N V214 T12U7O721VG G14N9";
        private const string HASHALGORITHM = "SHA1";
        private const int PASSWORDINTERATION = 2;
        private const string INITVECTOR = "www.web.sags.com";
        private const int KEYSIZE = 256;

        public static string Encrypt(string plainText)
        {
            return Encrypt(plainText, PASSPHRASE, SALTVALUE, HASHALGORITHM, PASSWORDINTERATION, INITVECTOR, KEYSIZE);
        }

        public static string Decrypt(string cipherText)
        {
            return Decrypt(cipherText, PASSPHRASE, SALTVALUE, HASHALGORITHM, PASSWORDINTERATION, INITVECTOR, KEYSIZE);
        }

        /// <summary>
        ///     Encrypts specified plaintext using Rijndael symmetric key algorithm
        ///     and returns a base64-encoded result.
        /// </summary>
        /// <param name="plainText">
        ///     Plaintext value to be encrypted.
        /// </param>
        /// <param name="passPhrase">
        ///     Passphrase from which a pseudo-random password will be derived. The
        ///     derived password will be used to generate the encryption key.
        ///     Passphrase can be any string. In this example we assume that this
        ///     passphrase is an ASCII string.
        /// </param>
        /// <param name="saltValue">
        ///     Salt value used along with passphrase to generate password. Salt can
        ///     be any string. In this example we assume that salt is an ASCII string.
        /// </param>
        /// <param name="hashAlgorithm">
        ///     Hash algorithm used to generate password. Allowed values are: "MD5" and
        ///     "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
        /// </param>
        /// <param name="passwordIterations">
        ///     Number of iterations used to generate password. One or two iterations
        ///     should be enough.
        /// </param>
        /// <param name="initVector">
        ///     Initialization vector (or IV). This value is required to encrypt the
        ///     first block of plaintext data. For RijndaelManaged class IV must be
        ///     exactly 16 ASCII characters long.
        /// </param>
        /// <param name="keySize">
        ///     Size of encryption key in bits. Allowed values are: 128, 192, and 256.
        ///     Longer keys are more secure than shorter keys.
        /// </param>
        /// <returns>
        ///     Encrypted value formatted as a base64-encoded string.
        /// </returns>
        public static string Encrypt(string plainText,
            string passPhrase,
            string saltValue,
            string hashAlgorithm,
            int passwordIterations,
            string initVector,
            int keySize)
        {
            var initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            var saltValueBytes = Encoding.ASCII.GetBytes(saltValue);


            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);


            var password = new PasswordDeriveBytes(
                passPhrase,
                saltValueBytes,
                hashAlgorithm,
                passwordIterations);


            var keyBytes = password.GetBytes(keySize/8);


            var symmetricKey = new RijndaelManaged();


            symmetricKey.Mode = CipherMode.CBC;


            var encryptor = symmetricKey.CreateEncryptor(
                keyBytes,
                initVectorBytes);


            var memoryStream = new MemoryStream();


            var cryptoStream = new CryptoStream(memoryStream,
                encryptor,
                CryptoStreamMode.Write);

            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);


            cryptoStream.FlushFinalBlock();


            var cipherTextBytes = memoryStream.ToArray();


            memoryStream.Close();
            cryptoStream.Close();


            var cipherText = Convert.ToBase64String(cipherTextBytes);


            return cipherText;
        }

        /// <summary>
        ///     Decrypts specified ciphertext using Rijndael symmetric key algorithm.
        /// </summary>
        /// <param name="cipherText">
        ///     Base64-formatted ciphertext value.
        /// </param>
        /// <param name="passPhrase">
        ///     Passphrase from which a pseudo-random password will be derived. The
        ///     derived password will be used to generate the encryption key.
        ///     Passphrase can be any string. In this example we assume that this
        ///     passphrase is an ASCII string.
        /// </param>
        /// <param name="saltValue">
        ///     Salt value used along with passphrase to generate password. Salt can
        ///     be any string. In this example we assume that salt is an ASCII string.
        /// </param>
        /// <param name="hashAlgorithm">
        ///     Hash algorithm used to generate password. Allowed values are: "MD5" and
        ///     "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
        /// </param>
        /// <param name="passwordIterations">
        ///     Number of iterations used to generate password. One or two iterations
        ///     should be enough.
        /// </param>
        /// <param name="initVector">
        ///     Initialization vector (or IV). This value is required to encrypt the
        ///     first block of plaintext data. For RijndaelManaged class IV must be
        ///     exactly 16 ASCII characters long.
        /// </param>
        /// <param name="keySize">
        ///     Size of encryption key in bits. Allowed values are: 128, 192, and 256.
        ///     Longer keys are more secure than shorter keys.
        /// </param>
        /// <returns>
        ///     Decrypted string value.
        /// </returns>
        /// <remarks>
        ///     Most of the logic in this function is similar to the Encrypt
        ///     logic. In order for decryption to work, all parameters of this function
        ///     - except cipherText value - must match the corresponding parameters of
        ///     the Encrypt function which was called to generate the
        ///     ciphertext.
        /// </remarks>
        public static string Decrypt(string cipherText,
            string passPhrase,
            string saltValue,
            string hashAlgorithm,
            int passwordIterations,
            string initVector,
            int keySize)
        {
            var initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            var saltValueBytes = Encoding.ASCII.GetBytes(saltValue);


            var cipherTextBytes = Convert.FromBase64String(cipherText);


            var password = new PasswordDeriveBytes(
                passPhrase,
                saltValueBytes,
                hashAlgorithm,
                passwordIterations);


            var keyBytes = password.GetBytes(keySize/8);


            var symmetricKey = new RijndaelManaged();


            symmetricKey.Mode = CipherMode.CBC;


            var decryptor = symmetricKey.CreateDecryptor(
                keyBytes,
                initVectorBytes);


            var memoryStream = new MemoryStream(cipherTextBytes);


            var cryptoStream = new CryptoStream(memoryStream,
                decryptor,
                CryptoStreamMode.Read);


            var plainTextBytes = new byte[cipherTextBytes.Length];


            var decryptedByteCount = cryptoStream.Read(plainTextBytes,
                0,
                plainTextBytes.Length);


            memoryStream.Close();
            cryptoStream.Close();


            var plainText = Encoding.UTF8.GetString(plainTextBytes,
                0,
                decryptedByteCount);


            return plainText;
        }
    }
}