using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Helper
{
    public class Encryption
    {
        public byte[] HexStringToByteArray(string Hex)
        {
            byte[] Bytes = new byte[Hex.Length / 2];
            int[] HexValue = new int[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05,
                0x06, 0x07, 0x08, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

            for (int x = 0, i = 0; i < Hex.Length; i += 2, x += 1)
            {
                Bytes[x] = (byte)(HexValue[Char.ToUpper(Hex[i + 0]) - '0'] << 4 |
                                  HexValue[Char.ToUpper(Hex[i + 1]) - '0']);
            }

            return Bytes;
        }

        public void EncryptTripleDES(ref string text, ref string key)
        {
            key = string.Format("{0}AAIT{1}SA{2}", DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("HHdd"), DateTime.Now.ToString("MM"));
            string IV = "0123456789ABCDEF";
            byte[] textBytes = new byte[text.Length];
            textBytes = ASCIIEncoding.ASCII.GetBytes(text);
            byte[] keyBytes = new byte[key.Length];
            keyBytes = ASCIIEncoding.ASCII.GetBytes(key);
            byte[] ivBytes = new byte[IV.Length];
            ivBytes = HexStringToByteArray(IV);
            MemoryStream ms = new MemoryStream();
            TripleDES alg = TripleDES.Create();
            alg.Padding = PaddingMode.ANSIX923;
            alg.Key = keyBytes;
            alg.IV = ivBytes;

            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(textBytes, 0, textBytes.Length);
            cs.Close();

            byte[] encrptedBytes = ms.ToArray();

            StringBuilder Result = new StringBuilder(encrptedBytes.Length * 2);
            string HexAlphabet = "0123456789ABCDEF";

            foreach (byte B in encrptedBytes)
            {
                Result.Append(HexAlphabet[(int)(B >> 4)]);
                Result.Append(HexAlphabet[(int)(B & 0xF)]);
            }
            text = Result.ToString();
        }

        public string DecryptString(string cipherText, string key, string iv)
        {
            var keybytes = Encoding.UTF8.GetBytes(key);
            var ivbytes = Encoding.UTF8.GetBytes(iv);
            var cipherTextToDecrypt = Convert.FromBase64String(cipherText);
            string output = AESEncrytDecry.DecryptStringFromBytes(cipherTextToDecrypt, keybytes, ivbytes);
            return output;
        }
        public class AESEncrytDecry
        {
            public static string DecryptStringAES(string cipherText, string key, string iv)
            {
                var keybytes = Encoding.UTF8.GetBytes(key);
                var ivbytes = Encoding.UTF8.GetBytes(iv);
                var encrypted = Convert.FromBase64String(cipherText);
                var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, ivbytes);
                return decriptedFromJavascript;
            }

            public static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
            {
                // Check arguments.
                if (cipherText == null || cipherText.Length <= 0)
                {
                    throw new ArgumentNullException("cipherText");
                }
                if (key == null || key.Length <= 0)
                {
                    throw new ArgumentNullException("key");
                }
                if (iv == null || iv.Length <= 0)
                {
                    throw new ArgumentNullException("key");
                }

                // Declare the string used to hold
                // the decrypted text.
                string plaintext = null;

                // Create an RijndaelManaged object
                // with the specified key and IV.
                using (var rijAlg = new RijndaelManaged())
                {
                    //Settings
                    rijAlg.Mode = CipherMode.CBC;
                    rijAlg.Padding = PaddingMode.PKCS7;
                    rijAlg.FeedbackSize = 128;

                    rijAlg.Key = key;
                    rijAlg.IV = iv;

                    // Create a decrytor to perform the stream transform.
                    var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                    try
                    {
                        // Create the streams used for decryption.
                        using (var msDecrypt = new MemoryStream(cipherText))
                        {
                            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {

                                using (var srDecrypt = new StreamReader(csDecrypt))
                                {
                                    // Read the decrypted bytes from the decrypting stream
                                    // and place them in a string.
                                    plaintext = srDecrypt.ReadToEnd();

                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        plaintext = "keyError";
                    }
                }

                return plaintext;
            }

            public static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
            {
                // Check arguments.
                if (plainText == null || plainText.Length <= 0)
                {
                    throw new ArgumentNullException("plainText");
                }
                if (key == null || key.Length <= 0)
                {
                    throw new ArgumentNullException("key");
                }
                if (iv == null || iv.Length <= 0)
                {
                    throw new ArgumentNullException("key");
                }
                byte[] encrypted;
                //string encryptedStr;
                // Create a RijndaelManaged object
                // with the specified key and IV.
                using (var rijAlg = new RijndaelManaged())
                {
                    rijAlg.Mode = CipherMode.CBC;
                    rijAlg.Padding = PaddingMode.PKCS7;
                    rijAlg.FeedbackSize = 128;

                    rijAlg.Key = key;
                    rijAlg.IV = iv;

                    // Create a decrytor to perform the stream transform.
                    var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                    // Create the streams used for encryption.
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (var swEncrypt = new StreamWriter(csEncrypt))
                            {
                                //Write all data to the stream.
                                swEncrypt.Write(plainText);
                            }
                            encrypted = msEncrypt.ToArray();
                            //encryptedStr =  Encoding.UTF8.GetString(msEncrypt.ToArray());

                        }
                    }
                }

                // Return the encrypted bytes from the memory stream.
                return encrypted;
            }

        }
    }
}
